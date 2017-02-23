using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using Pathfinder.Library;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Assert = Pathfinder.Utilities.Assert;

namespace PsrdParser.Serializers.PSRD
{
	public class ItemJsonSerializer : JsonSerializer<IItem, string>
	{
		private const string NAME_JSON_ATTRIBUTE = "name";
		private const string FIELD_JSON_ATTRIBUTE = "body";
		private const string SUBTYPE_JSON_ATTRIBUTE = "subtype";

		private const string MISC_JSON_ATTRIBUTE = "misc";
		private const string WEAPON_JSON_ATTRIBUTE = "Weapon";
		private const string ARMOR_JSON_ATTRIBUTE = "Armor";

		private static readonly Regex WeightPattern = new Regex(@"(\d+) lbs?\..*");
		private static readonly WeaponSpecialsLibrary WeaponSpecialsLibrary = new WeaponSpecialsLibrary();

		public override IItem Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);
			string itemName = getString(jObject, NAME_JSON_ATTRIBUTE);
			ItemType itemType = GetItemType(jObject);

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

		private IItem CreateArmor(JObject pJObject, string pItemName, ItemType pItemType)
		{
			var armorNode = pJObject[MISC_JSON_ATTRIBUTE]?[ARMOR_JSON_ATTRIBUTE];

			return new Armor(
				pItemName,
				pItemType,
				getCategory(pJObject),
				getPrice(pJObject),
				_GetWeightValue(pJObject),
				getString(pJObject, FIELD_JSON_ATTRIBUTE),
				GetArmorBonus(armorNode),
				GetShieldBonus(armorNode),
				GetMaximumDexterityBonus(armorNode),
				GetArmorCheckPenalty(armorNode),
				GetArcaneSpellFailureChance(armorNode),
				GetSpeedModifier(armorNode)
				);
		}

		private IItem CreateWeapon(JObject pJObject, string pItemName)
		{
			var weaponNode = pJObject[MISC_JSON_ATTRIBUTE]?[WEAPON_JSON_ATTRIBUTE];

			return new Weapon(
				pItemName,
				getCategory(pJObject),
				getPrice(pJObject),
				_GetWeightValue(pJObject),
				getString(pJObject, FIELD_JSON_ATTRIBUTE),
				GetProficiency(weaponNode),
				GetWeaponType(weaponNode),
				GetEncumbrance(weaponNode),
				GetSize(weaponNode),
				GetDamageType(weaponNode),
				GetBaseWeaponDamage(weaponNode),
				GetCriticalThreat(weaponNode),
				GetCriticalMultiplier(weaponNode),
				GetRange(weaponNode),
				GetSpecialText(weaponNode)
				);
		}

		private IItem CreateItem(JObject pJObject, string pItemName, ItemType pItemType)
		{
			return new Item(
				pItemName,
				pItemType,
				getCategory(pJObject),
				getPrice(pJObject),
				_GetWeightValue(pJObject),
				getString(pJObject, FIELD_JSON_ATTRIBUTE)
				);
		}

		private ItemType GetItemType(JObject pJObject)
		{
			var itemType = getString(pJObject, SUBTYPE_JSON_ATTRIBUTE) ?? string.Empty;
			var textInfo = new CultureInfo("en-US", false).TextInfo;
			itemType = textInfo.ToTitleCase(itemType);
			ItemType value;
			if (ItemType.TryParse(itemType, out value))
			{
				return value;
			}

			var miscNode = pJObject?[MISC_JSON_ATTRIBUTE]?[ARMOR_JSON_ATTRIBUTE]?.ToString();
			if (!string.IsNullOrEmpty(miscNode))
			{
				return ItemType.Armor;
			}

			miscNode = pJObject?[MISC_JSON_ATTRIBUTE]?[WEAPON_JSON_ATTRIBUTE]?.ToString();
			if (!string.IsNullOrEmpty(miscNode))
			{
				return ItemType.Weapon;
			}

			return ItemType.None;
		}

		private static decimal _GetWeightValue(JObject pJObject)
		{
			var value = getString(pJObject, "weight");
			if (string.IsNullOrEmpty(value))
			{
				return 0;
			}

			var match = WeightPattern.Match(value);
			if (!match.Success || match.Groups.Count == 1)
			{
				return 0;
			}

			var weightText = match.Groups[1].Value;

			decimal weight;
			if (!decimal.TryParse(weightText, out weight))
			{
				weight = 0;
			}

			return weight;
		}

