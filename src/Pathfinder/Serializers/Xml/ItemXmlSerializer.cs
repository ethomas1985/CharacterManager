using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Xml
{
	internal class ItemXmlSerializer : ISerializer<IItem, string>
	{
		public IItem Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);

			return new Item(
				_GetElementValue(xDocument, nameof(IItem.Name)),
				_GetElementValue(xDocument, nameof(IItem.Category)),
				_GetCost(xDocument),
				_GetElementValue(xDocument, nameof(IItem.Weight)),
				_GetElementValue(xDocument, nameof(IItem.Description))
				);
		}

		private static string _GetElementValue(XContainer pXDocument, string pName)
		{
			return pXDocument
				.Descendants(pName)
				.Select(x => x.Value)
				.FirstOrDefault();
		}

		private static Money _GetCost(XContainer pXDocument)
		{
			var copper = _AsInt(_GetElementValue(pXDocument, nameof(IMoney.Copper)));
			var silver = _AsInt(_GetElementValue(pXDocument, nameof(IMoney.Silver)));
			var gold = _AsInt(_GetElementValue(pXDocument, nameof(IMoney.Gold)));
			var platinum = _AsInt(_GetElementValue(pXDocument, nameof(IMoney.Platinum)));

			return new Money(copper, silver, gold, platinum);
		}

		private static int _AsInt(string pValue)
		{
			int value;
			return int.TryParse(pValue, out value) ? value : default(int);
		}

		public string Serialize(IItem pObject)
		{
			var xDocument =
					new XDocument(
						new XDeclaration("1.0", "utf-8", "yes"),
						new XElement(
							nameof(Item),
							new List<XElement>
							{
								new XElement(nameof(Item.Name), pObject.Name),
								new XElement(nameof(Item.Category), pObject.Category),
								new XElement(
									nameof(Item.Cost),
									new List<XElement>
									{
										new XElement(nameof(IMoney.Copper), pObject.Cost.Copper),
										new XElement(nameof(IMoney.Silver), pObject.Cost.Silver),
										new XElement(nameof(IMoney.Gold), pObject.Cost.Gold),
										new XElement(nameof(IMoney.Platinum), pObject.Cost.Platinum),
									}),
								new XElement(nameof(Item.Weight), pObject.Weight),
								new XElement(nameof(Item.Description), pObject.Description),

							}.ToArray<object>()));
			return xDocument.ToString();
		}
	}
}
