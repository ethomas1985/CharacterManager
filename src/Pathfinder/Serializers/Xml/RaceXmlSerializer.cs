﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Xml
{
	internal class RaceXmlSerializer : ISerializer<IRace, string>
	{
		public RaceXmlSerializer(ILegacyRepository<ITrait> pTraitRepository)
		{
			TraitRepository = pTraitRepository;
		}

		public ILegacyRepository<ITrait> TraitRepository { get; }

		public IRace Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);

			var name = GetName(xDocument);
			var adjective = GetAdjective(xDocument);
			var description = GetDescription(xDocument);
			var size = GetSize(xDocument);
			var baseSpeed = GetBaseSpeed(xDocument);
			var abilities = GetAbilities(xDocument);
			var traits = GetTraits(xDocument, TraitRepository);
			var languages = GetLanguages(xDocument);

			try
			{
				return new Race(name, adjective, description, size, baseSpeed, abilities, traits, languages);
			}
			catch (Exception ex)
			{
				Tracer.Message(pMessage: ex.ToString());
				throw;
			}
		}

		private static string GetName(XDocument xDocument)
		{
			return
				xDocument
					.Descendants(nameof(Race.Name))
					.Select(x => x.Value)
					.FirstOrDefault();
		}

		private static string GetAdjective(XDocument xDocument)
		{
			return
				xDocument
					.Descendants(nameof(Race.Adjective))
					.Select(x => x.Value)
					.FirstOrDefault();
		}
		private static string GetDescription(XDocument xDocument)
		{
			var description =
				xDocument
					.Descendants(nameof(Race.Description))
					.Select(x => x.Value)
					.FirstOrDefault();
			return description;
		}
		private static Size GetSize(XDocument xDocument)
		{
			var sizeValue =
				xDocument
					.Descendants(nameof(Race.Size))
					.Select(x => x.Value)
					.FirstOrDefault();

			Size outSize;
			var sizeParsed = Size.TryParse(sizeValue, out outSize);
			var size = sizeParsed ? outSize : Size.Medium;
			return size;
		}
		private static int GetBaseSpeed(XDocument xDocument)
		{
			int baseSpeed;
			int.TryParse(
				xDocument
					.Descendants(nameof(Race.BaseSpeed))
					.Select(x => x.Value)
					.FirstOrDefault(), out baseSpeed);
			return baseSpeed;
		}
		private static Dictionary<AbilityType, int> GetAbilities(XDocument xDocument)
		{
			var abilities =
				xDocument
					.Descendants(nameof(Race.AbilityScores))
					.Descendants()
					.ToDictionary(
						x =>
						{
							AbilityType outType;
							AbilityType.TryParse(x.Name.LocalName, out outType);
							return outType;
						},
						v =>
						{
							int outValue;
							int.TryParse(v.Value, out outValue);
							return outValue;
						});
			return abilities;
		}
		private static IEnumerable<ITrait> GetTraits(XDocument xDocument, ILegacyRepository<ITrait> pRepository)
		{
			var traits =
				xDocument
					.Descendants(nameof(Race.Traits))
					.Descendants()
					.Select(x => pRepository[x.Value]);
			return traits?.ToList();
		}
		private static IEnumerable<Language> GetLanguages(XDocument xDocument)
		{
			var languages =
				xDocument
					.Descendants(nameof(Race.Languages))
					.Descendants()
					.Select(x => new Language(x.Value));
			return languages?.ToList();
		}

		public string Serialize(IRace pObject)
		{
			var xDocument =
				new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(nameof(Race),
						new List<XElement>
						{
							new XElement(nameof(Race.Name), pObject.Name),
							new XElement(nameof(Race.Adjective), pObject.Adjective),
							new XElement(nameof(Race.Description), pObject.Description),
							new XElement(nameof(Race.Size), pObject.Size),
							new XElement(nameof(Race.BaseSpeed), pObject.BaseSpeed),
							new XElement(
								nameof(Race.AbilityScores),
								pObject.AbilityScores
									.Select(x => new XElement(x.Key.ToString(), x.Value))
									.ToArray<object>()),
							new XElement(
								nameof(Race.Traits),
								pObject.Traits
									.Select(x => new XElement(nameof(Trait), x.Name))
									.ToArray<object>()),
							new XElement(
								nameof(Race.Languages),
								pObject.Languages
									.Select(x => new XElement(nameof(Language), x.Name))
									.ToArray<object>())
						}.ToArray<object>()));

			return xDocument.ToString();
		}
	}
}