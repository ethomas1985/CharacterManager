using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
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
				_GetItemType(xDocument),
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

		private static ItemType _GetItemType(XContainer pXDocument)
		{
			var itemType = _GetElementValue(pXDocument, nameof(IItem.ItemType));
			ItemType value;
			if (ItemType.TryParse(itemType, out value))
			{
				return value;
			}
			return ItemType.None;
		}

		private static Purse _GetCost(XContainer pXDocument)
		{
			var copper = _AsInt(_GetElementValue(pXDocument, nameof(IPurse.Copper)));
			var silver = _AsInt(_GetElementValue(pXDocument, nameof(IPurse.Silver)));
			var gold = _AsInt(_GetElementValue(pXDocument, nameof(IPurse.Gold)));
			var platinum = _AsInt(_GetElementValue(pXDocument, nameof(IPurse.Platinum)));

			return new Purse(copper, silver, gold, platinum);
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
										new XElement(nameof(IPurse.Copper), pObject.Cost?.Copper),
										new XElement(nameof(IPurse.Silver), pObject.Cost?.Silver),
										new XElement(nameof(IPurse.Gold), pObject.Cost?.Gold),
										new XElement(nameof(IPurse.Platinum), pObject.Cost?.Platinum),
									}),
								new XElement(nameof(Item.Weight), pObject.Weight),
								new XElement(nameof(Item.Description), pObject.Description),

							}.ToArray<object>()));
			return xDocument.ToString();
		}
	}
}
