using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AssignSkillPointMethod
	{
		private const string SKILL_NAME = "Unit Testing";

		private static Skill CreateTestingSkill()
		{
			return new Skill(
				SKILL_NAME,
				AbilityType.Intelligence,
				false,
				false,
				"Test all the Units");
		}

		[Test]
		public void Fails_On_Null_Skill()
		{
			Assert.That(
				() =>
				{
					var skillLibrary = new MockSkillLibrary();

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
					var skillLibrary = new MockSkillLibrary();
					skillLibrary.Store(CreateTestingSkill());

					var original = (ICharacter)new Character(skillLibrary);

					original.AssignSkillPoint(skillLibrary[SKILL_NAME], -1);

				},
				Throws.Nothing);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = new MockSkillLibrary();
			skillLibrary.Store(CreateTestingSkill());

			var original = (ICharacter)new Character(skillLibrary);

			var result = original.AssignSkillPoint(skillLibrary[SKILL_NAME], -1);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = new MockSkillLibrary();
			skillLibrary.Store(CreateTestingSkill());

			var original = (ICharacter)new Character(skillLibrary);

			original.AssignSkillPoint(skillLibrary[SKILL_NAME], -1);

			Assert.That(original[SKILL_NAME].Ranks, Is.EqualTo(0));
		}

		[Test]
		public void NewInstanceHasRank()
		{
			var skillLibrary = new MockSkillLibrary();
			skillLibrary.Store(CreateTestingSkill());

			var original = (ICharacter)new Character(skillLibrary);

			var reault = original.AssignSkillPoint(skillLibrary[SKILL_NAME], 10);

			Assert.That(reault[SKILL_NAME].Ranks, Is.EqualTo(10));
		}
	}
}