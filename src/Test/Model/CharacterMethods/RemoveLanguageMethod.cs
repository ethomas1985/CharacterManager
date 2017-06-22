using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class RemoveLanguageMethod
	{
		private static ILibrary<ISkill> SkillLibrary
		{
			get
			{
				var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		private readonly Language _language = new Language("Middle Test-ese");

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<ArgumentNullException>(() => original.RemoveLanguage(null));
		}

		[Test]
		public void Success()
		{
			var original = ((ICharacter) new Character(SkillLibrary)).AddLanguage(_language);
			Assert.IsTrue(original.Languages.Contains(_language));

			var result = original.RemoveLanguage(_language);

			Assert.IsFalse(result.Languages.Contains(_language));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = ((ICharacter) new Character(SkillLibrary)).AddLanguage(_language);
			Assert.IsTrue(original.Languages.Contains(_language));

			var result = original.RemoveLanguage(_language);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = ((ICharacter) new Character(SkillLibrary)).AddLanguage(_language);
			Assert.IsTrue(original.Languages.Contains(_language));

			original.RemoveLanguage(_language);

			Assert.IsTrue(original.Languages.Contains(_language));
		}
	}
}