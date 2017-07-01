using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Model.Currency;
using Pathfinder.Model.Currency;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class CurrencyJsonSerializer : AbstractJsonSerializer<ICurrency>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ICurrency pValue)
		{
			pWriter.WriteValue(pValue.ToString());
		}

		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			var stringValue = pReader.Value.ToString();
			if (string.IsNullOrWhiteSpace(stringValue))
			{
				return new Copper(0);
			}

			var isMatch = new Regex(@"(\d+) (\w+)").Match(stringValue);
			if (!isMatch.Success)
			{
				throw new JsonException($"Invalid Formatting: [{nameof(ICurrency)}] \"{stringValue}\"");
			}
			var value = isMatch.Groups[1].Value.AsInt();
			var denomination = isMatch.Groups[2].Value;
			switch (denomination)
			{
				case Copper.DENOMINATION:
					return new Copper(value);
				case Silver.DENOMINATION:
					return new Silver(value);
				case Gold.DENOMINATION:
					return new Gold(value);
				case Platinum.DENOMINATION:
					return new Platinum(value);
			}

			throw new JsonException($"Unsupported {nameof(ICurrency.Denomination)}: {denomination}");
		}

		protected override ICurrency DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException("Purposefully ignored. JsonConverter.ReadJson is Overridden.");
		}
	}
}
