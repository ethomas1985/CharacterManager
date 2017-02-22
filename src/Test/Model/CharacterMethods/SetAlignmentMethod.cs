using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetAlignmentMethod
	{
		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var result = original.SetAlignment(Alignment.LawfulGood);

			Assert.AreEqual(Alignment.LawfulGood, result.Alignment);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetAlignment(Alignment.LawfulGood);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetAlignment(Alignment.LawfulGood);

			Assert.AreNotSame(Alignment.Neutral, original.Alignment);
		}
	}
}