using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Xml
{
	internal class SpellXmlSerializer : ISerializer<ISpell, string>
	{
		public ISpell Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var document = XDocument.Parse(pValue);

			return
				new Spell(
					GetName(document),
					GetMagicSchool(document),
					GetSubMagicSchool(document),
					GetMagicDescriptors(document),
					GetSavingThrow(document),
					GetDescription(document),
					HasSpellResistance(document),
					GetSpellResistance(document),
					GetCastingTime(document),
					GetRange(document),
					GetLevelRequirements(document),
					GetDuration(document),
					GetComponents(document));
		}

		private string GetName(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.Name))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private MagicSchool GetMagicSchool(XDocument pDocument)
		{
			var srcValue = pDocument
				.Descendants(nameof(ISpell.School))
				.Select(x => x.Value)
				.FirstOrDefault();

			MagicSchool value;
			if (!Enum.TryParse(srcValue, out value))
			{
				throw new InvalidCastException($"Invalid Magic School: {srcValue}");
			}
			return value;
		}

		private MagicSubSchool GetSubMagicSchool(XDocument pDocument)
		{
			var srcValue = pDocument
				.Descendants(nameof(ISpell.School))
				.Select(x => x.Value)
				.FirstOrDefault();

			MagicSubSchool value;
			if (!Enum.TryParse(srcValue, out value))
			{
				throw new InvalidCastException($"Invalid Sub-Magic School: {srcValue}");
			}
			return value;
		}

		private ISet<MagicDescriptor> GetMagicDescriptors(XDocument pDocument)
		{
			var srcValues = pDocument
				.Descendants(nameof(ISpell.School))
				.Select(x =>
						{
							MagicDescriptor value;
							if (!Enum.TryParse(x.Value, out value))
							{
								throw new InvalidCastException($"Invalid Magic Descriptor: {x.Value}");
							}
							return value;
						});


			return new HashSet<MagicDescriptor>(srcValues);
		}

		private string GetSavingThrow(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.SavingThrow))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private string GetDescription(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.Description))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private bool HasSpellResistance(XDocument pDocument)
		{
			var srcValue = pDocument
				.Descendants(nameof(ISpell.HasSpellResistance))
				.Select(x => x.Value)
				.FirstOrDefault();

			bool value;
			return bool.TryParse(srcValue, out value) && value;
		}

		private string GetSpellResistance(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.SpellResistance))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private string GetCastingTime(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.CastingTime))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private string GetRange(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.Range))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private IDictionary<string, int> GetLevelRequirements(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.LevelRequirements))
				.Select(
					x => new
					{
						ClassName = x.Descendants(nameof(Class)).Select(y => y.Value).First(),
						Level = x.Descendants(nameof(IClassLevel.Level)).Select(y => y.Value).First()
					})
				.ToDictionary(
					x => x.ClassName,
					x => ToInt(x.Level));
		}

		private static int ToInt(string pValue)
		{
			int value;
			return int.TryParse(pValue, out value) ? value : 0;
		}

		private string GetDuration(XDocument pDocument)
		{
			return pDocument
				.Descendants(nameof(ISpell.Duration))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private ISet<Tuple<ComponentType, string>> GetComponents(XDocument pDocument)
		{
			var components = 
				pDocument
					.Descendants(nameof(ISpell.Components))
					.Descendants("Component")
					.ToList();
			if (!components.Any())
			{
				return null;
			}

			var tuples =
				components
					.Select(
						x => new Tuple<ComponentType, string>(
							x.Descendants("Type").Select(y => ToComponentType(y.Value)).First(),
							x.Descendants("Text").Select(y => y.Value).First()
							))
					.ToList();
			return tuples.Any() ? new HashSet<Tuple<ComponentType, string>>(tuples) : null;
		}

		private static ComponentType ToComponentType(string y)
		{
			ComponentType value;
			return Enum.TryParse(y, out value) ? value : default(ComponentType);
		}

		public string Serialize(ISpell pObject)
		{
			var xDocument =
				new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(nameof(Spell),
						new List<XElement>
						{
							new XElement(nameof(ISpell.Name), pObject.Name),
							new XElement(nameof(ISpell.School), pObject.School),
							new XElement(nameof(ISpell.SubSchool), pObject.SubSchool),
							new XElement(
								nameof(ISpell.MagicDescriptors),
								pObject.MagicDescriptors?.Select(
									x => new XElement(nameof(MagicDescriptor), x))),
							new XElement(nameof(ISpell.SavingThrow), pObject.SavingThrow),
							new XElement(nameof(ISpell.Description), pObject.Description),
							new XElement(nameof(ISpell.HasSpellResistance), pObject.HasSpellResistance),
							new XElement(nameof(ISpell.SpellResistance), pObject.SpellResistance),
							new XElement(nameof(ISpell.CastingTime), pObject.CastingTime),
							new XElement(nameof(ISpell.Range), pObject.Range),
							new XElement(
								nameof(ISpell.LevelRequirements),
								pObject.LevelRequirements.Select(
									x => new XElement("LevelRequirement", new XElement(nameof(Class), x.Key), new XElement(nameof(IClassLevel.Level), x.Value)))),
							new XElement(nameof(ISpell.Duration), pObject.Duration),
							new XElement(
								nameof(ISpell.Components),
								pObject.Components?.Select(
									x => new XElement("Component", new XElement("Type", x.Item1), new XElement("Text", x.Item2))))
						}.ToArray<object>()));

			return xDocument.ToString();
		}
	}
}
