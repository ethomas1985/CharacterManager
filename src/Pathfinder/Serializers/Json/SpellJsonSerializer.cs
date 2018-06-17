using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class SpellJsonSerializer : AbstractJsonSerializer<ISpell>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ISpell pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ISpell.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(ISpell.School), pValue.School);
			WriteArrayProperty(pWriter, pSerializer, nameof(ISpell.SubSchools), pValue.SubSchools);

			WriteProperty(pWriter, pSerializer, nameof(ISpell.MagicDescriptors), pValue.MagicDescriptors);

			WriteProperty(pWriter, pSerializer, nameof(ISpell.SavingThrow), pValue.SavingThrow);
			WriteProperty(pWriter, pSerializer, nameof(ISpell.Description), pValue.Description);
			WriteProperty(pWriter, pSerializer, nameof(ISpell.HasSpellResistance), pValue.HasSpellResistance);
			WriteProperty(pWriter, pSerializer, nameof(ISpell.SpellResistance), pValue.SpellResistance);
			WriteProperty(pWriter, pSerializer, nameof(ISpell.CastingTime), pValue.CastingTime);
			WriteProperty(pWriter, pSerializer, nameof(ISpell.Range), pValue.Range);

			WriteProperty(pWriter, pSerializer, nameof(ISpell.LevelRequirements), pValue.LevelRequirements);

			WriteProperty(pWriter, pSerializer, nameof(ISpell.Duration), pValue.Duration);

			WriteArrayProperty(pWriter, pSerializer, nameof(ISpell.Components), pValue.Components);

			pWriter.WriteEndObject();
		}

		protected override ISpell DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(ISpell.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ISpell.Name)}");
			}

			var magicSchoolString = GetString(pJobject, nameof(ISpell.School));
			if (!Enum.TryParse(magicSchoolString, out MagicSchool magicSchool))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ISpell.School)}");
			}

			var magicSubSchools = GetValuesFromArray<MagicSubSchool>(pSerializer, pJobject, nameof(ISpell.SubSchools));

			var magicDescriptors = new HashSet<MagicDescriptor>(
				GetValuesFromArray<MagicDescriptor>(pSerializer, pJobject, nameof(ISpell.MagicDescriptors)));

			var savingThrow = GetString(pJobject, nameof(ISpell.SavingThrow));

			var description = GetString(pJobject, nameof(ISpell.Description));

			var hasSpellResistance = GetBoolean(pJobject, nameof(ISpell.HasSpellResistance));

			var spellResistance = GetString(pJobject, nameof(ISpell.SpellResistance));

			var castingTime = GetString(pJobject, nameof(ISpell.CastingTime));

			var range = GetString(pJobject, nameof(ISpell.Range));

			var levelRequirements = GetValuesFromHashObject<string, int>(pSerializer, pJobject, nameof(ISpell.LevelRequirements));

			var duration = GetString(pJobject, nameof(ISpell.Duration));

			var spellComponents = new HashSet<ISpellComponent>(
				GetValuesFromArray<ISpellComponent>(pSerializer, pJobject, nameof(ISpell.Components)));

			return new Spell(name, magicSchool, magicSubSchools, magicDescriptors, savingThrow, description, hasSpellResistance, spellResistance, castingTime, range, levelRequirements, duration, spellComponents);
		}
	}
}
