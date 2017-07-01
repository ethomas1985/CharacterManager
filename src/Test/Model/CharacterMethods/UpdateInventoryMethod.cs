using Moq;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[Ignore("Method Not Yet Implemented.")]
	[TestFixture]
	public class UpdateInventoryMethod
	{
		private static IRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<IRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void Success()
		{
			Assert.Fail("Not Yet Implemented");
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillRepository);

			var result = original.UpdateInventory(null);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillRepository);
			original.UpdateInventory(null);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void HasPendingEvents()
		{
			Assert.Fail("Not Implemented");
		}
	}
}