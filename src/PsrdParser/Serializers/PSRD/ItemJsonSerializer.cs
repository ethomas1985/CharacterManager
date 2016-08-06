using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;

namespace PsrdParser.Serializers.PSRD
{
	public class ItemJsonSerializer : JsonSerializer<IItem, string>
	{
		private const string _NAME_FIELD = "name";

		public override IItem Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);
			string itemName = getString(jObject, _NAME_FIELD);
			ItemType itemType = getItemType(jObject);

			switch (itemType)
			{
				case ItemType.None:
					return CreateItem(jObject, itemName, itemType);
				case ItemType.Weapon:
					return CreateWeapon(jObject, itemName);
				default:
					return CreateArmor(jObject, itemName, itemType);

			}
		}

		private IItem CreateArmor(JObject jObject, string itemName, ItemType itemType)
		{
			var armorNode = jObject["misc"]["Armor"];

			return new Armor(
				itemName,
				itemType,
				getCategory(jObject),
				getPrice(jObject),
				getString(jObject, "weight"),
				getString(jObject, "body"),
				getArmorBonus(armorNode),
				getShieldBonus(armorNode),
				getMaximumDexterityBonus(armorNode),
				getArmorCheckPenalty(armorNode),
				getArcaneSpellFailureChance(armorNode),
				getSpeedModifier(armorNode)
				);
		}

		private IItem CreateWeapon(JObject jObject, string itemName)
		{
			var weaponNode = jObject["misc"]["weapon"];

			return new Weapon(
				itemName,
				getCategory(jObject),
				getPrice(jObject),
				getString(jObject, "weight"),
				getString(jObject, "body"),
				getProficiency(weaponNode),
				getWeaponType(weaponNode),
				getEncumbrance(weaponNode),
				getSize(weaponNode),
				getDamageType(weaponNode),
				getBaseWeaponDamage(weaponNode),
				getCriticalThreat(weaponNode),
				getCriticalMultiplier(weaponNode),
				getRange(weaponNode),
				getSpecialText(weaponNode)
				);
		}

		private IItem CreateItem(JObject jObject, string itemName, ItemType itemType)
		{
			return new Item(
				itemName,
				itemType,
				getCategory(jObject),
				getPrice(jObject),
				getString(jObject, "weight"),
				getString(jObject, "body")
				);
		}

		private ItemType getItemType(JObject pJObject)
		{
			ItemType value;
			if (ItemType.TryParse(getString(pJObject, "subtype"), out value))
			{
				return value;
			}

			var miscNode = (string) pJObject?["misc"]?["Armor"];
			if (!string.IsNullOrEmpty(miscNode))
			{
				return ItemType.Armor;
			}

			miscNode = (string) pJObject?["misc"]?["Weapon"];
			if (!string.IsNullOrEmpty(miscNode))
			{
				return ItemType.Weapon;
			}

			return ItemType.None;
		}

		private string getMiscType(JObject pJObject)
		{
			var miscNode = pJObject?["misc"]?["Armor"];
			return miscNode?.ToString();
		}

		protected string getCategory(JObject pJObject)
		{
			var miscNode = pJObject["misc"];
			var nullNode = miscNode?["null"];
			if (nullNode == null)
			{
				return null;
			}

			return (string) nullNode["Gear Type"];
		}

		protected IPurse getPrice(JObject pJObject)
		{
			var rawValue = getString(pJObject, "price");
			if (string.IsNullOrEmpty(rawValue)
				|| "varies".Equals(rawValue)
				 || "&mdash;".Equals(rawValue)
				 || "special".Equals(rawValue))
			{
				return null;
			}

			var regex = new Regex(@"(?:(\d{1,3}(?:,\d{1,3})*) (cp|sp|gp|pp)).*");

			var match = regex.Match(rawValue);
			Assert.IsTrue(match.Success, $"{rawValue} does not match the Regex Pattern.");
			Assert.IsTrue(match.Groups[1].Success, $"Group 1 was not a success.");
			Assert.IsTrue(match.Groups[2].Success, $"Group 2 was not a success.");

			int amount;
			string stringValue = match.Groups[1].Value.Replace(",", string.Empty);
			var tryParsed = int.TryParse(stringValue, out amount);
			Assert.IsTrue(tryParsed, $"int.TryParse failed. String was '{stringValue}'");
			Assert.IsTrue(amount > 0, $"amount must be greater than 0");

			var denomination = match.Groups[2].Value;
			switch (denomination)
			{
				case "cp":
					return new Purse(amount);
				case "sp":
					return new Purse(0, amount);
				case "gp":
					return new Purse(0, 0, amount);
				case "pp":
					return new Purse(0, 0, 0, amount);
			}

			throw new Exception($"Unexpected currency denomination: {denomination}");
		}

		private int getArmorBonus(JToken pJObject)
		{
			var value = (string) pJObject["Armor Bonus"];

			return value.AsInt();
		}
		private int getShieldBonus(JToken pJObject)
		{
			var value = (string) pJObject["Shield Bonus"];

			return value.AsInt();
		}
		private int getArmorCheckPenalty(JToken pJObject)
		{
			var value = (string) pJObject["Armor Check Penalty"];

			return value.AsInt();
		}
		private int getMaximumDexterityBonus(JToken pJObject)
		{
			var value = (string) pJObject["Maximum Dex Bonus"];

			return value.AsInt();
		}
		private decimal getArcaneSpellFailureChance(JToken pJObject)
		{
			var armorBonusString = (string) pJObject["Arcane Spell Failure Chance"];

			return armorBonusString.AsInt();
		}
		private int getSpeedModifier(JToken pJObject)
		{
			var armorBonusString = (string) pJObject["Speed (30 ft.)"];

			return armorBonusString.AsInt();
		}

		private Proficiency getProficiency(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private WeaponType getWeaponType(JToken pJObject)
		{
			var strValue = (string) pJObject["Weapon Class"];

			WeaponType value;
			if (WeaponType.TryParse(strValue, out value))
			{
				return value;
			}

			throw new Exception($"Unknown Weapon Type: {value}");
		}

		private Encumbrance getEncumbrance(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private WeaponSize getSize(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private DamageType getDamageType(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<IWeaponDamage> getBaseWeaponDamage(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private int getCriticalThreat(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private int getCriticalMultiplier(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private int getRange(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		private IEnumerable<IWeaponSpecial> getSpecialText(JToken pJObject)
		{
			throw new NotImplementedException();
		}

		public override string Serialize(IItem pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
