using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;
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
			return new Item(
				getString(jObject, NAME_FIELD),
				getCategory(jObject),
				getPrice(jObject),
				getString(jObject, "weight"),
				getString(jObject, "body")
				);
		}

		protected string getCategory(JObject pJObject)
		{
			var miscNode = pJObject["misc"];
			var nullNode = miscNode["null"];
			return (string) nullNode.First;
		}

		protected IMoney getPrice(JObject pJObject)
		{
			var rawValue = getString(pJObject, "price");

			var regex = new Regex(@"(?:([\d,]+) (cp|sp|gp|pp))/.*");

			var match = regex.Match(rawValue);
			Assert.IsTrue(match.Success, $"{rawValue} does not match the Regex Pattern.");
			Assert.IsTrue(match.Groups[1].Success, $"Group 1 was not a success.");
			Assert.IsTrue(match.Groups[2].Success, $"Group 2 was not a success.");

			int amount;
			var tryParsed = int.TryParse(match.Groups[1].Value, out amount);
			Assert.IsTrue(tryParsed, $"int.TryParse failed.");
			Assert.IsTrue(amount > 0, $"amount must be greater than 0");

			var denomination = match.Groups[2].Value;
			switch (denomination)
			{
				case "cp":
					return new Money(amount);
				case "sp":
					return new Money(0, amount);
				case "gp":
					return new Money(0,0,amount);
				case "pp":
					return new Money(0,0,0,amount);
			}

			throw new Exception($"Unexpected currency denomination: {denomination}");
		}

		public override string Serialize(IItem pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
