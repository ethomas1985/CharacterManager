using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetEyesMethod
	{
		private const string EYES = "Octarine";

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.SetEyes(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var result = original.SetHomeland(EYES);

			Assert.AreEqual(EYES, result.Homeland);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetEyes(EYES);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetEyes(EYES);

			Assert.IsNull(original.Name);
		}
	}
}