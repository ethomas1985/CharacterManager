using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Pathfinder.Enums;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetRaceMethod
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
		public void Null()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.SetRace(null));
		}

		[Test]
		public void SetsRace()
		{
			var skillLibrary = SkillRepository;

			var original = (ICharacter)new Character(skillLibrary);

			var race = RaceMother.Create();
			var result = original.SetRace(race);

			Assert.AreEqual(race, result.Race);
		}

		[Test]
		public void SetsLanguages()
		{
			var skillLibrary = SkillRepository;

			var original = (ICharacter)new Character(skillLibrary);

			var race = RaceMother.Create();
			var result = original.SetRace(race);

			Assert.IsTrue(!race.Languages.Except(result.Languages).Any());
		}

		[Test]
		public void SetsRacialEffects()
		{
			var skillLibrary = SkillRepository;

			var original = (ICharacter)new Character(skillLibrary);

			var race = RaceMother.Create();
			var result = original.SetRace(race);

			Assert.That(
				result.Effects,
					Is.EquivalentTo(new IEffect[]
					{
						new RacialEffect(
							"+1 Strength",
							EffectType.Untyped,
							"+1 Strength",
							new Dictionary<string, int>
							{
								[nameof(AbilityType.Strength)] = 1
							}),
						new RacialEffect(
							"+5 Strength",
							EffectType.Circumstance,
							"+5 Strength",
							new Dictionary<string, int>
							{
								[nameof(AbilityType.Strength)] = 5
							}),
					}));

		}

		[Test]
		public void RemovedPreviousRaceLanguages()
		{
			var skillLibrary = SkillRepository;

			var original = new Character(skillLibrary)
					.SetRace(RaceMother.Create());

			var race = RaceMother.Create();
			var result = original.SetRace(race);

			Assert.IsTrue(!race.Languages.Except(result.Languages).Any());
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = SkillRepository;

			var original = (ICharacter)new Character(skillLibrary);

			var race = RaceMother.Create();
			var result = original.SetRace(race);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = SkillRepository;

			var originalRace = RaceMother.Create();
			var original = new Character(skillLibrary)
					.SetRace(originalRace);

			var race = RaceMother.Create();
			original.SetRace(race);

			Assert.AreEqual(originalRace, original.Race);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var race = RaceMother.Create();
			var result = original.SetRace(race);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new RaceSet(original.Id, 1, race),
					}));
		}
	}
}
