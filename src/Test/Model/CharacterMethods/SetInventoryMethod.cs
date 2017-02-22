using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[Ignore("Method Not Yet Implemented.")]
	[TestFixture]
	public class SetInventoryMethod
	{
		[Test]
		public void Success()
		{
			Assert.Fail("Not Yet Implemented");
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetInventory(null);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetInventory(null);

			Assert.IsNull(original.Name);
		}
	}
}