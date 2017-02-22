using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetIntelligenceMethod
	{
		[Test]
		public void InvalidValue()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<Exception>(() => original.SetIntelligence(-1));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var result = original.SetIntelligence(10);

			Assert.AreEqual(10, result.Intelligence.Base);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetIntelligence(10);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetIntelligence(10);

			Assert.AreEqual(0, original.Intelligence.Base);
		}
	}
}