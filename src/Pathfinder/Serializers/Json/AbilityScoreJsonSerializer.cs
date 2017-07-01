using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class AbilityScoreJsonSerializer : AbstractJsonSerializer<IAbilityScore>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IAbilityScore pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Type), pValue.Type);
			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Score), pValue.Score);
			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Modifier), pValue.Modifier);
			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Base), pValue.Base);
			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Enhanced), pValue.Enhanced);
			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Inherent), pValue.Inherent);
			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Penalty), pValue.Penalty);
			WriteProperty(pWriter, pSerializer, nameof(IAbilityScore.Temporary), pValue.Temporary);

			pWriter.WriteEndObject();
		}

		protected override IAbilityScore DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var abilityTypeToken = pJobject.SelectToken(nameof(IAbilityScore.Type));
			if (abilityTypeToken == null)
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IAbilityScore.Type)}");
			}
			var type = pSerializer.Deserialize<AbilityType>(abilityTypeToken.CreateReader());
			var baseValue = GetInt(pJobject, nameof(IAbilityScore.Base));
			var enhancedValue = GetInt(pJobject, nameof(IAbilityScore.Enhanced));
			var inherentValue = GetInt(pJobject, nameof(IAbilityScore.Inherent));

			return new AbilityScore(type, baseValue, enhancedValue, inherentValue);
		}
	}
}
