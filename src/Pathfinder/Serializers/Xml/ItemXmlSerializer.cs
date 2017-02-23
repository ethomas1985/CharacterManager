using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Pathfinder.Serializers.Xml
{
	internal class ItemXmlSerializer : ISerializer<IItem, string>
	{
		private static readonly Regex WeightPattern = new Regex(@"(\d+) lbs?\..*");

		public IItem Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var xDocument = XDocument.Parse(pValue);

			decimal weight = decimal.TryParse(_GetElementValue(xDocument, nameof(IItem.Weight)), out weight) ? weight : 0;
			return new Item(
				_GetElementValue(xDocument, nameof(IItem.Name)),
				_GetItemType(xDocument),
				_GetElementValue(xDocument, nameof(IItem.Category)),
				_GetCost(xDocument),
				_GetWeightValue(xDocument),
				_GetElementValue(xDocument, nameof(IItem.Description))
				);
		}

		private static decimal _GetWeightValue(XContainer pXDocument)
		{
			var weightText = WeightPattern.Matches(_GetElementValue(pXDocument, nameof(IItem.Weight)))[1].Value;

			decimal weight;
			if (!decimal.TryParse(weightText, out weight))
			{
				weight = 0;
			}

			return weight;
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
			var copper = _GetElementValue(pXDocument, nameof(IPurse.Copper)).AsInt();
			var silver = _GetElementValue(pXDocument, nameof(IPurse.Silver)).AsInt();
			var gold = _GetElementValue(pXDocument, nameof(IPurse.Gold)).AsInt();
			var platinum = _GetElementValue(pXDocument, nameof(IPurse.Platinum)).AsInt();

			return new Purse(copper, silver, gold, platinum);
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
								new XElement(nameof(Item.ItemType), pObject.ItemType),
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
