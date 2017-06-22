using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class ReplaceArmorMethod
	{
		private static ILibrary<ISkill> SkillLibrary
		{
			get
			{
				var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void RequiresArmorToReplaceNotNull()
		{
			ICharacter original = new Character(SkillLibrary);

			Assert.That(
				() => original.ReplaceArmor(null, null),
				Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void ReturnsArmorToEquipNotNull()
		{
			ICharacter original = new Character(SkillLibrary);

			Assert.That(
				() => original.ReplaceArmor(new Mock<IItem>().Object, null),
				Throws.Exception.TypeOf<ArgumentNullException>());
		}

		[Test]
		public void RequiresArmorToReplaceToBeInInventory()
		{
			var armorToReplace = CreateTestingItem($"Armor To Replace");
			ICharacter original = new Character(SkillLibrary);

			Assert.That(
				() => original.ReplaceArmor(armorToReplace, new Mock<IItem>().Object),
				Throws.Exception
					.TypeOf<ArgumentException>()
					.With.Message.EqualTo($"Cannot remove item. Item not in inventory. {armorToReplace.Name}"));
		}

		[Test]
		public void ReturnsArmorToEquipToBeInInventory()
		{
			var armorToReplace = CreateTestingItem($"Armor To Replace");
			var armorToEquip = CreateTestingItem($"Armor To Equip");
			ICharacter original =
				new Character(SkillLibrary)
					.AddToInventory(armorToReplace);

			Assert.That(
				() => original.ReplaceArmor(armorToReplace, armorToEquip),
				Throws.Exception
					.TypeOf<ArgumentException>()
					.With.Message.EqualTo($"Cannot equip item. Item not in inventory. {armorToEquip.Name}"));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var armorToReplace = CreateTestingItem($"Armor To Replace");
			var armorToEquip = CreateTestingItem($"Armor To Equip");

			ICharacter original = new Character(SkillLibrary)
				.AddToInventory(armorToReplace)
				.AddToInventory(armorToEquip);

			ICharacter result = original.ReplaceArmor(armorToReplace, armorToEquip);

			Assert.That(original, Is.Not.SameAs(result));
		}

		[Test]
		public void OriginalUnchanged()
		{
			var armorToReplace = CreateTestingItem($"Armor To Replace");
			var armorToEquip = CreateTestingItem($"Armor To Equip");

			ICharacter original = new Character(SkillLibrary)
				.AddToInventory(armorToReplace)
				.AddToInventory(armorToEquip)
				.EquipArmor(armorToReplace);

			original.ReplaceArmor(armorToReplace, armorToEquip);

			Assert.That(original.EquipedArmor.Select(x => x.Value).First(), Is.EqualTo(armorToReplace));
		}

		[Test]
		public void Success()
		{
			var armorToReplace = CreateTestingItem($"Armor To Replace");
			var armorToEquip = CreateTestingItem($"Armor To Equip");

			ICharacter original = new Character(SkillLibrary)
				.AddToInventory(armorToReplace)
				.AddToInventory(armorToEquip);

			var result = original.ReplaceArmor(armorToReplace, armorToEquip);

			Assert.That(result.EquipedArmor.Select(x => x.Value).First(), Is.EqualTo(armorToEquip));
		}

		public static IItem CreateTestingItem(string pName)
		{
			return new Item(
				pName,
				ItemType.None,
				"Unit Testing",
				new Purse(1, 1, 1, 1),
				pWeight: 12,
				pDescription: "For Unit Testing",
				pArmorComponent: new ArmorComponent(
					pArmorBonus: 1,
					pShieldBonus: 1,
					pMaximumDexterityBonus: 1,
					pArmorCheckPenalty: 1,
					pArcaneSpellFailureChance: 0.20m,
					pSpeed: 25));
		}
	}
}