		protected string getCategory(JObject pJObject)
		{
			var miscNode = pJObject[MISC_JSON_ATTRIBUTE];
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

		private int GetArmorBonus(JToken pJObject)
		{
			if (pJObject == null)
			{
				return 0;
			}

			var value = (string) pJObject["Armor Bonus"];

			return value.AsInt();
		}
		private int GetShieldBonus(JToken pJObject)
		{
			if (pJObject == null)
			{
				return 0;
			}

			var value = (string) pJObject["Shield Bonus"];

			return value.AsInt();
		}
		private int GetArmorCheckPenalty(JToken pJObject)
		{
			if (pJObject == null)
			{
				return 0;
			}

			var value = (string) pJObject["Armor Check Penalty"];

			return value.AsInt();
		}
		private int GetMaximumDexterityBonus(JToken pJObject)
		{
			if (pJObject == null)
			{
				return 0;
			}

			var value = (string) pJObject["Maximum Dex Bonus"];

			return value.AsInt();
		}
		private decimal GetArcaneSpellFailureChance(JToken pJObject)
		{
			if (pJObject == null)
			{
				return 0;
			}

			var armorBonusString = (string) pJObject["Arcane Spell Failure Chance"];

			return armorBonusString.AsInt();
		}
		private int GetSpeedModifier(JToken pJObject)
		{
			if (pJObject == null)
			{
				return 0;
			}

			var armorBonusString = (string) pJObject["Speed (30 ft.)"];

			return armorBonusString.AsInt();
		}

		private Proficiency GetProficiency(JToken pJObject)
		{
			var proficiency = pJObject[nameof(Proficiency)]?.Value<string>();

			switch (proficiency)
			{
				case "Simple Weapons":
					return Proficiency.Simple;
				case "Martial Weapons":
					return Proficiency.Martial;
				case "Exotic Weapons":
					return Proficiency.Exotic;
				default: 
					return Proficiency.None;
			}
		}

		private WeaponType GetWeaponType(JToken pJObject)
		{
			var strValue = (string) pJObject["Weapon Class"];

			WeaponType value = WeaponType.None;
			switch(strValue)
			{
				case "Unarmed Attacks":
					return WeaponType.Unarmed;
				case "Light Melee Weapons":
					return WeaponType.LightMelee;
				case "One-Handed Melee Weapons":
					return WeaponType.OneHandedMelee;
				case "Two-Handed Melee Weapons":
					return WeaponType.TwoHandedMelee;
				case "Ranged Weapons":
					return WeaponType.Ranged;
			}

			throw new Exception($"Unknown Weapon Type: {strValue}");
		}

		private Encumbrance GetEncumbrance(JToken pJObject)
		{
			return Encumbrance.None;
		}

		private WeaponSize GetSize(JToken pJObject)
		{
			return WeaponSize.Medium;
		}

		private DamageType GetDamageType(JToken pJObject)
		{
			var types = pJObject["Type"]?.Value<string>()?.Split(new [] {" or "}, StringSplitOptions.RemoveEmptyEntries);
			
			var weaponType = DamageType.None;
			if (types == null)
			{
				return weaponType;
			}

			foreach (var type in types)
			{
				switch (type)
				{
					case "P":
						weaponType |= DamageType.Piercing;
						break;
					case "B":
						weaponType |= DamageType.Bludgeoning;
						break;
					case "S":
						weaponType |= DamageType.Slashing;
						break;
				}
			}

			return weaponType;
		}

		private IEnumerable<IDice> GetBaseWeaponDamage(JToken pJObject)
		{
			var pattern = new Regex(@"(\d+)d(\d+)");

			var values = pJObject["Dmg (M)"]?.Value<string>()?.Split(new [] {"/"}, StringSplitOptions.RemoveEmptyEntries);

			var list = new List<IDice>();
			if (values == null)
			{
				return list;
			}

			foreach (var value in values)
			{
				var match = pattern.Match(value);
				if (!match.Success)
				{
					throw new Exception($"Parsing error");
				}

				var count = match.Groups[1].Value.AsInt();
				var die = new Die(match.Groups[2].Value.AsInt());

				list.Add(new Dice(count, die));
			}

			return list;
		}

		private int GetCriticalThreat(JToken pJObject)
		{
			const int defaultCriticalThreat = 20;

			var pattern = new Regex(@"(\d+)&ndash;\d+");

			var values = pJObject["Critical"]?.Value<string>();
			if (string.IsNullOrEmpty(values))
			{
				return defaultCriticalThreat;
			}

			var match = pattern.Match(values);
			if (!match.Success)
			{
				return defaultCriticalThreat;
			}

			int crit;
			if (!int.TryParse(match.Groups[1].Value, out crit))
			{
				crit = defaultCriticalThreat;
			}

			return crit;
		}

		private int GetCriticalMultiplier(JToken pJObject)
		{
			const int defaultCriticalMultiplier = 2;

			var pattern = new Regex(@"(?:\d+&ndash;\d+/)&times;(\d+)");

			var values = pJObject["Critical"]?.Value<string>();
			if (string.IsNullOrEmpty(values))
			{
				return defaultCriticalMultiplier;
			}

			var match = pattern.Match(values);
			if (!match.Success)
			{
				return defaultCriticalMultiplier;
			}

			int crit;
			if (!int.TryParse(match.Groups[1].Value, out crit))
			{
				crit = defaultCriticalMultiplier;
			}

			return crit;
		}

		private int GetRange(JToken pJObject)
		{
			var pattern = new Regex(@"(\d+)(?: ft.)?");
			var value = pJObject["Range"]?.Value<string>();
			if (string.IsNullOrEmpty(value))
			{
				return 0;
			}

			var match = pattern.Match(value);
			if (!match.Success)
			{
				return 0;
			}

			int crit;
			if (!int.TryParse(match.Groups[1].Value, out crit))
			{
				crit = 0;
			}

			return crit;
		}

		private IEnumerable<IWeaponSpecial> GetSpecialText(JToken pJObject)
		{
			var specialText = pJObject["Special"]?.Value<string>()?.Split(',').Select(x => x.Trim()).ToList();
			if (specialText == null || !specialText.Any())
			{
				return null;
			}
			return specialText.Select(x => WeaponSpecialsLibrary[x]).ToList();
		}

		public override string Serialize(IItem pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
