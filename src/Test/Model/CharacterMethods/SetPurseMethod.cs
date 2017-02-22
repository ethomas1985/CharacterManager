using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Test.Mocks;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetPurseMethod
	{
		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetPurse(10, 10, 10, 10);

			Assert.AreEqual(new Copper(10), result.Purse.Copper);
			Assert.AreEqual(new Silver(10), result.Purse.Silver);
			Assert.AreEqual(new Gold(10), result.Purse.Gold);
			Assert.AreEqual(new Platinum(10), result.Purse.Platinum);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetPurse(10, 10, 10, 10);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetPurse(10, 10, 10, 10);

			Assert.IsNull(original.Name);
		}
	}
}