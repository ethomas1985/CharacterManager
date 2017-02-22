using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetRaceMethod
	{
		private static IRace SetupMockRace()
		{
			var race = new Race(
							    "Test Race",
							    "Testy",
							    "This is a Test Race",
							    Size.Medium,
							    30,
							    new Dictionary<AbilityType, int>(),
							    new List<ITrait>(),
							    new List<ILanguage>
							    {
								    new Language("Test-ese"),
								    new Language("Test-ish")
							    });

			return race;
		}

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.SetRace(null));
		}

		[Test]
		public void SetsRace()
		{
			var skillLibrary = new MockSkillLibrary();

			var original = (ICharacter) new Character(skillLibrary);

			var race = SetupMockRace();
			var result = original.SetRace(race);

			Assert.AreEqual(race, result.Race);
		}

		[Test]
		public void SetsLanguages()
		{
			var skillLibrary = new MockSkillLibrary();

			var original = (ICharacter) new Character(skillLibrary);

			var race = SetupMockRace();
			var result = original.SetRace(race);

			Assert.IsTrue(!race.Languages.Except(result.Languages).Any());
		}

		[Test]
		public void RemovedPreviousRaceLanguages()
		{
			var skillLibrary = new MockSkillLibrary();

			var original = ((ICharacter) new Character(skillLibrary))
					.SetRace(
							 new Race(
									  "Test Race",
									  "Testy",
									  "This is a Test Race",
									  Size.Medium,
									  30,
									  new Dictionary<AbilityType, int>(),
									  new List<ITrait>(),
									  new List<ILanguage>
									  {
										  new Language("Old Test-ese"),
										  new Language("Old Test-ish")
									  }));

			var race = SetupMockRace();
			var result = original.SetRace(race);

			Assert.IsTrue(!race.Languages.Except(result.Languages).Any());
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var skillLibrary = new MockSkillLibrary();

			var original = (ICharacter) new Character(skillLibrary);

			var race = SetupMockRace();
			var result = original.SetRace(race);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var skillLibrary = new MockSkillLibrary();

			var originalRace = new Race(
									    "Test Race",
									    "Testy",
									    "This is a Test Race",
									    Size.Medium,
									    30,
									    new Dictionary<AbilityType, int>(),
									    new List<ITrait>(),
									    new List<ILanguage>
									    {
										    new Language("Old Test-ese"),
										    new Language("Old Test-ish")
									    });
			var original = ((ICharacter) new Character(skillLibrary))
					.SetRace(originalRace);

			var race = SetupMockRace();
			original.SetRace(race);

			Assert.AreEqual(originalRace, original.Race);
		}
	}
}
