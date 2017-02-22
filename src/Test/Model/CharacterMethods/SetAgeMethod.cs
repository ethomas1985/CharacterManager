using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetAgeMethod
	{
		[Test]
		public void Negative()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<Exception>(() => original.SetAge(-1));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetAge(30);

			Assert.AreEqual(30, result.Age);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetAge(30);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetAge(30);

			Assert.AreNotEqual(30, original.Age);
		}
	}
}