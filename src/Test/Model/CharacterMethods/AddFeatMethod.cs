using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;
using System.Linq;
using Pathfinder.Test.Serializers.Json.CharacterTests;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddFeatMethod
	{
		[Test]
		public void FailsWithNullFeat()
		{
			var original = (ICharacter)new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.AddFeat(null));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(new MockSkillLibrary());

			var result = original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(new MockSkillLibrary());
			original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2());

			Assert.That(original.Feats, Is.Empty);
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter)new Character(new MockSkillLibrary());

			var result = original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2());

			Assert.That(result.Feats.Count(), Is.EqualTo(1));
		}

		[Test]
		public void When_Is_Specialized()
		{
			const string specialization = "user-choice";

			var original = (ICharacter)new Character(new MockSkillLibrary());

			var result = original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2(), specialization);

			Assert.That(result.Feats.First().Specialization, Is.EqualTo(specialization));
		}
	}
}