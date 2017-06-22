using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddLanguageMethod
	{
		private readonly Language _language = new Language("Middle Test-ese");
		private static readonly ILibrary<ISkill> SkillLibrary = new Mock<ILibrary<ISkill>>().Object;

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<ArgumentNullException>(() => original.AddLanguage(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			var result = original.AddLanguage(_language);

			Assert.IsTrue(result.Languages.Contains(_language));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var result = original.AddLanguage(_language);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			original.AddLanguage(_language);

			Assert.IsFalse(original.Languages.Contains(_language));
		}
	}
}