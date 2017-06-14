using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class LanguageJsonSerializer: AbstractJsonSerializer<ILanguage>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ILanguage pValue)
		{
			pWriter.WriteValue(pValue.Name);
		}
		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			var tokens = pReader.Value as string;
			if (string.IsNullOrWhiteSpace(tokens))
			{
				throw new JsonException("Invalid Value");
			}
			return new Language(tokens);
		}

		protected override ILanguage DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException("Purposefully ignored. JsonConverter.ReadJson is Overridden.");
		}
	}
}
