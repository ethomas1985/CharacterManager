using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetHeightMethod
	{
		private const string HEIGHT = "Over 9000";

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.SetHeight(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			var result = original.SetHeight(HEIGHT);

			Assert.AreEqual(HEIGHT, result.Height);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.SetHeight(HEIGHT);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.SetHeight(HEIGHT);

			Assert.IsNull(original.Name);
		}
	}
}