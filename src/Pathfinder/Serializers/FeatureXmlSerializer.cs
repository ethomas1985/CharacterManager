using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers
{
	internal class FeatureXmlSerializer: ISerializer<IFeature, string>
	{
		public IFeature Deserialize(string pValue)
		{
			throw new System.NotImplementedException();
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
