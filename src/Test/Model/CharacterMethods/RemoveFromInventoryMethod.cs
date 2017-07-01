using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class RemoveFromInventoryMethod
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
		public void ThrowsWhenItemIsNull()
		{
			Assert.That(
				() =>
				{
					new Character(SkillRepository)
						.RemoveFromInventory(null);
				},
				Throws.Exception.InstanceOf(typeof(ArgumentNullException)));
		}

		[Test]
		public void Success()
		{
			Assert.That(
				() =>
				{
					new Character(SkillRepository)
						.AddToInventory(ItemMother.Create())
						.RemoveFromInventory(ItemMother.Create());
				},
				Throws.Nothing);
		}

		[Test]
		public void SuccessWhenItemNotInInventory()
		{
			Assert.That(
				() =>
				{
					new Character(SkillRepository)
						.RemoveFromInventory(ItemMother.Create());
				},
				Throws.Nothing);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var item = ItemMother.Create();

			var original = new Character(SkillRepository)
				.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var item = ItemMother.Create();

			var original = new Character(SkillRepository)
				.AddToInventory(item);

			original.RemoveFromInventory(item);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void DecrementsQuantityOfExistingItem()
		{
			var item = ItemMother.Create();

			var original = new Character(SkillRepository)
				.AddToInventory(item)
				.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.That(result.Inventory[item], Is.EqualTo(1));
		}

		[Test]
		public void RemovesItemWhenQuantityReachesZero()
		{
			var item = ItemMother.Create();

			var original = new Character(SkillRepository)
				.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.That(result.Inventory[item], Is.EqualTo(0));
		}

		[Test]
		public void HasPendingEvents()
		{
			var item = ItemMother.Create();

			var original = new Character(SkillRepository)
				.AddToInventory(item);

			var result = original.RemoveFromInventory(item);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ItemAddedToInventory(original.Id, 1, item),
						new ItemRemovedFromInventory(original.Id, 2, item),
					}));
		}
	}
}