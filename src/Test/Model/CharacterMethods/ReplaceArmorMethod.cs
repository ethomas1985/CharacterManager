using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class ReplaceArmorMethod
	{
		private static IRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<IRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void RequiresArmorToReplaceNotNull()
		{
			ICharacter original = new Character(SkillRepository);

			Assert.That(
				() => original.ReplaceArmor(null, null),
				Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void RequiresArmorToEquipNotNull()
		{
			ICharacter original = new Character(SkillRepository);

			Assert.That(
				() => original.ReplaceArmor(new Mock<IItem>().Object, null),
				Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void RequiresArmorToReplaceToBeInInventory()
		{
			var armorToReplace = ItemMother.Armor($"Armor To Replace");
			ICharacter original = new Character(SkillRepository);

			Assert.That(
				() => original.ReplaceArmor(armorToReplace, new Mock<IItem>().Object),
				Throws.Exception
					.TypeOf<ArgumentException>()
					.With.Message.EqualTo($"Cannot remove item. Item not in inventory. {armorToReplace.Name}"));
		}

		[Test]
		public void RequiresArmorToEquipToBeInInventory()
		{
			var armorToReplace = ItemMother.Armor($"Armor To Replace");
			var armorToEquip = ItemMother.Armor($"Armor To Equip");
			ICharacter original =
				new Character(SkillRepository)
					.AddToInventory(armorToReplace);

			Assert.That(
				() => original.ReplaceArmor(armorToReplace, armorToEquip),
				Throws.Exception
					.TypeOf<ArgumentException>()
					.With.Message.EqualTo($"Cannot equip item. Item not in inventory. {armorToEquip.Name}"));
		}

		[Test]
		public void RequiresMatchingItemType()
		{
			var armorToReplace = ItemMother.Armor();
			var armorToEquip = ItemMother.Arms();
			ICharacter original =
				new Character(SkillRepository)
					.AddToInventory(armorToReplace)
					.AddToInventory(armorToEquip);

			Assert.That(
				() => original.ReplaceArmor(armorToReplace, armorToEquip),
				Throws.Exception
					.TypeOf<ArgumentException>()
					.With.Message.EqualTo($"Item Types do not match; Equiped '{armorToReplace.ItemType}', Other '{armorToEquip.ItemType}'"));
		}

		[Test]
		public void RequiresArmorToAlreadyBeEquiped()
		{
			var armorToReplace = ItemMother.Armor($"Armor To Replace");
			var armorToEquip = ItemMother.Armor($"Armor To Equip");
			ICharacter original =
				new Character(SkillRepository)
					.AddToInventory(armorToReplace)
					.AddToInventory(armorToEquip);

			Assert.That(
				() => original.ReplaceArmor(armorToReplace, armorToEquip),
				Throws.Exception
					.TypeOf<ArgumentException>()
					.With.Message.EqualTo($"Armor not equiped; {armorToReplace.Name}"));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var armorToReplace = ItemMother.Armor($"Armor To Replace");
			var armorToEquip = ItemMother.Armor($"Armor To Equip");

			ICharacter original = new Character(SkillRepository)
				.AddToInventory(armorToReplace)
				.AddToInventory(armorToEquip)
				.EquipArmor(armorToReplace);

			ICharacter result = original.ReplaceArmor(armorToReplace, armorToEquip);

			Assert.That(original, Is.Not.SameAs(result));
		}

		[Test]
		public void OriginalUnchanged()
		{
			var armorToReplace = ItemMother.Armor($"Armor To Replace");
			var armorToEquip = ItemMother.Armor($"Armor To Equip");

			ICharacter original = new Character(SkillRepository)
				.AddToInventory(armorToReplace)
				.AddToInventory(armorToEquip)
				.EquipArmor(armorToReplace);

			original.ReplaceArmor(armorToReplace, armorToEquip);

			Assert.That(original.EquipedArmor.Select(x => x.Value).First(), Is.EqualTo(armorToReplace));
		}

		[Test]
		public void Success()
		{
			var armorToReplace = ItemMother.Armor($"Armor To Replace");
			var armorToEquip = ItemMother.Armor($"Armor To Equip");

			ICharacter original = new Character(SkillRepository)
				.AddToInventory(armorToReplace)
				.AddToInventory(armorToEquip)
				.EquipArmor(armorToReplace);

			var result = original.ReplaceArmor(armorToReplace, armorToEquip);

			Assert.That(result.EquipedArmor.Select(x => x.Value).First(), Is.EqualTo(armorToEquip));
		}

		[Test]
		public void HasPendingEvents()
		{
			var armorToReplace = ItemMother.Armor($"Armor To Replace");
			var armorToEquip = ItemMother.Armor($"Armor To Equip");

			ICharacter original = new Character(SkillRepository)
				.AddToInventory(armorToReplace)
				.AddToInventory(armorToEquip)
				.EquipArmor(armorToReplace);

			var result = original.ReplaceArmor(armorToReplace, armorToEquip);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ItemAddedToInventory(original.Id, 1, armorToReplace),
						new ItemAddedToInventory(original.Id, 2, armorToEquip),
						new ArmorEquiped(original.Id, 3, armorToReplace),
						new ArmorRemoved(original.Id, 4, armorToReplace),
						new ArmorEquiped(original.Id, 5, armorToEquip),
					}));
		}
	}
}