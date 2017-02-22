using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;
using System.Linq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddLanguageMethod
	{
		private readonly Language _language = new Language("Middle Test-ese");

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.AddLanguage(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var result = original.AddLanguage(_language);

			Assert.IsTrue(result.Languages.Contains(_language));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.AddLanguage(_language);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.AddLanguage(_language);

			Assert.IsFalse(original.Languages.Contains(_language));
		}
	}
}