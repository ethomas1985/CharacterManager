using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;
using Pathfinder.Enums;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetNameMethod
	{
		private static ILegacyRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<ILegacyRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.SetName(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillRepository);

			const string testName = "Test Name";
			var result = original.SetName(testName);

			Assert.AreEqual(testName, result.Name);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = SkillRepository;

			var original = (ICharacter) new Character(skillLibrary);

			const string testName = "Test Name";
			var result = original.SetName(testName);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = SkillRepository;

			var original = (ICharacter) new Character(skillLibrary);

			const string testName = "Test Name";
			original.SetName(testName);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			const string testName = "Test Name";
			var result = original.SetName(testName);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new NameSet(original.Id, 1, testName),
					}));
		}
	}
}