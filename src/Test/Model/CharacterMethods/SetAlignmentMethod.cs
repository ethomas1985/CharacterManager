using Moq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetAlignmentMethod
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
		public void Success()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			var result = original.SetAlignment(Alignment.LawfulGood);

			Assert.AreEqual(Alignment.LawfulGood, result.Alignment);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			var result = original.SetAlignment(Alignment.LawfulGood);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillLibrary);
			original.SetAlignment(Alignment.LawfulGood);

			Assert.AreNotSame(Alignment.Neutral, original.Alignment);
		}
	}
}