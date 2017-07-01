using Moq;
using NUnit.Framework;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Model.Currency;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetPurseMethod
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
		public void Success()
		{
			var original = (ICharacter) new Character(SkillRepository);

			var result = original.SetPurse(10, 10, 10, 10);

			Assert.AreEqual(new Copper(10), result.Purse.Copper);
			Assert.AreEqual(new Silver(10), result.Purse.Silver);
			Assert.AreEqual(new Gold(10), result.Purse.Gold);
			Assert.AreEqual(new Platinum(10), result.Purse.Platinum);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillRepository);

			var result = original.SetPurse(10, 10, 10, 10);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillRepository);
			original.SetPurse(10, 10, 10, 10);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetPurse(10, 10, 10, 10);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new PurseSet(original.Id, 1, 10, 10, 10, 10), 
					}));
		}
	}
}