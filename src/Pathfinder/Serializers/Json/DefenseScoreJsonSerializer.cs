using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Serializers.Json
{
	public class DefenseScoreJsonSerializer : AbstractJsonSerializer<IDefenseScore>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IDefenseScore pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.Type), pValue.Type);
			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.Score), pValue.Score);

			if (pValue.Type == DefensiveType.CombatManeuverDefense)
			{
				WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.StrengthModifier), pValue.StrengthModifier);
				WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.BaseAttackBonus), pValue.BaseAttackBonus);
			}
			else
			{
				WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.ArmorBonus), pValue.ArmorBonus);
				WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.ShieldBonus), pValue.ShieldBonus);
			}

			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.DexterityModifier), pValue.DexterityModifier);
			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.SizeModifier), pValue.SizeModifier);
			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.DeflectBonus), pValue.DeflectBonus);
			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.DodgeBonus), pValue.DodgeBonus);
			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.NaturalBonus), pValue.NaturalBonus);
			WriteProperty(pWriter, pSerializer, nameof(IDefenseScore.TemporaryBonus), pValue.TemporaryBonus);

			pWriter.WriteEndObject();
		}

		protected override IDefenseScore DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException($"Deserialization of {nameof(IDefenseScore)} is not supported.");
		}
	}
}
