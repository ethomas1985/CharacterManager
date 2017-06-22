using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetDexterityMethod
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
		public void InvalidValue()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<Exception>(() => original.SetDexterity(-1));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			var result = original.SetDexterity(10);

			Assert.AreEqual(10, result.Dexterity.Base);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var result = original.SetDexterity(10);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			original.SetDexterity(10);

			Assert.AreEqual(0, original.Dexterity.Base);
		}
	}
}