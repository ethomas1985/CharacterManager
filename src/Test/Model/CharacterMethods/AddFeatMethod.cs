using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddFeatMethod
	{
		[Test]
		public void FailsWithNullFeat()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.AddFeat(null));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.AddFeat(CreateTestingFeat());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());
			original.AddFeat(CreateTestingFeat());

			Assert.That(original.Feats, Is.Empty);
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.AddFeat(CreateTestingFeat());

			Assert.That(result.Feats.Count(), Is.EqualTo(1));
		}

		private static Feat CreateTestingFeat()
		{
			return new Feat(
						    "Feat 2",
						    FeatType.General, 
						    new List<string> {"Feat 1"},
						    "Testing Description",
						    "Testing Benefit",
						    "Testing Special");
		}
	}
}