using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class IncrementClassMethod
	{
		private static ILibrary<ISkill> SkillLibrary
		{
			get
			{
				var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		private const int SKILL_ADDEND = 5;
		private const int HIT_DIE_FACES = 6;

		private static IClass CreateMockClass()
		{
			return ClassMother.Create(
				"Mock Class",
				new HashSet<Alignment>
				{
					Alignment.ChaoticGood,
					Alignment.ChaoticNeutral,
					Alignment.ChaoticEvil
				},
				new Die(HIT_DIE_FACES),
				SKILL_ADDEND);
		}

		[Test]
		public void NullClass()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<ArgumentNullException>(() => original.IncrementClass(null));
		}

		[Test]
		public void NewClass()
		{
			var original = (ICharacter) new Character(SkillLibrary);

			Assert.Throws<ArgumentException>(() => original.IncrementClass(ClassMother.Create()));
		}

		[Test]
		public void Success()
		{
			var mockClass = CreateMockClass();
			var original = ((ICharacter) new Character(SkillLibrary)).AddClass(mockClass);

			var firstClass = original.Classes.Select(x => x.Class).First();
			var result = original.IncrementClass(firstClass);

			Assert.IsNotNull(result);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var mockClass = CreateMockClass();
			var original = ((ICharacter) new Character(SkillLibrary)).AddClass(mockClass);
			var result = original.IncrementClass(mockClass);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var mockClass = CreateMockClass();
			var original = ((ICharacter) new Character(SkillLibrary)).AddClass(mockClass);
			var originalCharacterClass = original.Classes.First();

			var result = original.IncrementClass(mockClass);
			var resultCharacterClass = result.Classes.First();

			Assert.AreEqual(originalCharacterClass.Class, resultCharacterClass.Class);
			Assert.AreEqual(1, originalCharacterClass.Level);
		}

		[Test] public void IncrementsLevel()
		{
			var mockClass = CreateMockClass();
			var original = ((ICharacter) new Character(SkillLibrary)).AddClass(mockClass);
			var originalCharacterClass = original.Classes.First();

			var result = original.IncrementClass(mockClass);
			var resultCharacterClass = result.Classes.First();

			Assert.AreEqual(originalCharacterClass.Level + 1, resultCharacterClass.Level);
		}

		[Test]
		public void UpdatesHitPointsWithDefault()
		{
			var mockClass = CreateMockClass();
			var original = ((ICharacter) new Character(SkillLibrary))
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

		[Test] public void UpdatesHitPoints()
		{
			var mockClass = CreateMockClass();
			var original = ((ICharacter) new Character(SkillLibrary))
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

		[Test] public void UpdatesSkillPoints()
		{
			var mockClass = CreateMockClass();
			var original = ((ICharacter) new Character(SkillLibrary))
				.SetIntelligence(10)
				.AddClass(mockClass);

			var result = original.IncrementClass(mockClass);
			var resultCharacterClass = result.Classes.First();


			var expected = resultCharacterClass.Level * (SKILL_ADDEND + result.Intelligence.Modifier);
			Assert.AreEqual(expected, result.MaxSkillRanks);
		}
	}
}