using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AppendExperienceMethod
	{
		private static readonly ILegacyRepository<ISkill> SkillRepository = new Mock<ILegacyRepository<ISkill>>().Object;

		[Test]
		public void NotNull()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.IsNotNull(original.Experience);
		}

		[Test]
		public void InitializedToZero()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.AreEqual(0, original.Experience.Total);
		}

		[Test]
		public void FailWithNullExperienceEvent()
		{
			var original = (ICharacter)new Character(SkillRepository);

			IExperienceEvent nullEvent = null;
			Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullEvent));
		}

		[Test]
		public void SuccessWithExperienceEvent()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var result = original.AppendExperience(new ExperienceEvent("Test", "Test", 10));

			Assert.AreEqual(10, result.Experience.Total);
		}

		[Test]
		public void ReturnsNewInstanceWithExperienceEvent()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var experience = new ExperienceEvent("Test", "Test", 10);
			var result = original.AppendExperience(experience);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchangedWithExperienceEvent()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var experience = new ExperienceEvent("Test", "Test", 10);
			original.AppendExperience(experience);

			Assert.AreEqual(0, original.Experience.Total);
		}

		[Test]
		public void HasPendingEventsForExperienceEvent()
		{
			ICharacter original = new Character(SkillRepository);

			var experienceEvent1 = new ExperienceEvent("Experience Event 1", "Experience Event 1", 10);
			var result = original.AppendExperience(experienceEvent1);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ExperienceAdded(original.Id, 1, experienceEvent1),
					}));
		}

		[Test]
		public void FailWithNullExperience()
		{
			var original = (ICharacter)new Character(SkillRepository);

			IExperience nullExperience = null;
			Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullExperience));
		}

		[Test]
		public void SuccessWithExperience()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var experience = new Experience()
				.Append(new ExperienceEvent("Test", "Test", 10));
			var result = original.AppendExperience(experience);

			Assert.AreEqual(10, result.Experience.Total);
		}

		[Test]
		public void ReturnsNewInstanceWithEmptyExperience()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var experience = new Experience();
			var result = original.AppendExperience(experience);

			Assert.That(result, Is.Not.SameAs(original));
		}

		[Test]
		public void OriginalUnchangedWithEmptyExperience()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var experience = new Experience();
			var result = original.AppendExperience(experience);

			Assert.That(original.Experience, Is.Empty);
		}

		[Test]
		public void HasPendingEventsForExperience()
		{
			ICharacter original = new Character(SkillRepository);

			var experienceEvent1 = new ExperienceEvent("Experience Event 1", "Experience Event 1", 10);
			var experienceEvent2 = new ExperienceEvent("Experience Event 2", "Experience Event 2", 10);

			var experience = new Experience()
				.Append(experienceEvent1)
				.Append(experienceEvent2);
			var result = original.AppendExperience(experience);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ExperienceAdded(original.Id, 1, experienceEvent1),
						new ExperienceAdded(original.Id, 2, experienceEvent2),
					}));
		}
	}
}