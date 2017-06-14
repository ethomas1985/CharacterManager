using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;

namespace Pathfinder.Serializers.Json
{
	public class SavingThrowJsonSerializer : AbstractJsonSerializer<ISavingThrow>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ISavingThrow pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ISavingThrow.Type), pValue.Type);
			WriteProperty(pWriter, pSerializer, nameof(ISavingThrow.AbilityModifier), pValue.AbilityModifier);
			WriteProperty(pWriter, pSerializer, nameof(ISavingThrow.Base), pValue.Base);
			WriteProperty(pWriter, pSerializer, nameof(ISavingThrow.Resist), pValue.Resist);
			WriteProperty(pWriter, pSerializer, nameof(ISavingThrow.Misc), pValue.Misc);
			WriteProperty(pWriter, pSerializer, nameof(ISavingThrow.Temporary), pValue.Temporary);
			WriteProperty(pWriter, pSerializer, nameof(ISavingThrow.Score), pValue.Score);

			pWriter.WriteEndObject();
		}

		protected override ISavingThrow DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException($"Deserializing {nameof(ISavingThrow)} types is not supported.");
		}
	}
}
