using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Model;

namespace Pathfinder.Serializers.Json
{
	public class OffensiveScoreJsonSerializer : AbstractJsonSerializer<IOffensiveScore>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IOffensiveScore pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IOffensiveScore.Type), pValue.Type);
			WriteProperty(pWriter, pSerializer, nameof(IOffensiveScore.AbilityModifier), pValue.AbilityModifier);
			WriteProperty(pWriter, pSerializer, nameof(IOffensiveScore.BaseAttackBonus), pValue.BaseAttackBonus);
			WriteProperty(pWriter, pSerializer, nameof(IOffensiveScore.SizeModifier), pValue.SizeModifier);
			WriteProperty(pWriter, pSerializer, nameof(IOffensiveScore.TemporaryModifier), pValue.TemporaryModifier);
			WriteProperty(pWriter, pSerializer, nameof(IOffensiveScore.Score), pValue.Score);

			pWriter.WriteEndObject();
		}

		protected override IOffensiveScore DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException($"Deserializing {nameof(IOffensiveScore)} types is not supported.");
		}
	}
}
