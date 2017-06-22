using Moq;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddDamageMethod
	{
		private static readonly ILibrary<ISkill> SkillLibrary = new Mock<ILibrary<ISkill>>().Object;

		[Test]
		public void TakeDamage()
		{
			var original = ((ICharacter) new Character(SkillLibrary)).SetDamage(10);

			var result = original.AddDamage(5);

			Assert.AreEqual(15, result.Damage);
		}

		[Test]
		public void HealDamage()
		{
			var original = ((ICharacter) new Character(SkillLibrary)).SetDamage(10);

			var result = original.AddDamage(-5);

			Assert.AreEqual(5, result.Damage);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = ((ICharacter) new Character(SkillLibrary)).SetDamage(10);

			var result = original.AddDamage(-1);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = ((ICharacter) new Character(SkillLibrary)).SetDamage(10);
			original.AddDamage(-1);

			Assert.IsNull(original.Name);
		}
	}
}