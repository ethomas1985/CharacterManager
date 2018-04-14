using System;
using System.Collections.Immutable;
using Moq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	//[Ignore("Method Not Yet Implemented.")]
	[TestFixture]
	public class EquipArmorMethod
	{

		private static ILegacyRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<ILegacyRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
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
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.EquipArmor(null));
		}
		[Test]
		public void ItemNotInInventory()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.That(
				() => original.EquipArmor(ItemMother.Armor()),
				Throws.TypeOf<ArgumentException>().And.Message.EqualTo("Item not in inventory."));
		}

		[Test]
		public void Equiped()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original = 
				new Character(SkillRepository)
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(armorComponent);

			Assert.That(result.EquipedArmor.Values, Contains.Item(armorComponent));
		}

		[Test]
		public void EquipedInSlot()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original =
				new Character(SkillRepository)
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(armorComponent);

			Assert.That(result.EquipedArmor.Keys, Contains.Item(armorComponent.ItemType));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original =
				new Character(SkillRepository)
					.AddToInventory(armorComponent);
			var result = original.EquipArmor(ItemMother.Armor());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original =
				new Character(SkillRepository)
					.AddToInventory(armorComponent);

			original.EquipArmor(ItemMother.Armor());

			Assert.That(original.EquipedArmor, Is.Empty);
		}

		[Test]
		public void EquipedArmorUpdatesArmorClass()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original =
				new Character(SkillRepository)
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(ItemMother.Armor());

			Assert.That(result.ArmorClass.Score, Is.EqualTo(7));
		}

		[Test]
		public void EquipedArmorLimitsDexterityScore()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original =
				new Character(SkillRepository)
					.SetDexterity(18)
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(ItemMother.Armor());

			Assert.That(result.Dexterity.Modifier, Is.LessThan(2));
		}

		[Test]
		public void EquipedArmorLimitsSpeed()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original =
				new Character(SkillRepository)
					.SetRace(CreateTestingRace())
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(ItemMother.Armor());

			Assert.That(result.Speed, Is.EqualTo(25));
		}

		[Test]
		public void EquipedArmorEffectsArmorCheckPenalty()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original =
				new Character(SkillRepository)
					.SetRace(CreateTestingRace())
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(ItemMother.Armor());

			Assert.That(result.ArmorCheckPenalty, Is.EqualTo(1));
		}

		[Test]
		public void HasPendingEvents()
		{
			var armorComponent = ItemMother.Armor();
			ICharacter original = 
				new Character(SkillRepository)
					.AddToInventory(armorComponent);

			var result = original.EquipArmor(armorComponent);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ItemAddedToInventory(original.Id, 1, armorComponent), 
						new ArmorEquiped(original.Id, 2, armorComponent),
					}));
		}
	}
}