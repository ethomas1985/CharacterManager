using System;
using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model.Currency;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model;

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
				_GetWeightValue(xDocument),
                _SplitParagraphs(_GetElementValue(xDocument, nameof(IItem.Description)))
				);
		}

        private IEnumerable<string> _SplitParagraphs(string pGetElementValue)
        {
            return WebUtility.HtmlDecode(pGetElementValue)
                .Replace(@"<\p>", Environment.NewLine)
                .Replace(@"<p>", string.Empty)
                .Split(new [] {Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static decimal _GetWeightValue(XContainer pXDocument)
		{
			return _GetElementValue(pXDocument, nameof(IItem.Weight)).AsDecimal();
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
            var regex = new Regex(@"(\d+)(?: [csgp]p)?");
            string capture(string value)
            {
                return regex.Match(value).Groups[1].Value;
            };

            var copper = capture(_GetElementValue(pXDocument, nameof(IPurse.Copper))).AsInt();
			var silver = capture(_GetElementValue(pXDocument, nameof(IPurse.Silver))).AsInt();
			var gold = capture(_GetElementValue(pXDocument, nameof(IPurse.Gold))).AsInt();
			var platinum = capture(_GetElementValue(pXDocument, nameof(IPurse.Platinum))).AsInt();

			return new Purse(copper, silver, gold, platinum);
		}

		public string Serialize(IItem pObject)
		{
			var weapon = pObject.WeaponComponent;
			var armor = pObject.ArmorComponent;

			var xDocument =
					new XDocument(
						new XDeclaration("1.0", "utf-8", "yes"),
						new XElement(
							nameof(Item),
							new XElement(nameof(IItem.Name), pObject.Name),
							new XElement(nameof(IItem.Category), pObject.Category),
							new XElement(nameof(IItem.ItemType), pObject.ItemType),
							new XElement(
								nameof(IItem.Cost),
								new XElement(nameof(IPurse.Copper), pObject.Cost?.Copper),
								new XElement(nameof(IPurse.Silver), pObject.Cost?.Silver),
								new XElement(nameof(IPurse.Gold), pObject.Cost?.Gold),
								new XElement(nameof(IPurse.Platinum), pObject.Cost?.Platinum)
								),
							new XElement(nameof(IItem.Weight), pObject.Weight),
							new XElement(nameof(IItem.Description), pObject.Description),
							weapon == null
								? null
								: new XElement(
									nameof(IItem.WeaponComponent),
									new XElement(nameof(IWeaponComponent.Proficiency), weapon.Proficiency),
									new XElement(nameof(IWeaponComponent.WeaponType), weapon.WeaponType),
									new XElement(nameof(IWeaponComponent.Encumbrance), weapon.Encumbrance),
									new XElement(nameof(IWeaponComponent.Size), weapon.Size),
									new XElement(nameof(IWeaponComponent.DamageType), weapon.DamageType),
									new XElement(nameof(IWeaponComponent.BaseWeaponDamage), weapon.BaseWeaponDamage.Select(x => new XElement(nameof(Die), $"{x.DieCount}d{x.Die.Faces}"))),
									new XElement(nameof(IWeaponComponent.CriticalThreat), weapon.CriticalThreat),
									new XElement(nameof(IWeaponComponent.CriticalMultiplier), weapon.CriticalMultiplier),
									new XElement(nameof(IWeaponComponent.Range), weapon.Range),
									new XElement(nameof(IWeaponComponent.Specials), weapon.Specials)),
							armor == null
								? null
								: new XElement(
									nameof(IItem.ArmorComponent),
									new XElement(nameof(IArmorComponent.ArmorBonus), armor.ArmorBonus),
									new XElement(nameof(IArmorComponent.ShieldBonus), armor.ShieldBonus),
									new XElement(nameof(IArmorComponent.MaximumDexterityBonus), armor.MaximumDexterityBonus),
									new XElement(nameof(IArmorComponent.ArmorCheckPenalty), armor.ArmorCheckPenalty),
									new XElement(nameof(IArmorComponent.ArcaneSpellFailureChance), armor.ArcaneSpellFailureChance),
									new XElement(nameof(IArmorComponent.SpeedModifier), armor.SpeedModifier))
						)
					);

			return xDocument.ToString();
		}
	}
}
