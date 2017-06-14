using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;

namespace Pathfinder.Serializers.Json
{
	public class SkillScoreJsonSerializer : AbstractJsonSerializer<ISkillScore>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ISkillScore pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ISkillScore.Skill), pValue.Skill.Name);
			WriteProperty(pWriter, pSerializer, nameof(ISkillScore.Ability), pValue.Ability);
			WriteProperty(pWriter, pSerializer, nameof(ISkillScore.AbilityModifier), pValue.AbilityModifier);
			WriteProperty(pWriter, pSerializer, nameof(ISkillScore.Ranks), pValue.Ranks);
			WriteProperty(pWriter, pSerializer, nameof(ISkillScore.ClassModifier), pValue.ClassModifier);
			WriteProperty(pWriter, pSerializer, nameof(ISkillScore.MiscModifier), pValue.MiscModifier);
			WriteProperty(pWriter, pSerializer, nameof(ISkillScore.ArmorClassPenalty), pValue.ArmorClassPenalty);

			pWriter.WriteEndObject();
		}

		protected override ISkillScore DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException($"Deserializing {nameof(ISkillScore)} types is not supported.");
		}
	}
}
