using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class ClassLevelJsonSerializer : AbstractJsonSerializer<IClassLevel>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IClassLevel pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IClassLevel.Level), pValue.Level);

			WriteArrayProperty(pWriter, pSerializer, nameof(IClassLevel.BaseAttackBonus), pValue.BaseAttackBonus);

			WriteProperty(pWriter, pSerializer, nameof(IClassLevel.Fortitude), pValue.Fortitude);
			WriteProperty(pWriter, pSerializer, nameof(IClassLevel.Reflex), pValue.Reflex);
			WriteProperty(pWriter, pSerializer, nameof(IClassLevel.Will), pValue.Will);

			WriteArrayProperty(pWriter, pSerializer, nameof(IClassLevel.Specials), pValue.Specials);

			WriteSimpleDictionary(pWriter, pSerializer, nameof(IClassLevel.SpellsPerDay), pValue.SpellsPerDay, k => k.ToString(), v => v);

			WriteSimpleDictionary(pWriter, pSerializer, nameof(IClassLevel.SpellsKnown), pValue.SpellsKnown, k => k.ToString(), v => v);

			pWriter.WritePropertyName(nameof(IClassLevel.Spells));
			pWriter.WriteStartObject();
			if (pValue.Spells != null)
			{
				foreach (var keypair in pValue.Spells)
				{
					WriteArrayProperty(pWriter, pSerializer, keypair.Key.ToString(), keypair.Value);
				}
			}
			pWriter.WriteEndObject();

			pWriter.WriteEndObject();
		}

		protected override IClassLevel DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var level = GetInt(pJobject, nameof(IClassLevel.Level));

			var baseAttackBonus = GetValuesFromArray<int>(pSerializer, pJobject, nameof(IClassLevel.BaseAttackBonus));

			var fortitude = GetInt(pJobject, nameof(IClassLevel.Fortitude));
			var reflex = GetInt(pJobject, nameof(IClassLevel.Reflex));
			var will = GetInt(pJobject, nameof(IClassLevel.Will));

			var specials = GetValuesFromArray<string>(pSerializer, pJobject, nameof(IClassLevel.Specials));

			var spellsPerDay = GetValuesFromHashObject<int, int>(pSerializer, pJobject, nameof(IClassLevel.SpellsPerDay));

			var spellsKnown = GetValuesFromHashObject<int, int>(pSerializer, pJobject, nameof(IClassLevel.SpellsKnown));

			var spells = GetValuesFromHashObject<int, IEnumerable<string>>(
				pSerializer, pJobject, nameof(IClassLevel.Spells));

			return
				new ClassLevel(
					level,
					baseAttackBonus,
					fortitude,
					reflex,
					will,
					specials,
					spellsPerDay,
					spellsKnown,
					spells);
		}
	}
}
