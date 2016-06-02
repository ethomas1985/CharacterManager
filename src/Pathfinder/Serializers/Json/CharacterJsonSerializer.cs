using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class CharacterJsonSerializer : JsonConverter
	{
		public CharacterJsonSerializer(
			ILibrary<IRace> pRaceLibrary,
			ILibrary<ISkill> pSkillLibrary,
			ILibrary<IClass> pClassLibrary)
		{
			RaceLibrary = pRaceLibrary;
			SkillLibrary = pSkillLibrary;
			ClassLibrary = pClassLibrary;
		}

		public ILibrary<IRace> RaceLibrary { get; }
		public ILibrary<ISkill> SkillLibrary { get; }
		public ILibrary<IClass> ClassLibrary { get; }

		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override bool CanConvert(Type pObjectType)
		{
			//return pObjectType.IsAssignableFrom(typeof(ICharacter));
			return typeof(ICharacter).IsAssignableFrom(pObjectType);
		}

		public override object ReadJson(
			JsonReader pReader,
			Type pObjectType,
			object pExistingValue,
			JsonSerializer pSerializer)
		{
			if (!CanConvert(pObjectType))
			{
				throw new ArgumentException($"{nameof(pObjectType)} MUST be an implementation of {nameof(ICharacter)}");
			}

			ICharacter character = new Character(SkillLibrary);

			var jObject = JObject.Load(pReader);

			var name = getString(jObject, nameof(ICharacter.Name));
			if (!string.IsNullOrWhiteSpace(name))
			{
				character = character.SetName(name);
			}

			var age = getInt(jObject, nameof(ICharacter.Age));
			character = character.SetAge(age);

			var parsedAlignment = getString(jObject, nameof(ICharacter.Alignment));
			Alignment alignment;
			if (Enum.TryParse(parsedAlignment, out alignment))
			{
				character = character.SetAlignment(alignment);
			}

			var parsedDeity = getString(jObject, nameof(ICharacter.Deity));
			if (!string.IsNullOrWhiteSpace(parsedDeity))
			{
				character = character.SetDeity(new Deity(parsedDeity));
			}

			var parsedGender = getString(jObject, nameof(ICharacter.Gender));
			Gender gender;
			if (Enum.TryParse(parsedGender, out gender))
			{
				character = character.SetGender(gender);
			}

			var eyes = getString(jObject, nameof(ICharacter.Eyes));
			if (!string.IsNullOrWhiteSpace(eyes))
			{
				character = character.SetEyes(eyes);
			}

			var hair = getString(jObject, nameof(ICharacter.Hair));
			if (!string.IsNullOrWhiteSpace(hair))
			{
				character = character.SetHair(hair);
			}

			var height = getString(jObject, nameof(ICharacter.Height));
			if (!string.IsNullOrWhiteSpace(height))
			{
				character = character.SetHeight(height);
			}

			var weight = getString(jObject, nameof(ICharacter.Weight));
			if (!string.IsNullOrWhiteSpace(weight))
			{
				character = character.SetWeight(weight);
			}

			var homeland = getString(jObject, nameof(ICharacter.Homeland));
			if (!string.IsNullOrWhiteSpace(homeland))
			{
				character = character.SetHomeland(homeland);
			}

			var parsedRace = getString(jObject, nameof(ICharacter.Race));
			IRace race;
			if (parsedRace != null && RaceLibrary.TryGetValue(parsedRace, out race))
			{
				character = character.SetRace(race);
			}

			//Todo: Parse non-starting languages
			//var languages = getString(jObject, nameof(ICharacter.Languages));
			//character = character.AddLanguages(name);

			var damage = getInt(jObject, nameof(ICharacter.Damage));
			character = character.SetDamage(damage);

			character = parseAbilityScore(jObject, nameof(ICharacter.Strength), character.SetStrength);
			character = parseAbilityScore(jObject, nameof(ICharacter.Dexterity), character.SetDexterity);
			character = parseAbilityScore(jObject, nameof(ICharacter.Constitution), character.SetConstitution);
			character = parseAbilityScore(jObject, nameof(ICharacter.Intelligence), character.SetIntelligence);
			character = parseAbilityScore(jObject, nameof(ICharacter.Wisdom), character.SetWisdom);
			character = parseAbilityScore(jObject, nameof(ICharacter.Charisma), character.SetCharisma);

			// TODO = DefensiveScores, OffensiveScores, SavingThrows, Effects, Classes, etc

			character = parseClasses(jObject, character, nameof(ICharacter.Classes));

			return character;
		}

		protected ICharacter parseAbilityScore(JObject pJObject, string pAbilityName, Func<int, int, int, ICharacter> pSetAbilityScore)
		{
			var baseValue = getInt(pJObject, $"{pAbilityName}.{nameof(IAbilityScore.Base)}");
			var enhancedValue = getInt(pJObject, $"{pAbilityName}.{nameof(IAbilityScore.Enhanced)}");
			var inherentValue = getInt(pJObject, $"{pAbilityName}.{nameof(IAbilityScore.Inherent)}");

			return pSetAbilityScore(baseValue, enhancedValue, inherentValue);
		}

		protected ICharacter parseClasses(JObject pJObject, ICharacter pCharacter, string pFieldName)
		{
			var tokens = pJObject.SelectTokens(pFieldName).Children();

			var character = pCharacter;
			foreach (var token in tokens)
			{
				var className = token.SelectToken(nameof(ICharacterClass.Class))?.ToString();

				var levelText = token.SelectToken(nameof(ICharacterClass.Level))?.ToString();
				int level;
				if (!int.TryParse(levelText, out level))
				{
					level = 0;
				}

				var isFavoredText = token.SelectToken(nameof(ICharacterClass.IsFavored))?.ToString();
				bool isFavored;
				if (!bool.TryParse(isFavoredText, out isFavored))
				{
					isFavored = false;
				}

				var hitPointTokens = token.SelectToken(nameof(ICharacterClass.HitPoints));
				var hitPoints = hitPointTokens.Values<int>();

				var @class = ClassLibrary[className];

				character = character.AddClass(@class, level, isFavored, hitPoints);
			}

			return character;
		}

		protected static string getString(JObject pJObject, string pField)
		{
			return pJObject.SelectToken(pField)?.ToString();
		}

		protected static int getInt(JObject pJObject, string pField)
		{
			var parsedValue = pJObject.SelectToken(pField).ToString();
			int value;
			return int.TryParse(parsedValue, out value) ? value : 0;
		}

		protected static string getStringFor(JObject pJObject, string pField, string pValue)
		{
			var section = pJObject["sections"].Children().Where(x => x[pField] != null && ((string) x[pField]).Equals(pValue));
			return section.Select(x => (string) x["body"]).FirstOrDefault();
		}

		protected static bool getBoolean(JObject pJObject, string pField)
		{
			bool value;
			return bool.TryParse((string) pJObject[pField], out value) && value;
		}

		public override void WriteJson(
			JsonWriter pWriter,
			object pValue,
			JsonSerializer pSerializer)
		{
			var character = pValue as ICharacter;
			Assert.IsTrue(character != null, $"{nameof(pValue)} was not an instance of {nameof(ICharacter)}");

			pWriter.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
			pWriter.Formatting = Formatting.Indented;
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(ICharacter.Age), character.Age);
			_writeProperty(pWriter, nameof(ICharacter.Alignment), character.Alignment.ToString());

			if (character.Deity != null)
			{
				_writeProperty(pWriter, nameof(ICharacter.Deity), character.Deity.ToString());
			}

			_writeProperty(pWriter, nameof(ICharacter.Gender), character.Gender.ToString());

			if (!string.IsNullOrEmpty(character.Eyes))
			{
				_writeProperty(pWriter, nameof(ICharacter.Eyes), character.Eyes);
			}

			if (!string.IsNullOrEmpty(character.Hair))
			{
				_writeProperty(pWriter, nameof(ICharacter.Hair), character.Hair);
			}

			if (!string.IsNullOrEmpty(character.Height))
			{
				_writeProperty(pWriter, nameof(ICharacter.Height), character.Height);
			}

			if (!string.IsNullOrEmpty(character.Weight))
			{
				_writeProperty(pWriter, nameof(ICharacter.Weight), character.Weight);
			}

			if (!string.IsNullOrEmpty(character.Homeland))
			{
				_writeProperty(pWriter, nameof(ICharacter.Homeland), character.Homeland);
			}

			if (!string.IsNullOrEmpty(character.Name))
			{
				_writeProperty(pWriter, nameof(ICharacter.Name), character.Name);
			}

			if (character.Race != null)
			{
				_writeProperty(pWriter, nameof(ICharacter.Race), character.Race.Name);
			}

			_writeProperty(pWriter, nameof(ICharacter.BaseSize), character.BaseSize.ToString());
			_writeProperty(pWriter, nameof(ICharacter.Size), character.Size.ToString());

			_writeLanguages(pWriter, character);

			_writeProperty(pWriter, nameof(ICharacter.MaxHealthPoints), character.MaxHealthPoints);
			_writeProperty(pWriter, nameof(ICharacter.Damage), character.Damage);
			_writeProperty(pWriter, nameof(ICharacter.HealthPoints), character.HealthPoints);
			_writeProperty(pWriter, nameof(ICharacter.BaseSpeed), character.BaseSpeed);
			_writeProperty(pWriter, nameof(ICharacter.ArmoredSpeed), character.ArmoredSpeed);

			_writeProperty(pWriter, nameof(ICharacter.Initiative), character.Initiative);

			_writeCharacterClasses(pWriter, character.Classes, nameof(ICharacter.Classes));

			_writeAbilityScore(pWriter, character.Strength, nameof(ICharacter.Strength));
			_writeAbilityScore(pWriter, character.Dexterity, nameof(ICharacter.Dexterity));
			_writeAbilityScore(pWriter, character.Constitution, nameof(ICharacter.Constitution));
			_writeAbilityScore(pWriter, character.Intelligence, nameof(ICharacter.Intelligence));
			_writeAbilityScore(pWriter, character.Wisdom, nameof(ICharacter.Wisdom));
			_writeAbilityScore(pWriter, character.Charisma, nameof(ICharacter.Charisma));

			_writeDefenseScore(pWriter, character.ArmorClass, nameof(ICharacter.ArmorClass));
			_writeDefenseScore(pWriter, character.FlatFooted, nameof(ICharacter.FlatFooted));
			_writeDefenseScore(pWriter, character.Touch, nameof(ICharacter.Touch));
			_writeDefenseScore(pWriter, character.CombatManeuverDefense, nameof(ICharacter.CombatManeuverDefense));

			_writeSavingThrow(pWriter, character.Fortitude, nameof(ICharacter.Fortitude));
			_writeSavingThrow(pWriter, character.Reflex, nameof(ICharacter.Reflex));
			_writeSavingThrow(pWriter, character.Will, nameof(ICharacter.Will));

			_writeOffensiveScore(pWriter, character.Melee, nameof(ICharacter.Melee));
			_writeOffensiveScore(pWriter, character.Ranged, nameof(ICharacter.Ranged));
			_writeOffensiveScore(pWriter, character.CombatManeuverBonus, nameof(ICharacter.CombatManeuverBonus));

			_writeSkillScores(pWriter, character.SkillScores, nameof(ICharacter.SkillScores));

			pWriter.WriteEndObject();
		}

		private static void _writeLanguages(JsonWriter pWriter, ICharacter pCharacter)
		{
			if (pCharacter.Languages == null)
			{
				return;
			}

			pWriter.WritePropertyName(nameof(ICharacter.Languages));

			pWriter.WriteStartArray();
			foreach (var language in pCharacter.Languages)
			{
				pWriter.WriteValue(language.ToString());
			}
			pWriter.WriteEndArray();

		}

		private void _writeCharacterClasses(JsonWriter pWriter, IEnumerable<ICharacterClass> pCharacterClasses, string pPropertyName)
		{
			//TODO : Classes
			pWriter.WritePropertyName(pPropertyName);
			pWriter.WriteStartArray();

			foreach (var characterClass in pCharacterClasses)
			{
				pWriter.WriteStartObject();

				_writeProperty(pWriter, nameof(ICharacterClass.Class), characterClass.Class.Name);
				_writeProperty(pWriter, nameof(ICharacterClass.Level), characterClass.Level);
				_writeProperty(pWriter, nameof(ICharacterClass.IsFavored), characterClass.IsFavored);

				_writeProperty(pWriter, nameof(ICharacterClass.BaseAttackBonus), characterClass.BaseAttackBonus);
				_writeProperty(pWriter, nameof(ICharacterClass.Fortitude), characterClass.Fortitude);
				_writeProperty(pWriter, nameof(ICharacterClass.Reflex), characterClass.Reflex);
				_writeProperty(pWriter, nameof(ICharacterClass.Will), characterClass.Will);

				pWriter.WritePropertyName(nameof(ICharacterClass.HitPoints));
				_writeSimpleArray(pWriter, characterClass.HitPoints.ToArray());

				pWriter.WriteEndObject();
			}

			pWriter.WriteEndArray();
		}

		private static void _writeAbilityScore(JsonWriter pWriter, IAbilityScore pAbilityScore, string pPropertyName)
		{
			pWriter.WritePropertyName(pPropertyName);
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IAbilityScore.Type), pAbilityScore.Type.ToString());
			_writeProperty(pWriter, nameof(IAbilityScore.Score), pAbilityScore.Score);
			_writeProperty(pWriter, nameof(IAbilityScore.Modifier), pAbilityScore.Modifier);
			_writeProperty(pWriter, nameof(IAbilityScore.Base), pAbilityScore.Base);
			_writeProperty(pWriter, nameof(IAbilityScore.Enhanced), pAbilityScore.Enhanced);
			_writeProperty(pWriter, nameof(IAbilityScore.Inherent), pAbilityScore.Inherent);
			_writeProperty(pWriter, nameof(IAbilityScore.Penalty), pAbilityScore.Penalty);
			_writeProperty(pWriter, nameof(IAbilityScore.Temporary), pAbilityScore.Temporary);

			pWriter.WriteEndObject();
		}

		private static void _writeDefenseScore(JsonWriter pWriter, IDefenseScore pDefenseScore, string pPropertyName)
		{
			pWriter.WritePropertyName(pPropertyName);
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IDefenseScore.Type), pDefenseScore.Type.ToString());
			_writeProperty(pWriter, nameof(IDefenseScore.Score), pDefenseScore.Score);
			_writeProperty(pWriter, nameof(IDefenseScore.ArmorBonus), pDefenseScore.ArmorBonus);
			_writeProperty(pWriter, nameof(IDefenseScore.ShieldBonus), pDefenseScore.ShieldBonus);
			_writeProperty(pWriter, nameof(IDefenseScore.DexterityModifier), pDefenseScore.DexterityModifier);
			_writeProperty(pWriter, nameof(IDefenseScore.StrengthModifier), pDefenseScore.StrengthModifier);
			_writeProperty(pWriter, nameof(IDefenseScore.BaseAttackBonus), pDefenseScore.BaseAttackBonus);
			_writeProperty(pWriter, nameof(IDefenseScore.SizeModifier), pDefenseScore.SizeModifier);
			_writeProperty(pWriter, nameof(IDefenseScore.DeflectBonus), pDefenseScore.DeflectBonus);
			_writeProperty(pWriter, nameof(IDefenseScore.DodgeBonus), pDefenseScore.DodgeBonus);
			_writeProperty(pWriter, nameof(IDefenseScore.MiscellaneousBonus), pDefenseScore.MiscellaneousBonus);
			_writeProperty(pWriter, nameof(IDefenseScore.NaturalBonus), pDefenseScore.NaturalBonus);
			_writeProperty(pWriter, nameof(IDefenseScore.TemporaryBonus), pDefenseScore.TemporaryBonus);

			pWriter.WriteEndObject();
		}

		private static void _writeSavingThrow(JsonWriter pWriter, ISavingThrow pSavingThrow, string pPropertyName)
		{
			pWriter.WritePropertyName(pPropertyName);
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(ISavingThrow.Type), pSavingThrow.Type.ToString());
			_writeProperty(pWriter, nameof(ISavingThrow.Score), pSavingThrow.Score);
			_writeProperty(pWriter, nameof(ISavingThrow.Ability), pSavingThrow.Ability);
			_writeProperty(pWriter, nameof(ISavingThrow.Base), pSavingThrow.Base);
			_writeProperty(pWriter, nameof(ISavingThrow.AbilityModifier), pSavingThrow.AbilityModifier);
			_writeProperty(pWriter, nameof(ISavingThrow.Resist), pSavingThrow.Resist);
			_writeProperty(pWriter, nameof(ISavingThrow.Misc), pSavingThrow.Misc);
			_writeProperty(pWriter, nameof(ISavingThrow.Temporary), pSavingThrow.Temporary);

			pWriter.WriteEndObject();
		}

		private static void _writeOffensiveScore(JsonWriter pWriter, IOffensiveScore pOffensiveScore, string pPropertyName)
		{
			pWriter.WritePropertyName(pPropertyName);
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IOffensiveScore.Type), pOffensiveScore.Type.ToString());
			_writeProperty(pWriter, nameof(IOffensiveScore.Score), pOffensiveScore.Score);
			_writeProperty(pWriter, nameof(IOffensiveScore.BaseAttackBonus), pOffensiveScore.BaseAttackBonus);
			_writeProperty(pWriter, nameof(IOffensiveScore.AbilityModifier), pOffensiveScore.AbilityModifier);
			_writeProperty(pWriter, nameof(IOffensiveScore.SizeModifier), pOffensiveScore.SizeModifier);
			_writeProperty(pWriter, nameof(IOffensiveScore.MiscModifier), pOffensiveScore.MiscModifier);
			_writeProperty(pWriter, nameof(IOffensiveScore.TemporaryModifier), pOffensiveScore.TemporaryModifier);

			pWriter.WriteEndObject();
		}

		private static void _writeSkillScores(JsonWriter pWriter, IEnumerable<ISkillScore> pSkillsScores, string pPropertyName)
		{
			pWriter.WritePropertyName(pPropertyName);
			pWriter.WriteStartArray();

			foreach (var skillScore in pSkillsScores)
			{
				_writeSkillScore(pWriter, skillScore);
			}

			pWriter.WriteEndArray();
		}

		private static void _writeSkillScore(JsonWriter pWriter, ISkillScore pSkillScore)
		{
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(ISkillScore.Ability), pSkillScore.Ability.ToString());
			_writeProperty(pWriter, nameof(ISkillScore.Ranks), pSkillScore.Ranks);
			_writeProperty(pWriter, nameof(ISkillScore.AbilityModifier), pSkillScore.AbilityModifier);
			_writeProperty(pWriter, nameof(ISkillScore.ClassModifier), pSkillScore.ClassModifier);
			_writeProperty(pWriter, nameof(ISkillScore.MiscModifier), pSkillScore.MiscModifier);
			_writeProperty(pWriter, nameof(ISkillScore.TemporaryModifier), pSkillScore.TemporaryModifier);
			_writeProperty(pWriter, nameof(ISkillScore.ArmorClassPenalty), pSkillScore.ArmorClassPenalty);
			_writeProperty(pWriter, nameof(ISkillScore.Total), pSkillScore.Total);

			_writeSkill(pWriter, pSkillScore.Skill);

			pWriter.WriteEndObject();
		}

		private static void _writeSkill(JsonWriter pWriter, ISkill pSkill)
		{
			pWriter.WritePropertyName(nameof(ISkillScore.Skill));
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(ISkill.Name), pSkill.Name);
			_writeProperty(pWriter, nameof(ISkill.AbilityType), pSkill.AbilityType.ToString());
			_writeProperty(pWriter, nameof(ISkill.TrainedOnly), pSkill.TrainedOnly);
			_writeProperty(pWriter, nameof(ISkill.ArmorCheckPenalty), pSkill.ArmorCheckPenalty);

			pWriter.WriteEndObject();
		}

		private static void _writeProperty(JsonWriter pWriter, string pName, object pValue)
		{
			pWriter.WritePropertyName(pName);
			pWriter.WriteValue(pValue);
		}

		private static void _writeSimpleArray<T>(JsonWriter pWriter, params T[] pValues)
		{
			pWriter.WriteStartArray();
			_writeValues(pWriter, pValues.ToArray());
			pWriter.WriteEndArray();
		}

		private static void _writeValues<T>(JsonWriter pWriter, params T[] pValues)
		{
			foreach (var value in pValues)
			{
				pWriter.WriteValue(value);
			}
		}
	}
}
