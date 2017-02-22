using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;
using System.Linq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AppendExperienceMethod
	{
		[Test]
		public void NotNull()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.IsNotNull(original.Experience);
		}

		[Test]
		public void InitializedToZero()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.AreEqual(0, original.Experience.Total);
		}

		[Test]
		public void FailWithNullEvent()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			IEvent nullEvent = null;
			Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullEvent));
		}

		[Test]
		public void SuccessWithEvent()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.AppendExperience(new Event("Test", "Test", 10));

			Assert.AreEqual(10, result.Experience.Total);
		}

		[Test]
		public void ReturnsNewInstanceWithEvent()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var experience = new Event("Test", "Test", 10);
			var result = original.AppendExperience(experience);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchangedWithEvent()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var experience = new Event("Test", "Test", 10);
			original.AppendExperience(experience);

			Assert.AreEqual(0, original.Experience.Total);
		}

		[Test]
		public void FailWithNullExperience()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			IExperience nullExperience = null;
			Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullExperience));
		}

		[Test]
		public void SuccessWithExperience()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var experience = new Experience()
				.Append(new Event("Test", "Test", 10));
			var result = original.AppendExperience(experience);

			Assert.AreEqual(10, result.Experience.Total);
		}

		[Test]
		public void ReturnsNewInstanceWithEmptyExperience()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var experience = new Experience()
				.Append(new Event("Test", "Test", 10));
			var result = original.AppendExperience(experience);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchangedWithEmptyExperience()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var experience = new Experience()
				.Append(new Event("Test", "Test", 10));
			var result = original.AppendExperience(experience);

			Assert.AreEqual(0, original.Experience.Count());
		}
	}
}