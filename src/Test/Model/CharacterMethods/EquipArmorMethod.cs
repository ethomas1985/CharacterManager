using System;
using System.Collections.Immutable;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Test.Mocks;

namespace Pathfinder.Test.Model.CharacterMethods
{
	//[Ignore("Method Not Yet Implemented.")]
	[TestFixture]
	public class EquipArmorMethod
	{
		private static Item CreateTestingItem()
		{
			return new Item(
				"Testing Item",
				ItemType.Arms,
				"Category",
				new Purse(100),
				10,
				"Description",
				pArmorComponent: new ArmorComponent(
					pArmorBonus: 1,
					pShieldBonus: 1,
					pMaximumDexterityBonus: 1,
					pArmorCheckPenalty: 1,
					pArcaneSpellFailureChance: 0.20m,
					pSpeed: 25));
		}

		private static Race CreateTestingRace()
		{
			return new Race(
				"Test Race",
				string.Empty,
				string.Empty,
				Size.Medium,
				30,
				ImmutableDictionary<AbilityType, int>.Empty,
				ImmutableList<ITrait>.Empty,
				ImmutableList<ILanguage>.Empty);
		}

		[Test]
		public void NullClass()
		{
			var original = (ICharacter)new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.EquipArmor(null));
		}
		[Test]
		public void ItemNotInInventory()
		{
			var original = (ICharacter)new Character(new MockSkillLibrary());

			Assert.That(
				() => original.EquipArmor(CreateTestingItem()),
				Throws.TypeOf<ArgumentException>().And.Message.EqualTo("Item not in inventory."));
		}

		[Test]
		public void Equiped()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original = 
				new Character(new MockSkillLibrary())
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(armorComponent);

			Assert.That(result.EquipedArmor.Values, Contains.Item(armorComponent));
		}

		[Test]
		public void EquipedInSlot()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original =
				new Character(new MockSkillLibrary())
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(armorComponent);

			Assert.That(result.EquipedArmor.Keys, Contains.Item(armorComponent.ItemType));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original =
				new Character(new MockSkillLibrary())
					.AddToInventory(armorComponent);
			var result = original.EquipArmor(CreateTestingItem());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original =
				new Character(new MockSkillLibrary())
					.AddToInventory(armorComponent);

			original.EquipArmor(CreateTestingItem());

			Assert.That(original.EquipedArmor, Is.Empty);
		}

		[Test]
		public void EquipedArmorUpdatesArmorClass()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original =
				new Character(new MockSkillLibrary())
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(CreateTestingItem());

			Assert.That(result.ArmorClass.Score, Is.EqualTo(7));
		}

		[Test]
		public void EquipedArmorLimitsDexterityScore()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original =
				new Character(new MockSkillLibrary())
					.SetDexterity(18)
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(CreateTestingItem());

			Assert.That(result.Dexterity.Modifier, Is.LessThan(2));
		}

		[Test]
		public void EquipedArmorLimitsSpeed()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original =
				new Character(new MockSkillLibrary())
					.SetRace(CreateTestingRace())
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(CreateTestingItem());

			Assert.That(result.Speed, Is.EqualTo(25));
		}

		[Test]
		public void EquipedArmorEffectsArmorCheckPenalty()
		{
			var armorComponent = CreateTestingItem();
			ICharacter original =
				new Character(new MockSkillLibrary())
					.SetRace(CreateTestingRace())
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(CreateTestingItem());

			Assert.That(result.ArmorCheckPenalty, Is.EqualTo(1));
		}
	}
}