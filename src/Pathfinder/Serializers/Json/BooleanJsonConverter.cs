using System;
using Newtonsoft.Json;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class BooleanJsonConverter: JsonConverter
	{
		public override bool CanConvert(Type pObjectType)
		{
			return typeof(bool).IsAssignableFrom(pObjectType);
		}

		public override void WriteJson(JsonWriter pWriter, object pValue, JsonSerializer pSerializer)
		{
			pWriter.WriteRawValue(pValue.ToString().ToCamelCase());
		}

		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			throw new NotImplementedException();
		}
	}
}
