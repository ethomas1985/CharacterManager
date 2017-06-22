using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using System;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class RemoveFromInventoryMethod
	{
		private static ILibrary<ISkill> SkillLibrary
		{
			get
			{
				var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

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
					var result = original.RemoveFromInventory(null);
				},
				Throws.Exception.InstanceOf(typeof(ArgumentNullException)));
		}

		[Test]
		public void Success()
		{
			Assert.That(
				() =>
				{
					var item = CreateTestingItem();

					var original = (ICharacter)new Character(SkillLibrary);
					original = original.AddToInventory(item);

					var result = original.RemoveFromInventory(item);
				},
				Throws.Nothing);
		}

		[Test]
		public void SuccessWhenItemNotInInventory()
		{
			Assert.That(
				() =>
				{
					var item = CreateTestingItem();

					var original = (ICharacter)new Character(SkillLibrary);

					var result = original.RemoveFromInventory(item);
				},
				Throws.Nothing);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var item = CreateTestingItem();

			var original = (ICharacter)new Character(SkillLibrary);
			original = original.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var item = CreateTestingItem();

			var original = (ICharacter)new Character(SkillLibrary);
			original = original.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void DecrementsQuantityOfExistingItem()
		{
			var item = CreateTestingItem();

			var original = (ICharacter)new Character(SkillLibrary);
			original = original.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.IsNull(result.Name);
		}

		[Test]
		public void RemovesItemWhenQuantityReachesZero()
		{
			var item = CreateTestingItem();

			var original = (ICharacter)new Character(SkillLibrary);
			original = original.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.IsNull(result.Name);
		}
	}
}