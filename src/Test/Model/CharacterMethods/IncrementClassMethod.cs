using NUnit.Framework;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class IncrementClassMethod
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
		public void NullClass()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.IncrementClass(null));
		}

		[Test]
		public void NewClass()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentException>(() => original.IncrementClass(ClassMother.Level1Neutral()));
		}

		[Test]
		public void Success()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository)).AddClass(mockClass);

			var firstClass = original.Classes.Select(x => x.Class).First();
			var result = original.IncrementClass(firstClass);

			Assert.IsNotNull(result);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository)).AddClass(mockClass);
			var result = original.IncrementClass(mockClass);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository)).AddClass(mockClass);
			var originalCharacterClass = original.Classes.First();

			var result = original.IncrementClass(mockClass);
			var resultCharacterClass = result.Classes.First();

			Assert.AreEqual(originalCharacterClass.Class, resultCharacterClass.Class);
			Assert.AreEqual(1, originalCharacterClass.Level);
		}

		[Test]
		public void IncrementsLevel()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository)).AddClass(mockClass);
			var originalCharacterClass = original.Classes.First();

			var result = original.IncrementClass(mockClass);
			var resultCharacterClass = result.Classes.First();

			Assert.AreEqual(originalCharacterClass.Level + 1, resultCharacterClass.Level);
		}

		[Test]
		public void UpdatesHitPointsWithDefault()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository))
				.SetConstitution(10)
				.AddClass(mockClass);
			var originalCharacterClass = original.Classes.First();

			var result = original.IncrementClass(mockClass);
			var resultCharacterClass = result.Classes.First();

			var newHitPoints = mockClass.HitDie.Faces;
			var originalHitPoints = originalCharacterClass.HitPoints.Sum();
			var constitutionModifier = original.Constitution.Modifier;

			var expected = originalHitPoints + newHitPoints + constitutionModifier;

			var actual = resultCharacterClass.HitPoints.Sum();
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void UpdatesHitPoints()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository))
				.SetConstitution(10)
				.AddClass(mockClass);
			var originalCharacterClass = original.Classes.First();

			const int newHitPoints = 3;
			var result = original.IncrementClass(mockClass, newHitPoints);
			var resultCharacterClass = result.Classes.First();

			var originalHitPoints = originalCharacterClass.HitPoints.Sum();
			var constitutionModifier = original.Constitution.Modifier;

			var expected = originalHitPoints + newHitPoints + constitutionModifier;

			var actual = resultCharacterClass.HitPoints.Sum();
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void UpdatesSkillPoints()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository))
				.SetIntelligence(10)
				.AddClass(mockClass);

			var result = original.IncrementClass(mockClass);
			var resultCharacterClass = result.Classes.First();


			var expected = resultCharacterClass.Level * (mockClass.SkillAddend + result.Intelligence.Modifier);
			Assert.AreEqual(expected, result.MaxSkillRanks);
		}

		[Test]
		public void HasPendingEvents()
		{
			var mockClass = ClassMother.Level1Neutral();
			var original = ((ICharacter)new Character(SkillRepository)).AddClass(mockClass);

			var firstClass = original.Classes.Select(x => x.Class).First();
			var result = original.IncrementClass(firstClass, 3);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ClassAdded(original.Id, 1, new CharacterClass(firstClass, 1, true, new [] { 6 })),
						new ClassLevelRaised(original.Id, 2, firstClass, 3),
					}));
		}
	}
}