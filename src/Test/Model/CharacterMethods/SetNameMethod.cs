using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetNameMethod
	{
		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.SetName(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			const string testName = "Test Name";
			var result = original.SetName(testName);

			Assert.AreEqual(testName, result.Name);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = new MockSkillLibrary();

			var original = (ICharacter) new Character(skillLibrary);

			const string testName = "Test Name";
			var result = original.SetName(testName);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = new MockSkillLibrary();

			var original = (ICharacter) new Character(skillLibrary);

			const string testName = "Test Name";
			original.SetName(testName);

			Assert.IsNull(original.Name);
		}
	}
}