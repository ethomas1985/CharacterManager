using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Utilities;

namespace PsrdParser.Serializers.PSRD
{
	public class ItemJsonSerializer : JsonSerializer<IItem, string>
	{
		private const string NAME_FIELD = "name";

		public override IItem Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);
			string itemName = getString(jObject, NAME_FIELD);
			return new Item(
				itemName,
				getItemType(jObject),
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
			return ItemType.None;
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
					return new Purse(0,0,amount);
				case "pp":
					return new Purse(0,0,0,amount);
			}

			throw new Exception($"Unexpected currency denomination: {denomination}");
		}

		public override string Serialize(IItem pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
