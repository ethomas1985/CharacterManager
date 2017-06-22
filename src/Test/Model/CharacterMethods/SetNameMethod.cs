using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetNameMethod
	{
		private static ILibrary<ISkill> SkillLibrary
		{
			get
			{
				var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<ArgumentNullException>(() => original.SetName(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			const string testName = "Test Name";
			var result = original.SetName(testName);

			Assert.AreEqual(testName, result.Name);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = SkillLibrary;

			var original = (ICharacter) new Character(skillLibrary);

			const string testName = "Test Name";
			var result = original.SetName(testName);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = SkillLibrary;

			var original = (ICharacter) new Character(skillLibrary);

			const string testName = "Test Name";
			original.SetName(testName);

			Assert.IsNull(original.Name);
		}
	}
}