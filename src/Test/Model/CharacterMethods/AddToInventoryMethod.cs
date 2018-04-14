using NUnit.Framework;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{

	[TestFixture]
	public class AddToInventoryMethod
	{
		private static readonly ILegacyRepository<ISkill> SkillRepository = new Mock<ILegacyRepository<ISkill>>().Object;

		[Test]
		public void ThrowsWhenItemIsNull()
		{
			Assert.That(
				() =>
				{
					new Character(SkillRepository).AddToInventory(null);
				},
				Throws.Exception.InstanceOf(typeof(ArgumentNullException)));
		}

		[Test]
		public void Success()
		{
			Assert.That(
				() => new Character(SkillRepository)
						.AddToInventory(ItemMother.Create()),
				Throws.Nothing);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var item = ItemMother.Create();

			var result = original.AddToInventory(item);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var item = ItemMother.Create();

			var result = original.AddToInventory(item);

			Assert.IsFalse(original.Inventory.Any());
		}

		[Test]
		public void IncrementsQuantityOfExistingItem()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var item = ItemMother.Create();

			var result = original.AddToInventory(item);

			Assert.IsNull(result.Name);
		}

		[Test]
		public void HasPendingEvents()
		{
			ICharacter original = new Character(SkillRepository);
			var item = ItemMother.Create();

			var result = original.AddToInventory(item);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ItemAddedToInventory(original.Id, 1, item),
					}));
		}
	}
}