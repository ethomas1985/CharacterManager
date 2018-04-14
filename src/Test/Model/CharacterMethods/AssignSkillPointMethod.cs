using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Model;
using Pathfinder.Test.Mocks;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AssignSkillPointMethod
	{
		[Test]
		public void Fails_On_Null_Skill()
		{
			Assert.That(
				() =>
				{
					var skillLibrary = MockHelper.GetSkillRepository();

					var original = (ICharacter)new Character(skillLibrary);

					original.AssignSkillPoint(null, -1);
				},
				Throws.Exception.InstanceOf(typeof(ArgumentNullException)));
		}

		[Test]
		public void Success()
		{
			Assert.That(
				() =>
				{
					var skillLibrary = MockHelper.GetSkillRepository();

					var original = (ICharacter)new Character(skillLibrary);

					original.AssignSkillPoint(skillLibrary[SkillMother.SKILL_NAME], -1);
				},
				Throws.Nothing);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = MockHelper.GetSkillRepository();

			var original = (ICharacter)new Character(skillLibrary);

			var result = original.AssignSkillPoint(skillLibrary[SkillMother.SKILL_NAME], -1);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = MockHelper.GetSkillRepository();

			var original = (ICharacter)new Character(skillLibrary);

			original.AssignSkillPoint(skillLibrary[SkillMother.SKILL_NAME], -1);

			Assert.That(original[SkillMother.SKILL_NAME].Ranks, Is.EqualTo(0));
		}

		[Test]
		public void NewInstanceHasRank()
		{
			var original = (ICharacter)new Character(MockHelper.GetSkillRepository());

			var testSkill = MockHelper.GetSkillRepository()[SkillMother.SKILL_NAME];
			Assert.That(testSkill, Is.Not.Null);

			var result = original.AssignSkillPoint(testSkill, 10);

			Assert.That(result[SkillMother.SKILL_NAME].Ranks, Is.EqualTo(10));
		}

		[Test]
		public void HasPendingEvents()
		{
			ICharacter original = new Character(MockHelper.GetSkillRepository());

			var result = original.AssignSkillPoint(MockHelper.GetSkillRepository()[SkillMother.SKILL_NAME], 10);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new SkillRanksAssigned(original.Id, 1, MockHelper.GetSkillRepository()[SkillMother.SKILL_NAME], 10),
					}));
		}
	}
}
