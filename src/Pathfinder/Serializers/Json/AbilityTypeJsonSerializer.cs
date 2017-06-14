using System;
using Newtonsoft.Json;
using Pathfinder.Enums;

namespace Pathfinder.Serializers.Json
{
	public class AbilityTypeJsonSerializer : JsonConverter
	{
		public override bool CanConvert(Type pObjectType)
		{
			return typeof(AbilityType).IsAssignableFrom(pObjectType);
		}

		public override void WriteJson(JsonWriter pWriter, object pValue, JsonSerializer pSerializer)
		{
			if (pValue is AbilityType)
			{
				pWriter.WriteValue(pValue.ToString());
			}

			throw new JsonWriterException($"Invalid {nameof(AbilityType)} Value: {pValue}");
		}

		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			var value = pReader.Value;
			if (Enum.TryParse(value.ToString(), out AbilityType outValue))
			{
				return outValue;
			}

			throw new JsonReaderException($"Invalid {nameof(AbilityType)} Value: {value}");
		}
	}
}
