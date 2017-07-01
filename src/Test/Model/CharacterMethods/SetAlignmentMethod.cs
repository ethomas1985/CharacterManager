using Moq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetAlignmentMethod
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
			var result = original.SetAlignment(Alignment.LawfulGood);

			Assert.AreEqual(Alignment.LawfulGood, result.Alignment);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillRepository);

			var result = original.SetAlignment(Alignment.LawfulGood);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillRepository);
			original.SetAlignment(Alignment.LawfulGood);

			Assert.AreNotSame(Alignment.Neutral, original.Alignment);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetAlignment(Alignment.LawfulGood);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new AlignmentSet(original.Id, 1, Alignment.LawfulGood),
					}));
		}
	}
}