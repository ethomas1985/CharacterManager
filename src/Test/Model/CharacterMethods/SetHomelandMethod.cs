using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetHomelandMethod
	{
		private const string TESTLANDIA = "Testlandia";

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.SetHomeland(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var result = original.SetHomeland(TESTLANDIA);

			Assert.AreEqual(TESTLANDIA, result.Homeland);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var result = original.SetHomeland(TESTLANDIA);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetHomeland(TESTLANDIA);

			Assert.IsNull(original.Homeland);
		}
	}
}