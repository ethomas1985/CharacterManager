using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Collections.Generic;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AssignSkillPointMethod
	{
		private const string SKILL_NAME = "Test Skill";

		private static ILegacyRepository<ISkill> GetSkillRepository()
		{
			ISkill iSkill;
			var testSkill = SkillMother.Create();
			var mockSkillLibrary = new Mock<ILegacyRepository<ISkill>>();

			mockSkillLibrary
				.Setup(foo => foo.GetEnumerator())
				.Returns(new List<ISkill> { testSkill }.GetEnumerator());

			mockSkillLibrary
				.Setup(foo => foo.Values)
				.Returns(new List<ISkill> { testSkill });

			mockSkillLibrary
				.Setup(foo => foo[testSkill.Name])
				.Returns(testSkill);

			mockSkillLibrary
				.Setup(foo => foo.TryGetValue(testSkill.Name, out iSkill))
				.OutCallback((string t, out ISkill r) => r = testSkill)
				.Returns(true);

			return mockSkillLibrary.Object;
		}

		[Test]
		public void Fails_On_Null_Skill()
		{
			Assert.That(
				() =>
				{
					var skillLibrary = GetSkillRepository();

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
					var skillLibrary = GetSkillRepository();

					var original = (ICharacter)new Character(skillLibrary);

					original.AssignSkillPoint(skillLibrary[SKILL_NAME], -1);
				},
				Throws.Nothing);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = GetSkillRepository();

			var original = (ICharacter)new Character(skillLibrary);

			var result = original.AssignSkillPoint(skillLibrary[SKILL_NAME], -1);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = GetSkillRepository();

			var original = (ICharacter)new Character(skillLibrary);

			original.AssignSkillPoint(skillLibrary[SKILL_NAME], -1);

			Assert.That(original[SKILL_NAME].Ranks, Is.EqualTo(0));
		}

		[Test]
		public void NewInstanceHasRank()
		{
			var original = (ICharacter)new Character(GetSkillRepository());

			var result = original.AssignSkillPoint(GetSkillRepository()[SKILL_NAME], 10);

			Assert.That(result[SKILL_NAME].Ranks, Is.EqualTo(10));
		}

		[Test]
		public void HasPendingEvents()
		{
			ICharacter original = new Character(GetSkillRepository());

			var result = original.AssignSkillPoint(GetSkillRepository()[SKILL_NAME], 10);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new SkillRanksAssigned(original.Id, 1, GetSkillRepository()[SKILL_NAME], 10),
					}));
		}
	}
}