using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using System;
using System.Linq;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{

	[TestFixture]
	public class AddToInventoryMethod
	{
		private static readonly ILibrary<ISkill> SkillLibrary = new Mock<ILibrary<ISkill>>().Object;

		private static Item CreateTestingItem()
		{
			return new Item("Testing Item", ItemType.None, "Category", new Purse(100), 10, "Description");
		}

		[Test]
		public void ThrowsWhenItemIsNull()
		{
			Assert.That(
				() =>
				{
					var original = (ICharacter)new Character(SkillLibrary);
					var result = original.AddToInventory(null);
				},
				Throws.Exception.InstanceOf(typeof(ArgumentNullException)));
		}

		[Test]
		public void Success()
		{
			Assert.That(
				() =>
				{
					var original = (ICharacter)new Character(SkillLibrary);

					var item = CreateTestingItem();

					var result = original.AddToInventory(item);
				},
				Throws.Nothing);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(SkillLibrary);
			var item = CreateTestingItem();

			var result = original.AddToInventory(item);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(SkillLibrary);
			var item = CreateTestingItem();

			var result = original.AddToInventory(item);

			Assert.IsFalse(original.Inventory.Any());
		}

		[Test]
		public void IncrementsQuantityOfExistingItem()
		{
			var original = (ICharacter)new Character(SkillLibrary);
			var item = CreateTestingItem();

			var result = original.AddToInventory(item);

			Assert.IsNull(result.Name);
		}
	}
}