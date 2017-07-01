using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddFeatMethod
	{
		private static readonly IRepository<ISkill> SkillRepository = new Mock<IRepository<ISkill>>().Object;

		[Test]
		public void FailsWithNullFeat()
		{
			ICharacter original = new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.AddFeat(null));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			ICharacter original = new Character(SkillRepository);

			var result = original.AddFeat(FeatMother.CreateTestingFeat2());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			ICharacter original = new Character(SkillRepository);
			original.AddFeat(FeatMother.CreateTestingFeat2());

			Assert.That(original.Feats, Is.Empty);
		}

		[Test]
		public void Success()
		{
			ICharacter original = new Character(SkillRepository);

			var result = original.AddFeat(FeatMother.CreateTestingFeat2());

			Assert.That(result.Feats.Count(), Is.EqualTo(1));
		}

		[Test]
		public void When_Is_Specialized()
		{
			const string specialization = "user-choice";

			ICharacter original = new Character(SkillRepository);

			var result = original.AddFeat(FeatMother.CreateTestingFeat2(), specialization);

			Assert.That(result.Feats.First().Specialization, Is.EqualTo(specialization));
		}

		[Test]
		public void HasPendingEvents()
		{
			ICharacter original = new Character(SkillRepository);

			var testingFeat1 = FeatMother.CreateTestingFeat1();
			var result = original.AddFeat(testingFeat1);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new FeatAdded(original.Id, 1, testingFeat1),
					}));
		}
	}
}