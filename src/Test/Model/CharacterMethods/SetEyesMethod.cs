using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetEyesMethod
	{
		private const string EYES = "Octarine";

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

			Assert.Throws<ArgumentNullException>(() => original.SetEyes(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			var result = original.SetHomeland(EYES);

			Assert.AreEqual(EYES, result.Homeland);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var result = original.SetEyes(EYES);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			original.SetEyes(EYES);

			Assert.IsNull(original.Name);
		}
	}
}