using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AppendExperienceMethod
	{
		private static readonly ILibrary<ISkill> SkillLibrary = new Mock<ILibrary<ISkill>>().Object;

		[Test]
		public void NotNull()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.IsNotNull(original.Experience);
		}

		[Test]
		public void InitializedToZero()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.AreEqual(0, original.Experience.Total);
		}

		[Test]
		public void FailWithNullEvent()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			IEvent nullEvent = null;
			Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullEvent));
		}

		[Test]
		public void SuccessWithEvent()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var result = original.AppendExperience(new Event("Test", "Test", 10));

			Assert.AreEqual(10, result.Experience.Total);
		}

		[Test]
		public void ReturnsNewInstanceWithEvent()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var experience = new Event("Test", "Test", 10);
			var result = original.AppendExperience(experience);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchangedWithEvent()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			var experience = new Event("Test", "Test", 10);
			original.AppendExperience(experience);

			Assert.AreEqual(0, original.Experience.Total);
		}

		[Test]
		public void FailWithNullExperience()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			IExperience nullExperience = null;
			Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullExperience));
		}

		[Test]
		public void SuccessWithExperience()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var experience = new Experience()
				.Append(new Event("Test", "Test", 10));
			var result = original.AppendExperience(experience);

			Assert.AreEqual(10, result.Experience.Total);
		}

		[Test]
		public void ReturnsNewInstanceWithEmptyExperience()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var experience = new Experience()
				.Append(new Event("Test", "Test", 10));
			var result = original.AppendExperience(experience);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchangedWithEmptyExperience()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var experience = new Experience()
				.Append(new Event("Test", "Test", 10));
			var result = original.AppendExperience(experience);

			Assert.AreEqual(0, original.Experience.Count());
		}
	}
}