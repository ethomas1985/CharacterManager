using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetAgeMethod
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
		public void Negative()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<Exception>(() => original.SetAge(-1));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var result = original.SetAge(30);

			Assert.AreEqual(30, result.Age);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var result = original.SetAge(30);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			original.SetAge(30);

			Assert.AreNotEqual(30, original.Age);
		}
	}
}