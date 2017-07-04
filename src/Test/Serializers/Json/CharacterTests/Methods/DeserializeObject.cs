using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Test.ObjectMothers;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Serializers.Json.CharacterTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		private const string RACE_NAME = "Test Race";

		[Test]
		public void EmptyObject()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>("{}");
			Assert.NotNull(result);
		}

		[Test]
		public void Expected()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var testCharacter = CharacterMother.UnitMcTesterFace();

			Assert.That(result, Is.EqualTo(testCharacter));
		}

		[Test]
		public void WithRace()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>("{" +
																   $"Race: \"{RACE_NAME}\"" +
																   "}");
			Assert.That(result.Race.Name, Is.EqualTo(RACE_NAME));
		}

		[Test]
		public void WithClass()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(
				"{" +
				"	Classes: [" +
				"		{ " +
				"			\"Class\": \"Test Class\", " +
				"			\"Level\": 1," +
				"			\"IsFavored\": true," +
				"			\"BaseAttackBonus\": 0," +
				"			\"Fortitude\": 0," +
				"			\"Reflex\": 0," +
				"			\"Will\": 0," +
				"			HitPoints: [ 6 ]" +
				"		}" +
				"	]" +
				"}");
			Assert.AreEqual(1, result.Classes.Count());
		}

		[Test]
		public void WithStrength()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(AbilityType.Strength)}\": {{ \"Type\": \"{AbilityType.Strength}\", Base: 12 }} }}");

			Assert.AreEqual(new AbilityScore(AbilityType.Strength, 12), result.Strength);
		}

		[Test]
		public void WithDexterity()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(AbilityType.Dexterity)}\": {{ \"Type\": \"{AbilityType.Dexterity}\", Base: 12 }} }}");

			Assert.AreEqual(new AbilityScore(AbilityType.Dexterity, 12), result.Dexterity);
		}

		[Test]
		public void WithConstitution()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(AbilityType.Constitution)}\": {{ \"Type\": \"{AbilityType.Constitution}\", Base: 12 }} }}");

			Assert.AreEqual(new AbilityScore(AbilityType.Constitution, 12), result.Constitution);
		}

		[Test]
		public void WithIntelligence()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(AbilityType.Intelligence)}\": {{ \"Type\": \"{AbilityType.Intelligence}\", Base: 12 }} }}");

			Assert.AreEqual(new AbilityScore(AbilityType.Intelligence, 12), result.Intelligence);
		}

		[Test]
		public void WithWisdom()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(AbilityType.Wisdom)}\": {{ \"Type\": \"{AbilityType.Wisdom}\", Base: 12 }} }}");

			Assert.AreEqual(new AbilityScore(AbilityType.Wisdom, 12), result.Wisdom);
		}

		[Test]
		public void WithCharisma()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(AbilityType.Charisma)}\": {{ \"Type\": \"{AbilityType.Charisma}\", Base: 12 }} }}");

			Assert.AreEqual(new AbilityScore(AbilityType.Charisma, 12), result.Charisma);
		}

		[Test]
		public void WithName()
		{
			const string name = "Unit McTesterFace";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Name)}\": \"{name}\" }}");

			Assert.That(result.Name, Is.EqualTo(name));
		}

		[Test]
		public void WithAlignment()
		{
			const Alignment alignment = Alignment.LawfulGood;
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Alignment)}\": \"{alignment}\" }}");
			Assert.That(result.Alignment, Is.EqualTo(alignment));
		}

		[Test]
		public void WithGender()
		{
			const Gender gender = Gender.Male;
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Gender)}\": \"{Gender.Male}\" }}");

			Assert.That(result.Gender, Is.EqualTo(gender));
		}

		[Test]
		public void WithAge()
		{
			const int age = 10;
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Age)}\": {age} }}");

			Assert.That(result.Age, Is.EqualTo(age));
		}

		[Test]
		public void WithHomeland()
		{
			const string homeland = "Homeland";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Homeland)}\": \"{homeland}\" }}");

			Assert.That(result.Homeland, Is.EqualTo(homeland));
		}

		[Test]
		public void WithDeity()
		{
			const string deityName = "Deity";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Deity)}\": {{ \"Name\": \"{deityName}\" }} }}");

			Assert.That(result.Deity.Name, Is.EqualTo(deityName));
		}

		[Test]
		public void WithEyes()
		{
			const string eyes = "Blue";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Eyes)}\": \"{eyes}\" }}");

			Assert.That(result.Eyes, Is.EqualTo(eyes));
		}

		[Test]
		public void WithHair()
		{
			const string hair = "Blue";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Hair)}\": \"{hair}\" }}");
			Assert.That(result.Hair, Is.EqualTo(hair));
		}

		[Test]
		public void WithHeight()
		{
			const string heightEscaped = "9' 6\\\"";
			const string height = "9' 6\"";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Height)}\": \"{heightEscaped}\" }}");
			Assert.That(result.Height, Is.EqualTo(height));
		}

		[Test]
		public void WithWeight()
		{
			const string weight = "180 lbs.";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Weight)}\": \"{weight}\" }}");
			Assert.That(result.Weight, Is.EqualTo(weight));
		}

		[Test]
		public void WithOneLanguage()
		{
			const string language = "Test Language";
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Languages)}\": [ \"{language}\" ] }}");
			Assert.That(result.Languages, Is.EquivalentTo(new[] { new Language(language) }));
		}

		[Test]
		public void WithLanguages()
		{
			const string testLanguage = "Test Language";
			const string mockLanguage = "Mock Language";
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(ICharacter.Languages)}\": [ \"{testLanguage}\", \"{mockLanguage}\" ] }}");

			Assert.That(result.Languages, Is.EquivalentTo(new[] { new Language(testLanguage), new Language(mockLanguage) }));
		}

		[Test]
		public void WithDamage()
		{
			const int damage = 2;
			var result = JsonConvert.DeserializeObject<ICharacter>($"{{ \"{nameof(ICharacter.Damage)}\": \"{damage}\" }}");
			Assert.That(result.Damage, Is.EqualTo(damage));
		}

		[Test]
		public void WithPurse()
		{
			var purse = new Purse(1, 2, 3, 4);
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(ICharacter.Purse)}\": {JsonConvert.SerializeObject(purse)} }}");

			Assert.That(result.Purse, Is.EqualTo(purse));
		}

		[Test]
		public void WithSkills()
		{
			var skillScore =
				new SkillScore(
					SkillMother.Create(),
					new AbilityScore(AbilityType.Strength, 0),
					1, 0, 0, 0, 0);

			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(ICharacter.SkillScores)}\": [{JsonConvert.SerializeObject(skillScore)}] }}");
			Assert.That(result.SkillScores, Is.EquivalentTo(new ISkillScore[] { skillScore }));
		}

		[Test]
		public void WithExperience()
		{
			var @event = new ExperienceEvent("Test Event", "Event Description", 10000);
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(ICharacter.Experience)}\": [ {JsonConvert.SerializeObject(@event)} ] }}");
			Assert.That(result.Experience, Is.EquivalentTo(new[] { @event }));
		}

		[Test]
		public void WithFeats()
		{
			var name = FeatMother.CreateTestingFeat1();
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ \"{nameof(ICharacter.Feats)}\": [ {JsonConvert.SerializeObject(name)} ] }}");
			Assert.That(result.Feats, Is.EquivalentTo(new[] { name }));
		}

		[Test]
		public void WithInventory()
		{
			var testingItem = ItemMother.Create();
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ " +
				$"\"{nameof(ICharacter.Inventory)}\": [ " +
				$"{{ " +
				$"\"{nameof(KeyValuePair<IItem, int>.Key)}\": {JsonConvert.SerializeObject(testingItem)}, " +
				$"\"{nameof(KeyValuePair<IItem, int>.Value)}\": {1} " +
				$"}} " +
				$"] " +
				$"}}");
			Assert.That(result.Inventory, Is.EquivalentTo(new Inventory().Add(testingItem, 1)));
		}

		[Test]
		public void WithEquipedArmor()
		{
			var testingItem = ItemMother.Create();
			var result = JsonConvert.DeserializeObject<ICharacter>(
				$"{{ " +
				$"\"{nameof(ICharacter.Inventory)}\": [ " +
				$"{{ " +
				$"\"{nameof(KeyValuePair<IItem, int>.Key)}\": {JsonConvert.SerializeObject(testingItem)}, " +
				$"\"{nameof(KeyValuePair<IItem, int>.Value)}\": {1} " +
				$"}} " +
				$"], " +
				$"\"{nameof(ICharacter.EquipedArmor)}\": {{ " +
				$" \"{testingItem.ItemType}\": {JsonConvert.SerializeObject(testingItem)}" +
				$"}} " +
				$"}}");
			Assert.That(
				result.EquipedArmor,
				Is.EquivalentTo(
					new Dictionary<ItemType, IItem> { [testingItem.ItemType] = testingItem }));
		}

		/**
		 * The following are derived Properties.
		 */

		[Test]
		public void WithArmorClass()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);

			var expected =
				new DefenseScore(
					DefensiveType.ArmorClass,
					1, 1, new AbilityScore(AbilityType.Dexterity, 12), 0, 0, 0, 0, 0);

			Assert.That(result.ArmorClass, Is.EqualTo(expected));
		}

		[Test]
		public void WithFlatFooted()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new DefenseScore(
					DefensiveType.FlatFooted,
					1, 1, new AbilityScore(AbilityType.Dexterity, 12), 0, 0, 0, 0, 0);
			Assert.That(result.FlatFooted, Is.EqualTo(expected));
		}

		[Test]
		public void WithTouch()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new DefenseScore(
					DefensiveType.Touch,
					0, 0, new AbilityScore(AbilityType.Dexterity, 12), 0, 0, 0, 0, 0);
			Assert.That(result.Touch, Is.EqualTo(expected));
		}

		[Test]
		public void WithCombatManeuverDefense()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new DefenseScore(
					1,
					new AbilityScore(AbilityType.Strength, 12),
					new AbilityScore(AbilityType.Dexterity, 12),
					0, 0, 0, 0);
			Assert.That(result.CombatManeuverDefense, Is.EqualTo(expected));
		}

		[Test]
		public void WithFortitude()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new SavingThrow(
					SavingThrowType.Fortitude,
					new AbilityScore(AbilityType.Constitution, 12), 1, 0, 0, 0);
			Assert.That(result.Fortitude, Is.EqualTo(expected));
		}

		[Test]
		public void WithReflex()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new SavingThrow(
					SavingThrowType.Reflex,
					new AbilityScore(AbilityType.Dexterity, 12), 1, 0, 0, 0);
			Assert.That(result.Reflex, Is.EqualTo(expected));
		}

		[Test]
		public void WithWill()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new SavingThrow(
					SavingThrowType.Will,
					new AbilityScore(AbilityType.Wisdom, 12), 1, 0, 0, 0);
			Assert.That(result.Will, Is.EqualTo(expected));
		}

		[Test]
		public void WithMelee()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new OffensiveScore(
					OffensiveType.Melee,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0);
			Assert.That(result.Melee, Is.EqualTo(expected));
		}

		[Test]
		public void WithRanged()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new OffensiveScore(
					OffensiveType.Ranged,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0);
			Assert.That(result.Ranged, Is.EqualTo(expected));
		}

		[Test]
		public void WithCombatManeuverBonus()
		{
			var result = JsonConvert.DeserializeObject<ICharacter>(Resources.TestCharacter);
			var expected =
				new OffensiveScore(
					OffensiveType.CombatManeuverBonus,
					new AbilityScore(AbilityType.Strength, 12), 1, 0, 0);
			Assert.That(result.CombatManeuverBonus, Is.EqualTo(expected));
		}
	}
}