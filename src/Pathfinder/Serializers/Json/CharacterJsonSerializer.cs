using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Currency;
using Pathfinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Interface.Item;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class CharacterJsonSerializer : AbstractJsonSerializer<ICharacter>
	{
		public CharacterJsonSerializer(
			ILibrary<IRace> pRaceLibrary,
			ILibrary<ISkill> pSkillLibrary)
		{
			RaceLibrary = pRaceLibrary;
			SkillLibrary = pSkillLibrary;
		}

		public ILibrary<IRace> RaceLibrary { get; }
		public ILibrary<ISkill> SkillLibrary { get; }

		protected override ICharacter DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			ICharacter character = new Character(SkillLibrary);

			character = ParseRace(RaceLibrary, pJobject, character);

			character = ParseClasses(pSerializer, pJobject, character);

			var strengthToken = pJobject.SelectToken(nameof(ICharacter.Strength));
			if (strengthToken != null)
			{
				var strengthAbilityScore = pSerializer.Deserialize<IAbilityScore>(strengthToken.CreateReader());
				character =
					character.SetStrength(strengthAbilityScore.Base, strengthAbilityScore.Enhanced, strengthAbilityScore.Inherent);
			}

			var dexterityToken = pJobject.SelectToken(nameof(ICharacter.Dexterity));
			if (dexterityToken != null)
			{
				var dexterityAbilityScore = pSerializer.Deserialize<IAbilityScore>(dexterityToken.CreateReader());
				character =
					character.SetDexterity(dexterityAbilityScore.Base, dexterityAbilityScore.Enhanced, dexterityAbilityScore.Inherent);
			}

			var constitutionToken = pJobject.SelectToken(nameof(ICharacter.Constitution));
			if (constitutionToken != null)
			{
				var constitutionAbilityScore = pSerializer.Deserialize<IAbilityScore>(constitutionToken.CreateReader());
				character =
					character.SetConstitution(constitutionAbilityScore.Base, constitutionAbilityScore.Enhanced, constitutionAbilityScore.Inherent);
			}

			var intelligenceToken = pJobject.SelectToken(nameof(ICharacter.Intelligence));
			if (intelligenceToken != null)
			{
				var intelligenceAbilityScore = pSerializer.Deserialize<IAbilityScore>(intelligenceToken.CreateReader());
				character =
					character.SetIntelligence(intelligenceAbilityScore.Base, intelligenceAbilityScore.Enhanced, intelligenceAbilityScore.Inherent);
			}

			var wisdomToken = pJobject.SelectToken(nameof(ICharacter.Wisdom));
			if (wisdomToken != null)
			{
				var wisdomAbilityScore = pSerializer.Deserialize<IAbilityScore>(wisdomToken.CreateReader());
				character =
					character.SetWisdom(wisdomAbilityScore.Base, wisdomAbilityScore.Enhanced, wisdomAbilityScore.Inherent);
			}

			var charismaToken = pJobject.SelectToken(nameof(ICharacter.Charisma));
			if (charismaToken != null)
			{
				var charismaAbilityScore = pSerializer.Deserialize<IAbilityScore>(charismaToken.CreateReader());
				character =
					character.SetCharisma(charismaAbilityScore.Base, charismaAbilityScore.Enhanced, charismaAbilityScore.Inherent);
			}

			character = ParseName(pJobject, character);
			character = ParseAlignment(pJobject, character);
			character = ParseGender(pJobject, character);

			character = ParseAge(pJobject, character);
			character = ParseDeity(pJobject, character);
			character = ParseEyes(pJobject, character);
			character = ParseHair(pJobject, character);
			character = ParseHeight(pJobject, character);
			character = ParseWeight(pJobject, character);
			character = ParseHomeland(pJobject, character);

			character = ParseLanguages(pSerializer, pJobject, character);

			character = ParseDamage(pJobject, character);

			// TODO = Effects, etc

			character = ParseExperience(pSerializer, pJobject, character);

			character = ParseCharacterPurse(pSerializer, pJobject, character);

			character = ParseFeats(pSerializer, pJobject, character);

			character = ParseSkills(SkillLibrary, pJobject, character);

			character = ParseInventory(pSerializer, pJobject, character);

			character = ParseEquipedArmor(pSerializer, pJobject, character);

			return character;
		}

		private static ICharacter ParseRace(ILibrary<IRace> pRaceLibrary, JToken pJToken, ICharacter pCharacter)
		{
			var parsedRace = GetString(pJToken, nameof(ICharacter.Race));
			if (parsedRace != null && pRaceLibrary.TryGetValue(parsedRace, out IRace race))
			{
				return pCharacter.SetRace(race);
			}
			return pCharacter;
		}

		private static ICharacter ParseClasses(JsonSerializer pSerializer, JToken pJToken, ICharacter pCharacter)
		{
			return pJToken
				.SelectTokens(nameof(ICharacter.Classes))
				.Children()
				.Select(x => pSerializer.Deserialize<ICharacterClass>(x.CreateReader()))
				.Aggregate(pCharacter, (character, characterClass) => pCharacter.AddClass(characterClass.Class, characterClass.Level, characterClass.IsFavored, characterClass.HitPoints));
		}

		private static ICharacter ParseHomeland(JToken pJToken, ICharacter pCharacter)
		{
			var homeland = GetString(pJToken, nameof(ICharacter.Homeland));
			return !string.IsNullOrWhiteSpace(homeland)
				? pCharacter.SetHomeland(homeland)
				: pCharacter;
		}

		private static ICharacter ParseWeight(JToken pJToken, ICharacter pCharacter)
		{
			var weight = GetString(pJToken, nameof(ICharacter.Weight));
			return !string.IsNullOrWhiteSpace(weight)
				? pCharacter.SetWeight(weight)
				: pCharacter;
		}

		private static ICharacter ParseHeight(JToken pJToken, ICharacter pCharacter)
		{
			var height = GetString(pJToken, nameof(ICharacter.Height));
			return !string.IsNullOrWhiteSpace(height)
				? pCharacter.SetHeight(height)
				: pCharacter;
		}

		private static ICharacter ParseHair(JToken pJToken, ICharacter pCharacter)
		{
			var hair = GetString(pJToken, nameof(ICharacter.Hair));
			return !string.IsNullOrWhiteSpace(hair)
				? pCharacter.SetHair(hair)
				: pCharacter;
		}

		private static ICharacter ParseEyes(JToken pJToken, ICharacter pCharacter)
		{
			var eyes = GetString(pJToken, nameof(ICharacter.Eyes));
			return !string.IsNullOrWhiteSpace(eyes)
				? pCharacter.SetEyes(eyes)
				: pCharacter;
		}

		private static ICharacter ParseGender(JToken pJToken, ICharacter pCharacter)
		{
			var parsedGender = GetString(pJToken, nameof(ICharacter.Gender));
			if (string.IsNullOrWhiteSpace(parsedGender))
			{
				return pCharacter;
			}

			if (!Enum.TryParse(parsedGender.ToPascalCase(), out Gender gender))
			{
				throw new JsonException($"Invalid value for attribute {nameof(ICharacter.Gender)}: '{parsedGender}'");
			}
			pCharacter = pCharacter.SetGender(gender);
			return pCharacter;
		}

		private static ICharacter ParseDeity(JToken pJToken, ICharacter pCharacter)
		{
			var title = pJToken.SelectToken($"{nameof(ICharacter.Deity)}.{nameof(IDeity.Name)}")?.ToString();
			return !string.IsNullOrWhiteSpace(title)
				? pCharacter.SetDeity(new Deity(title))
				: pCharacter;
		}

		private static ICharacter ParseAlignment(JToken pJToken, ICharacter pCharacter)
		{
			var parsedAlignment = GetString(pJToken, nameof(ICharacter.Alignment));
			if (string.IsNullOrWhiteSpace(parsedAlignment))
			{
				return pCharacter;
			}

			if (!Enum.TryParse(parsedAlignment.ToPascalCase(), out Alignment alignment))
			{
				throw new JsonException($"Invalid value for attribute {nameof(ICharacter.Alignment)}: '{parsedAlignment}'");
			}
			pCharacter = pCharacter.SetAlignment(alignment);
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
			return !string.IsNullOrWhiteSpace(name)
				? pCharacter.SetName(name)
				: pCharacter;
		}

		private static ICharacter ParseDamage(JToken pJToken, ICharacter pCharacter)
		{
			var damage = GetInt(pJToken, nameof(ICharacter.Damage));
			pCharacter = pCharacter.SetDamage(damage);
			return pCharacter;
		}

		private static ICharacter ParseExperience(JsonSerializer pSerializer, JToken pJToken, ICharacter pCharacter)
		{
			var experienceToken = pJToken.SelectToken(nameof(ICharacter.Experience));
			if (experienceToken == null)
			{
				return pCharacter;
			}

			var experience = pSerializer.Deserialize<IExperience>(experienceToken.CreateReader());
			return pCharacter.AppendExperience(experience);
		}

		private static ICharacter ParseCharacterPurse(JsonSerializer pSerializer, JToken pJToken, ICharacter pCharacter)
		{
			var purseToken = pJToken.SelectToken(nameof(ICharacter.Purse));
			if (purseToken == null)
			{
				return pCharacter;
			}
			var purse = pSerializer.Deserialize<IPurse>(purseToken.CreateReader());
			return pCharacter.SetPurse(purse.Copper.Value, purse.Silver.Value, purse.Gold.Value, purse.Platinum.Value);
		}

		private static ICharacter ParseFeats(JsonSerializer pSerializer, JToken pJToken, ICharacter pCharacter)
		{
			var tokens = pJToken.SelectTokens(nameof(ICharacter.Feats)).Children();
			return tokens
				.Select(x => pSerializer.Deserialize<IFeat>(x.CreateReader()))
				.Where(x => x != null)
				.Aggregate(pCharacter, (current, feat) => current.AddFeat(feat, feat.Specialization));
		}

		private static ICharacter ParseSkills(ILibrary<ISkill> pSkillLibrary, JToken pJToken, ICharacter pCharacter)
		{
			return
				pJToken.SelectTokens(nameof(ICharacter.SkillScores))
				.Children()
				.Select(x => new
				{
					SkillName = GetString(x, nameof(ISkillScore.Skill)),
					Ranks = GetInt(x, nameof(ISkillScore.Ranks))
				})
				.Where(s => pSkillLibrary.TryGetValue(s.SkillName, out var skill))
				.Aggregate(
					pCharacter,
					(c, s) => c.AssignSkillPoint(pSkillLibrary[s.SkillName], s.Ranks));
		}

		private static ICharacter ParseInventory(JsonSerializer pSerializer, JToken pJToken, ICharacter pCharacter)
		{
			var inventoryToken = pJToken.SelectToken(nameof(ICharacter.Inventory));
			if (inventoryToken == null)
			{
				return pCharacter;
			}

			var inventory = pSerializer.Deserialize<IInventory>(inventoryToken.CreateReader());
			var character = pCharacter;
			foreach (var valuePair in inventory)
			{
				var item = valuePair.Key;
				var quantity = valuePair.Value;
				for (var i = 0; i < quantity; i++)
				{
					character = character.AddToInventory(item);
				}
			}

			return character;
		}

		private static ICharacter ParseEquipedArmor(JsonSerializer pSerializer, JToken pJToken, ICharacter pCharacter)
		{
			var equipedArmor = 
				GetValuesFromHashObject<ItemType, IItem>(pSerializer, pJToken, nameof(ICharacter.EquipedArmor));

			return 
				equipedArmor
					.Select(valuePair => valuePair.Value)
					.Aggregate(pCharacter, (c, i) => c.EquipArmor(i));
		}

		private static ICharacter ParseLanguages(JsonSerializer pSerializer, JToken pJToken, ICharacter pCharacter)
		{
			var tokens = pJToken.SelectToken(nameof(ICharacter.Languages));
			if (tokens == null)
			{
				return pCharacter;
			}

			return pSerializer.Deserialize<ILanguage[]>(tokens.CreateReader())
				.Aggregate(pCharacter, (character, language) => character.AddLanguage(language));
		}

		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ICharacter pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Age), pValue.Age);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Alignment), pValue.Alignment);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Deity), pValue.Deity);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Gender), pValue.Gender);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Eyes), pValue.Eyes);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Hair), pValue.Hair);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Height), pValue.Height);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Weight), pValue.Weight);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Homeland), pValue.Homeland);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Race), pValue.Race?.Name);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.BaseSize), pValue.BaseSize);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Size), pValue.Size);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Languages), pValue.Languages);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.MaxHealthPoints), pValue.MaxHealthPoints);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Damage), pValue.Damage);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.HealthPoints), pValue.HealthPoints);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.BaseSpeed), pValue.BaseSpeed);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Speed), pValue.Speed);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Purse), pValue.Purse);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Initiative), pValue.Initiative);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Classes), pValue.Classes);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Strength), pValue.Strength);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Dexterity), pValue.Dexterity);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Constitution), pValue.Constitution);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Intelligence), pValue.Intelligence);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Wisdom), pValue.Wisdom);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Charisma), pValue.Charisma);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.ArmorClass), pValue.ArmorClass);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.FlatFooted), pValue.FlatFooted);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Touch), pValue.Touch);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.CombatManeuverDefense), pValue.CombatManeuverDefense);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Fortitude), pValue.Fortitude);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Reflex), pValue.Reflex);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Will), pValue.Will);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Melee), pValue.Melee);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Ranged), pValue.Ranged);
			WriteProperty(pWriter, pSerializer, nameof(ICharacter.CombatManeuverBonus), pValue.CombatManeuverBonus);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.SkillScores), pValue.SkillScores);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Experience), pValue.Experience);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Feats), pValue.Feats);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.Inventory), pValue.Inventory);

			WriteProperty(pWriter, pSerializer, nameof(ICharacter.EquipedArmor), pValue.EquipedArmor);

			pWriter.WriteEndObject();
		}
	}
}
