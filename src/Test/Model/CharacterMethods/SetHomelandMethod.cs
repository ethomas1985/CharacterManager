using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetHomelandMethod
	{
		private const string TESTLANDIA = "Testlandia";

		private static ILibrary<ISkill> SkillLibrary
		{
			get
			{
				var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<ArgumentNullException>(() => original.SetHomeland(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			var result = original.SetHomeland(TESTLANDIA);

			Assert.AreEqual(TESTLANDIA, result.Homeland);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			var result = original.SetHomeland(TESTLANDIA);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			original.SetHomeland(TESTLANDIA);

			Assert.IsNull(original.Homeland);
		}
	}
}