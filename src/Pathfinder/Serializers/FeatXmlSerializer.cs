using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers
{
	internal class FeatXmlSerializer : ISerializer<IFeat, string>
	{
		public IFeat Deserialize(string pValue)
		{
			throw new System.NotImplementedException();
		}

		public string Serialize(IFeat pObject)
		{
			var xDocument =
				new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement(nameof(Feat),
						new List<XElement>
						{
									new XElement(nameof(Feat.Name), pObject.Name),
									new XElement(nameof(Feat.FeatType), pObject.FeatType),
									new XElement(
										nameof(Feat.Prerequisites),
										pObject.Prerequisites?
											.Select(x => new XElement("Prerequisite", x))
											.ToArray<object>()),
									new XElement(nameof(Feat.Description), pObject.Description),
									new XElement(nameof(Feat.Benefit), pObject.Benefit),
									new XElement(nameof(Feat.Special), pObject.Special),
						}.ToArray<object>()));

			return xDocument.ToString();
		}
	}
}
