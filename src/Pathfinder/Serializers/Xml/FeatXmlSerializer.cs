using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Interface.Model;

namespace Pathfinder.Serializers.Xml
{
	internal class FeatXmlSerializer : ISerializer<IFeat, string>
	{
		public IFeat Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);

			var description = _GetElementValue(xDocument, nameof(IFeat.Description));

			return new Feat(
				_GetElementValue(xDocument, nameof(IFeat.Name)),
				_GetFeatType(xDocument),
				_GetPrerequisites(xDocument),
				description,
				_GetElementValue(xDocument, nameof(IFeat.Benefit)),
				_GetElementValue(xDocument, nameof(IFeat.Special))
			);
		}

		private static string _GetElementValue(XContainer pXDocument, string pName)
		{
			return pXDocument
				.Descendants(pName)
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private static FeatType _GetFeatType(XContainer pXDocument)
		{
			var itemType = _GetElementValue(pXDocument, nameof(IFeat.FeatType));
			FeatType value;
			if (FeatType.TryParse(itemType, out value))
			{
				return value;
			}
			return FeatType.General;
		}

		private IEnumerable<string> _GetPrerequisites(XContainer pXDocument)
		{
			var prerequisites =
				new List<string>(
					pXDocument
						.Descendants(nameof(IFeat.Prerequisites))
						.Select(x => x.Value));
			return prerequisites;
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
									new XElement(nameof(Feat.IsSpecialized), pObject.IsSpecialized),
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
