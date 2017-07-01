using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Xml
{
	internal class FeatureXmlSerializer : ISerializer<IFeature, string>
	{
		public IFeature Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);

			return new Feature(
				GetName(xDocument),
				GetBody(xDocument),
				GetAbilityType(xDocument),
				GetSubFeatures(xDocument));
		}

		private static string GetName(XContainer pXDocument)
		{
			return pXDocument
				.Descendants(nameof(IFeature.Name))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private static string GetBody(XContainer pXDocument)
		{
			return pXDocument
				.Descendants(nameof(IFeature.Body))
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private static FeatureAbilityType GetAbilityType(XContainer pXDocument)
		{
			var textValue =
				pXDocument
					.Descendants(nameof(IFeature.AbilityType))
					.Select(x => x.Value)
					.FirstOrDefault();

			FeatureAbilityType value;
			if (!Enum.TryParse(textValue, out value))
			{
				throw new InvalidCastException($"Invalid FeatureAbilityTypes value: {textValue}");
			}
			return value;
		}

		private static IEnumerable<ISubFeature> GetSubFeatures(XContainer pXDocument)
		{
			return pXDocument
				.Descendants(nameof(SubFeature))
				.Select(
					x => new SubFeature(
						GetName(x),
						GetBody(x),
						GetAbilityType(x)));
		}

		public string Serialize(IFeature pObject)
		{
			var xDocument =
				new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(nameof(Feature),
								 new List<XElement>
								 {
									 new XElement(nameof(Feature.Name), pObject.Name),
									 new XElement(nameof(Feature.Body), pObject.Body),
									 new XElement(nameof(Feature.AbilityType), pObject.AbilityType),
									 new XElement(
										 nameof(Feature.SubFeatures),
										 pObject.SubFeatures?.Select(
											 subFeature => new XElement(
												 nameof(SubFeature),
												 new List<XElement>
												 {
													 new XElement(nameof(SubFeature.Name), subFeature.Name),
													 new XElement(nameof(SubFeature.Body), subFeature.Body),
													 new XElement(nameof(SubFeature.AbilityType), subFeature.AbilityType),
												 }))),
								 }));

			return xDocument.ToString();
		}
	}
}
