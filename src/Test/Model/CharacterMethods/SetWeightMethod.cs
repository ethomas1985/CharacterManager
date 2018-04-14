using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetWeightMethod
	{
		private const string WEIGHT = "Over 9000";

		private static ILegacyRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<ILegacyRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void Null()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.SetWeight(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetWeight(WEIGHT);

			Assert.AreEqual(WEIGHT, result.Weight);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var result = original.SetWeight(WEIGHT);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(SkillRepository);
			original.SetWeight(WEIGHT);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetWeight(WEIGHT);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new WeightSet(original.Id, 1, WEIGHT),
					}));
		}
	}
}