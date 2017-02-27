using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Interface.Item;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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

			var jObject = JObject.Load(pReader);

			ICharacter character = new Character(SkillLibrary);

			character = ParseRace(jObject, character);
			character = ParseClasses(jObject, character);

			character = ParseAbilityScore(jObject, nameof(ICharacter.Strength), character.SetStrength) ?? character;
			character = ParseAbilityScore(jObject, nameof(ICharacter.Dexterity), character.SetDexterity) ?? character;
			character = ParseAbilityScore(jObject, nameof(ICharacter.Constitution), character.SetConstitution) ?? character;
			character = ParseAbilityScore(jObject, nameof(ICharacter.Intelligence), character.SetIntelligence) ?? character;
			character = ParseAbilityScore(jObject, nameof(ICharacter.Wisdom), character.SetWisdom) ?? character;
			character = ParseAbilityScore(jObject, nameof(ICharacter.Charisma), character.SetCharisma) ?? character;

			character = ParseName(jObject, character);
			character = ParseAlignment(jObject, character);
			character = ParseGender(jObject, character);

			character = ParseAge(jObject, character);
			character = ParseDeity(jObject, character);
			character = ParseEyes(jObject, character);
			character = ParseHair(jObject, character);
			character = ParseHeight(jObject, character);
			character = ParseWeight(jObject, character);
			character = ParseHomeland(jObject, character);

			character = ParseLanguages(jObject, character);

			character = ParseDamage(jObject, character);

			// TODO = Effects, etc


			character = ParseExperience(jObject, character);

			character = ParseCharacterPurse(jObject, character);

			character = ParseFeats(jObject, character);

			character = ParseInventory(jObject, character);

			return character;
		}

		private ICharacter ParseRace(JToken pJToken, ICharacter pCharacter)
		{
			var parsedRace = GetString(pJToken, nameof(ICharacter.Race));
			IRace race;
			if (parsedRace != null && RaceLibrary.TryGetValue(parsedRace, out race))
			{
				return pCharacter.SetRace(race);
			}
			return pCharacter;
		}

		private ICharacter ParseClasses(JToken pJToken, ICharacter pCharacter)
		{
			var tokens = pJToken.SelectTokens(nameof(ICharacter.Classes)).Children();

			var character = pCharacter;
			foreach (var token in tokens)
			{
				character = ParseCharacterClass(token, character);
			}

			return character;
		}

		private ICharacter ParseCharacterClass(JToken pToken, ICharacter pCharacter)
		{
			var className =  GetString(pToken, nameof(ICharacterClass.Class));

			var level = GetInt(pToken, nameof(ICharacterClass.Level));
			var isFavored = GetBoolean(pToken, nameof(ICharacterClass.IsFavored));

			var hitPointTokens = pToken.SelectToken(nameof(ICharacterClass.HitPoints));
			var hitPoints = hitPointTokens.Values<int>();

			var @class = ClassLibrary[className];

			pCharacter = pCharacter.AddClass(@class, level, isFavored, hitPoints);
			return pCharacter;
		}

		private static ICharacter ParseHomeland(JToken pJToken, ICharacter pCharacter)
		{
			var homeland = GetString(pJToken, nameof(ICharacter.Homeland));
			if (!string.IsNullOrWhiteSpace(homeland))
			{
				pCharacter = pCharacter.SetHomeland(homeland);
			}
			return pCharacter;
		}

		private static ICharacter ParseWeight(JToken pJToken, ICharacter pCharacter)
		{
			var weight = GetString(pJToken, nameof(ICharacter.Weight));
			if (!string.IsNullOrWhiteSpace(weight))
			{
				pCharacter = pCharacter.SetWeight(weight);
			}
			return pCharacter;
		}

		private static ICharacter ParseHeight(JToken pJToken, ICharacter pCharacter)
		{
			var height = GetString(pJToken, nameof(ICharacter.Height));
			if (!string.IsNullOrWhiteSpace(height))
			{
				pCharacter = pCharacter.SetHeight(height);
			}
			return pCharacter;
		}

		private static ICharacter ParseHair(JToken pJToken, ICharacter pCharacter)
		{
			var hair = GetString(pJToken, nameof(ICharacter.Hair));
			if (!string.IsNullOrWhiteSpace(hair))
			{
				pCharacter = pCharacter.SetHair(hair);
			}
			return pCharacter;
		}

		private static ICharacter ParseEyes(JToken pJToken, ICharacter pCharacter)
		{
			var eyes = GetString(pJToken, nameof(ICharacter.Eyes));
			if (!string.IsNullOrWhiteSpace(eyes))
			{
				pCharacter = pCharacter.SetEyes(eyes);
			}
			return pCharacter;
		}

		private static ICharacter ParseGender(JToken pJToken, ICharacter pCharacter)
		{
			var parsedGender = GetString(pJToken, nameof(ICharacter.Gender));
			Gender gender;
			if (Enum.TryParse(parsedGender, out gender))
			{
				pCharacter = pCharacter.SetGender(gender);
			}
			return pCharacter;
		}

		private static ICharacter ParseDeity(JToken pJToken, ICharacter pCharacter)
		{
			var title = pJToken.SelectToken($"{nameof(ICharacter.Deity)}.{nameof(IDeity.Name)}")?.ToString();
			if (!string.IsNullOrWhiteSpace(title))
			{
				pCharacter = pCharacter.SetDeity(new Deity(title));
			}
			return pCharacter;
		}

		private static ICharacter ParseAlignment(JToken pJToken, ICharacter pCharacter)
		{
			var parsedAlignment = GetString(pJToken, nameof(ICharacter.Alignment));
			Alignment alignment;
			if (Enum.TryParse(parsedAlignment, out alignment))
			{
				pCharacter = pCharacter.SetAlignment(alignment);
			}
			return pCharacter;
		}

		private static ICharacter ParseAge(JToken pJToken, ICharacter pCharacter)
		{
			var age = GetInt(pJToken, nameof(ICharacter.Age));
			return age > 0 ? pCharacter.SetAge(age) : pCharacter;
		}

		private static ICharacter ParseName(JToken pJToken, ICharacter pCharacter)
		{
			var name = GetString(pJToken, nameof(ICharacter.Name));
			if (!string.IsNullOrWhiteSpace(name))
			{
				pCharacter = pCharacter.SetName(name);
			}
			return pCharacter;
		}

		private static ICharacter ParseDamage(JToken pJToken, ICharacter pCharacter)
		{
			var damage = GetInt(pJToken, nameof(ICharacter.Damage));
			pCharacter = pCharacter.SetDamage(damage);
			return pCharacter;
		}

		private ICharacter ParseExperience(JToken pJToken, ICharacter pCharacter)
		{
			var tokens = pJToken.SelectTokens(nameof(ICharacter.Experience)).Children();

			var character = pCharacter;
			foreach (var token in tokens)
			{
				character = ParseEvent(token, character);
			}

			return character;
		}

		private ICharacter ParseEvent(JToken pToken, ICharacter pCharacter)
		{
			string title =  GetString(pToken, nameof(IEvent.Title));

			string description =  GetString(pToken, nameof(IEvent.Description));

			int xp;
			var xpText =  GetString(pToken, nameof(IEvent.ExperiencePoints));
			if (!int.TryParse(xpText, out xp))
			{
				xp = 0;
			}

			return pCharacter.AppendExperience(new Event(title, description, xp));
		}

		private ICharacter ParseCharacterPurse(JToken pJToken, ICharacter pCharacter)
		{
			var purseToken = pJToken.SelectToken(nameof(ICharacter.Purse));
			if (purseToken == null)
			{
				return pCharacter;
			}

			var purse = ParsePurse(purseToken);

			return pCharacter.SetPurse(purse.Copper.Value, purse.Silver.Value, purse.Gold.Value, purse.Platinum.Value);
		}

		private static IPurse ParsePurse(JToken pJToken)
		{
			if (pJToken == null)
			{
				return null;
			}

			var copper = (int) pJToken.SelectToken(nameof(IPurse.Copper));
			var silver = (int) pJToken.SelectToken(nameof(IPurse.Silver));
			var gold = (int) pJToken.SelectToken(nameof(IPurse.Gold));
			var platinum = (int) pJToken.SelectToken(nameof(IPurse.Platinum));

			var purse = new Purse(copper, silver, gold, platinum);
			return purse;
		}

		private ICharacter ParseFeats(JToken pJToken, ICharacter pCharacter)
		{
			var tokens = pJToken.SelectTokens(nameof(ICharacter.Feats)).Children();

			var character = pCharacter;
			foreach (var token in tokens)
			{
				character = ParseFeat(token, character);
			}

			return character;
		}
		private ICharacter ParseFeat(JToken pToken, ICharacter pCharacter)
		{
			string name =  GetString(pToken, nameof(IFeat.Name));

			var parsedAlignment = GetString(pToken, nameof(IFeat.FeatType));
			FeatType featType;
			if (!Enum.TryParse(parsedAlignment, out featType))
			{
				featType = FeatType.General;
			}

			string description = GetString(pToken, nameof(IFeat.Description));
			string benefit =  GetString(pToken, nameof(IFeat.Benefit));
			string special =  GetString(pToken, nameof(IFeat.Special));

			var prerequisites = pToken[nameof(IFeat.Prerequisites)]?.Children().Select(x => x.Value<string>());

			bool isSpecialized = GetBoolean(pToken, nameof(IFeat.IsSpecialized));
			string specialization = isSpecialized ? GetString(pToken, nameof(IFeat.Specialization)) : null;

			return pCharacter.AddFeat(new Feat(name, featType, prerequisites, description, benefit, special, isSpecialized), specialization);
		}

		private ICharacter ParseInventory(JToken pJToken, ICharacter pCharacter)
		{
			var tokens = pJToken.SelectTokens(nameof(ICharacter.Inventory)).Children();

			var character = pCharacter;
			foreach (var token in tokens)
			{
				character = ParseInventoryItem(token, character);
			}

			return character;
		}
		private ICharacter ParseInventoryItem(JToken pToken, ICharacter pCharacter)
		{
			var item = ParseItem(pToken[nameof(IInventoryItem.Item)]);

			var quantity = GetInt(pToken, nameof(IInventoryItem.Quantity));

			var character = pCharacter;
			for (int i = 0; i < quantity; i++)
			{
				character = character.AddToInventory(item);
			}

			return character;
		}
		private IItem ParseItem(JToken pToken)
		{
			string name = GetString(pToken, nameof(IItem.Name));

			var parsedAlignment = GetString(pToken, nameof(IItem.ItemType));
			ItemType itemType;
			if (!Enum.TryParse(parsedAlignment, out itemType))
			{
				itemType = ItemType.None;
			}

			string description = GetString(pToken, nameof(IItem.Description));
			string category = GetString(pToken, nameof(IItem.Category));
			var purse = ParsePurse(pToken[nameof(IItem.Cost)]);
			decimal weight = GetString(pToken, nameof(IItem.Weight)).AsDecimal();

			return new Item(name, itemType, category, purse, weight, description);
		}

		private ICharacter ParseLanguages(JToken pJToken, ICharacter pCharacter)
		{
			var tokens = pJToken.SelectTokens(nameof(ICharacter.Languages)).Values<string>();

			var character = pCharacter;
			foreach (var token in tokens)
			{
				if (string.IsNullOrWhiteSpace(token) || character.Languages.Any(x => token.Equals(x.Name)))
				{
					continue;
				}

				character = character.AddLanguage(new Language(token));
			}

			return character;
		}

		private ICharacter ParseAbilityScore(JObject pJToken, string pAbilityName, Func<int, int, int, ICharacter> pSetAbilityScore)
		{
			var token = pJToken.SelectToken(pAbilityName);
			if (token == null)
			{
				return null;
			}
			var baseValue = GetInt(token, nameof(IAbilityScore.Base));
			var enhancedValue = GetInt(token, nameof(IAbilityScore.Enhanced));
			var inherentValue = GetInt(token, nameof(IAbilityScore.Inherent));

			return pSetAbilityScore(baseValue, enhancedValue, inherentValue);
		}


		private static string GetString(JToken pJToken, string pField)
		{
			return pJToken.SelectToken(pField)?.ToString();
		}

		private static int GetInt(JToken pJToken, string pField)
		{
			var parsedValue = GetString(pJToken, pField);
			int value;
			return int.TryParse(parsedValue, out value) ? value : 0;
		}

		private static bool GetBoolean(JToken pJToken, string pField)
		{
			bool value;
			return bool.TryParse(GetString(pJToken, pField), out value) && value;
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

			_writeDeity(pWriter, character);

			_writeProperty(pWriter, nameof(ICharacter.Gender), character.Gender.ToString());
			_writeProperty(pWriter, nameof(ICharacter.Eyes), character.Eyes);
			_writeProperty(pWriter, nameof(ICharacter.Hair), character.Hair);
			_writeProperty(pWriter, nameof(ICharacter.Height), character.Height);
			_writeProperty(pWriter, nameof(ICharacter.Weight), character.Weight);
			_writeProperty(pWriter, nameof(ICharacter.Homeland), character.Homeland);
			_writeProperty(pWriter, nameof(ICharacter.Name), character.Name);
			_writeProperty(pWriter, nameof(ICharacter.Race), character.Race?.Name);
			_writeProperty(pWriter, nameof(ICharacter.BaseSize), character.BaseSize.ToString());
			_writeProperty(pWriter, nameof(ICharacter.Size), character.Size.ToString());

			_writeLanguages(pWriter, character);

			_writeProperty(pWriter, nameof(ICharacter.MaxHealthPoints), character.MaxHealthPoints);
			_writeProperty(pWriter, nameof(ICharacter.Damage), character.Damage);
			_writeProperty(pWriter, nameof(ICharacter.HealthPoints), character.HealthPoints);
			_writeProperty(pWriter, nameof(ICharacter.BaseSpeed), character.BaseSpeed);
			_writeProperty(pWriter, nameof(ICharacter.ArmoredSpeed), character.ArmoredSpeed);

			_writePurse(pWriter, nameof(ICharacter.Purse), character.Purse);

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

			_writeExperience(pWriter, character.Experience);

			_writeFeats(pWriter, character.Feats);

			_writeInventory(pWriter, character.Inventory);

			pWriter.WriteEndObject();
		}

		private void _writePurse(JsonWriter pWriter, string pPropertyName, IPurse pPurse)
		{
			if (pPurse == null)
			{
				return;
			}

			pWriter.WritePropertyName(pPropertyName);
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IPurse.Copper), pPurse.Copper.Value);
			_writeProperty(pWriter, nameof(IPurse.Silver), pPurse.Silver.Value);
			_writeProperty(pWriter, nameof(IPurse.Gold), pPurse.Gold.Value);
			_writeProperty(pWriter, nameof(IPurse.Platinum), pPurse.Platinum.Value);

			pWriter.WriteEndObject();
		}

		private static void _writeDeity(JsonWriter pWriter, ICharacter pCharacter)
		{
			if (pCharacter.Deity == null)
			{
				return;
			}

			pWriter.WritePropertyName(nameof(ICharacter.Deity));
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IDeity.Name), pCharacter.Deity.Name);

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

			if (pDefenseScore.Type == DefensiveType.CombatManeuverDefense)
			{
				_writeProperty(pWriter, nameof(IDefenseScore.StrengthModifier), pDefenseScore.StrengthModifier);
			}
			else
			{
				_writeProperty(pWriter, nameof(IDefenseScore.ShieldBonus), pDefenseScore.ShieldBonus);
			}

			if (pDefenseScore.Type == DefensiveType.CombatManeuverDefense)
			{
				_writeProperty(pWriter, nameof(IDefenseScore.BaseAttackBonus), pDefenseScore.BaseAttackBonus);
			}
			else
			{
				_writeProperty(pWriter, nameof(IDefenseScore.ArmorBonus), pDefenseScore.ArmorBonus);
			}


			_writeProperty(pWriter, nameof(IDefenseScore.DexterityModifier), pDefenseScore.DexterityModifier);
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

		private void _writeExperience(JsonWriter pWriter, IExperience pExperience)
		{
			pWriter.WritePropertyName(nameof(ICharacter.Experience));
			pWriter.WriteStartArray();

			foreach (var experienceEvent in pExperience)
			{
				_writeEvent(pWriter, experienceEvent);
			}

			pWriter.WriteEndArray();
		}

		private void _writeEvent(JsonWriter pWriter, IEvent pExperienceEvent)
		{
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IEvent.Title), pExperienceEvent.Title);
			_writeProperty(pWriter, nameof(IEvent.Description), pExperienceEvent.Description);
			_writeProperty(pWriter, nameof(IEvent.ExperiencePoints), pExperienceEvent.ExperiencePoints);

			pWriter.WriteEndObject();
		}

		private void _writeFeats(JsonWriter pWriter, IEnumerable<IFeat> pFeats)
		{
			pWriter.WritePropertyName(nameof(ICharacter.Feats));
			pWriter.WriteStartArray();

			foreach (var feat in pFeats)
			{
				_writeFeat(pWriter, feat);
			}

			pWriter.WriteEndArray();
		}

		private void _writeFeat(JsonWriter pWriter, IFeat pFeat)
		{
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IFeat.Name), pFeat.Name);
			_writeProperty(pWriter, nameof(IFeat.FeatType), pFeat.FeatType.ToString());
			_writeProperty(pWriter, nameof(IFeat.IsSpecialized), pFeat.IsSpecialized.ToString());

			if (pFeat.IsSpecialized)
			{
				_writeProperty(pWriter, nameof(IFeat.Specialization), pFeat.Specialization);
			}

			_writeProperty(pWriter, nameof(IFeat.Description), pFeat.Description);
			_writeProperty(pWriter, nameof(IFeat.Benefit), pFeat.Benefit);
			_writeProperty(pWriter, nameof(IFeat.Special), pFeat.Special);

			pWriter.WritePropertyName(nameof(IFeat.Prerequisites));
			pWriter.WriteStartArray();

			foreach (var prerequisites in pFeat.Prerequisites)
			{
				pWriter.WriteValue(prerequisites);
			}

			pWriter.WriteEndArray();

			pWriter.WriteEndObject();
		}

		private void _writeInventory(JsonWriter pWriter, IInventory pInventory)
		{
			pWriter.WritePropertyName(nameof(ICharacter.Inventory));
			pWriter.WriteStartArray();

			foreach (var feat in pInventory)
			{
				_writeInventoryItem(pWriter, feat);
			}

			pWriter.WriteEndArray();
		}

		private void _writeInventoryItem(JsonWriter pWriter, IInventoryItem pInventoryItem)
		{
			pWriter.WriteStartObject();

			_writeItem(pWriter, pInventoryItem.Item);
			_writeProperty(pWriter, nameof(IInventoryItem.Quantity), pInventoryItem.Quantity);

			pWriter.WriteEndObject();
		}

		private void _writeItem(JsonWriter pWriter, IItem pItem)
		{
			pWriter.WritePropertyName(nameof(IInventoryItem.Item));
			pWriter.WriteStartObject();

			_writeProperty(pWriter, nameof(IItem.Name), pItem.Name);
			_writeProperty(pWriter, nameof(IItem.ItemType), pItem.ItemType.ToString());
			_writeProperty(pWriter, nameof(IItem.Category), pItem.Category);
			_writeProperty(pWriter, nameof(IItem.Description), pItem.Description);
			_writeProperty(pWriter, nameof(IItem.Weight), pItem.Weight);
			_writePurse(pWriter, nameof(IItem.Cost), pItem.Cost);

			pWriter.WriteEndObject();
		}

		private static void _writeProperty(JsonWriter pWriter, string pName, object pValue)
		{
			if (pValue == null)
			{
				return;
			}

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
