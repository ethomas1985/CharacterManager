using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class SkillJsonSerializer : AbstractJsonSerializer<ISkill>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ISkill pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ISkill.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.AbilityType), pValue.AbilityType);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.TrainedOnly), pValue.TrainedOnly);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.ArmorCheckPenalty), pValue.ArmorCheckPenalty);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.Description), pValue.Description);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.Check), pValue.Check);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.Action), pValue.Action);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.TryAgain), pValue.TryAgain);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.Special), pValue.Special);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.Restriction), pValue.Restriction);
			WriteProperty(pWriter, pSerializer, nameof(ISkill.Untrained), pValue.Untrained);

			pWriter.WriteEndObject();
		}

		protected override ISkill DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(ISkill.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ISkill.Name)}");
			}

			var abilityTypeString = GetString(pJobject, nameof(ISkill.AbilityType));
			if (!Enum.TryParse(abilityTypeString, out AbilityType abilityType))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ISkill.AbilityType)}");
			}

			var trainedOnly = GetBoolean(pJobject, nameof(ISkill.TrainedOnly));
			var armorCheckPenalty = GetBoolean(pJobject, nameof(ISkill.ArmorCheckPenalty));
			var description = GetString(pJobject, nameof(ISkill.Description));
			var check = GetString(pJobject, nameof(ISkill.Check));
			var action = GetString(pJobject, nameof(ISkill.Action));
			var tryAgain = GetString(pJobject, nameof(ISkill.TryAgain));
			var special = GetString(pJobject, nameof(ISkill.Special));
			var restriction = GetString(pJobject, nameof(ISkill.Restriction));
			var untrained = GetString(pJobject, nameof(ISkill.Untrained));

			return new Skill(name, abilityType, trainedOnly, armorCheckPenalty, description, check, action, tryAgain, special, restriction, untrained);
		}
	}
}
