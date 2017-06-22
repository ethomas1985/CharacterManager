using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;
using Pathfinder.Test.Serializers.Json.CharacterTests;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddFeatMethod
	{
		private static readonly ILibrary<ISkill> SkillLibrary = new Mock<ILibrary<ISkill>>().Object;

		[Test]
		public void FailsWithNullFeat()
		{
			var original = (ICharacter)new Character(SkillLibrary);

			Assert.Throws<ArgumentNullException>(() => original.AddFeat(null));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(SkillLibrary);

			var result = original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(SkillLibrary);
			original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2());

			Assert.That(original.Feats, Is.Empty);
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter)new Character(SkillLibrary);

			var result = original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2());

			Assert.That(result.Feats.Count(), Is.EqualTo(1));
		}

		[Test]
		public void When_Is_Specialized()
		{
			const string specialization = "user-choice";

			var original = (ICharacter)new Character(SkillLibrary);

			var result = original.AddFeat(CharacterJsonSerializerUtils.CreateTestingFeat2(), specialization);

			Assert.That(result.Feats.First().Specialization, Is.EqualTo(specialization));
		}
	}
}