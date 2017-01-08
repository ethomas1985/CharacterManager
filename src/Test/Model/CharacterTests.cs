using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Test.Mocks;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Model
{
	[TestFixture]
	public class CharacterTests
	{
		protected static ICharacter createCharacter(MockSkillLibrary pMockSkillLibrary)
		{
			return new Character(pMockSkillLibrary);
		}

		[TestFixture]
		public class SetRaceMethod : CharacterTests
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
						new Language("Test-ish"),
					});

				return race;
			}

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetRace(null));
			}

			[Test]
			public void SetsRace()
			{
				var skillLibrary = new MockSkillLibrary();

				var original = createCharacter(skillLibrary);

				var race = SetupMockRace();
				var result = original.SetRace(race);

				Assert.AreEqual(race, result.Race);
			}

			[Test]
			public void SetsLanguages()
			{
				var skillLibrary = new MockSkillLibrary();

				var original = createCharacter(skillLibrary);

				var race = SetupMockRace();
				var result = original.SetRace(race);

				Assert.IsTrue(!race.Languages.Except(result.Languages).Any());
			}

			[Test]
			public void RemovedPreviousRaceLanguages()
			{
				var skillLibrary = new MockSkillLibrary();

				var original =
					createCharacter(skillLibrary)
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

				var original = createCharacter(skillLibrary);

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
				var original =
					createCharacter(skillLibrary)
						.SetRace(originalRace);

				var race = SetupMockRace();
				original.SetRace(race);

				Assert.AreEqual(originalRace, original.Race);
			}
		}

		[TestFixture]
		public class SetNameMethod : CharacterTests
		{
			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetName(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());

				const string testName = "Test Name";
				var result = original.SetName(testName);

				Assert.AreEqual(testName, result.Name);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var skillLibrary = new MockSkillLibrary();

				var original = createCharacter(skillLibrary);

				const string testName = "Test Name";
				var result = original.SetName(testName);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var skillLibrary = new MockSkillLibrary();

				var original = createCharacter(skillLibrary);

				const string testName = "Test Name";
				original.SetName(testName);

				Assert.IsNull(original.Name);
			}
		}

		[TestFixture]
		public class SetAgeMethod : CharacterTests
		{
			[Test]
			public void Negative()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetAge(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetAge(30);

				Assert.AreEqual(30, result.Age);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetAge(30);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetAge(30);

				Assert.AreNotEqual(30, original.Age);
			}
		}

		[TestFixture]
		public class SetAlignmentMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetAlignment(Alignment.LawfulGood);

				Assert.AreEqual(Alignment.LawfulGood, result.Alignment);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetAlignment(Alignment.LawfulGood);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetAlignment(Alignment.LawfulGood);

				Assert.AreNotSame(Alignment.Neutral, original.Alignment);
			}
		}

		[TestFixture]
		public class SetHomelandMethod : CharacterTests
		{
			private const string TESTLANDIA = "Testlandia";

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetHomeland(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetHomeland(TESTLANDIA);

				Assert.AreEqual(TESTLANDIA, result.Homeland);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetHomeland(TESTLANDIA);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetHomeland(TESTLANDIA);

				Assert.IsNull(original.Homeland);
			}
		}

		[TestFixture]
		public class SetDeityMethod : CharacterTests
		{
			private readonly Deity _testingDeity = new Deity("Skepticus");

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetDeity(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetDeity(_testingDeity);

				Assert.AreEqual(_testingDeity, result.Deity);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetDeity(_testingDeity);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetDeity(_testingDeity);

				Assert.IsNull(original.Deity);
			}
		}

		[TestFixture]
		public class SetGenderMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetGender(Gender.Male);

				Assert.AreEqual(Gender.Male, result.Gender);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetGender(Gender.Male);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetGender(Gender.Male);

				Assert.AreEqual(Gender.Female, original.Gender);
			}
		}

		[TestFixture]
		public class SetEyesMethod : CharacterTests
		{
			private const string EYES = "Octarine";

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetEyes(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetHomeland(EYES);

				Assert.AreEqual(EYES, result.Homeland);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetEyes(EYES);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetEyes(EYES);

				Assert.IsNull(original.Name);
			}
		}

		[TestFixture]
		public class SetHairMethod : CharacterTests
		{
			private const string HAIR = "Octarine";

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetHair(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetHair(HAIR);

				Assert.AreEqual(HAIR, result.Hair);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetHair(HAIR);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetHair(HAIR);

				Assert.IsNull(original.Name);
			}
		}

		[TestFixture]
		public class SetHeightMethod : CharacterTests
		{
			private const string HEIGHT = "Over 9000";

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetHeight(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetHeight(HEIGHT);

				Assert.AreEqual(HEIGHT, result.Height);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetHeight(HEIGHT);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetHeight(HEIGHT);

				Assert.IsNull(original.Name);
			}
		}

		[TestFixture]
		public class SetWeightMethod : CharacterTests
		{
			private const string WEIGHT = "Over 9000";

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.SetWeight(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetWeight(WEIGHT);

				Assert.AreEqual(WEIGHT, result.Weight);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetWeight(WEIGHT);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetWeight(WEIGHT);

				Assert.IsNull(original.Name);
			}
		}

		[TestFixture]
		public class AddLanguageMethod : CharacterTests
		{
			private readonly Language _language = new Language("Middle Test-ese");

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.AddLanguage(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.AddLanguage(_language);

				Assert.IsTrue(result.Languages.Contains(_language));
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.AddLanguage(_language);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.AddLanguage(_language);

				Assert.IsFalse(original.Languages.Contains(_language));
			}
		}

		[TestFixture]
		public class RemoveLanguageMethod : CharacterTests
		{
			private readonly Language _language = new Language("Middle Test-ese");

			[Test]
			public void Null()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.RemoveLanguage(null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary()).AddLanguage(_language);
				Assert.IsTrue(original.Languages.Contains(_language));

				var result = original.RemoveLanguage(_language);

				Assert.IsFalse(result.Languages.Contains(_language));
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary()).AddLanguage(_language);
				Assert.IsTrue(original.Languages.Contains(_language));

				var result = original.RemoveLanguage(_language);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary()).AddLanguage(_language);
				Assert.IsTrue(original.Languages.Contains(_language));

				original.RemoveLanguage(_language);

				Assert.IsTrue(original.Languages.Contains(_language));
			}
		}

		[TestFixture]
		public class SetStrengthMethod : CharacterTests
		{
			[Test]
			public void InvalidValue()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetStrength(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetStrength(10);

				Assert.AreEqual(10, result.Strength.Base);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetStrength(10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetStrength(10);

				Assert.AreEqual(0, original.Strength.Base);
			}
		}

		[TestFixture]
		public class SetDexterityMethod : CharacterTests
		{
			[Test]
			public void InvalidValue()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetDexterity(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetDexterity(10);

				Assert.AreEqual(10, result.Dexterity.Base);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetDexterity(10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetDexterity(10);

				Assert.AreEqual(0, original.Dexterity.Base);
			}
		}

		[TestFixture]
		public class SetConstitutionMethod : CharacterTests
		{
			[Test]
			public void InvalidValue()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetConstitution(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetConstitution(10);

				Assert.AreEqual(10, result.Constitution.Base);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetConstitution(10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetConstitution(10);

				Assert.AreEqual(0, original.Constitution.Base);
			}
		}

		[TestFixture]
		public class SetIntelligenceMethod : CharacterTests
		{
			[Test]
			public void InvalidValue()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetIntelligence(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetIntelligence(10);

				Assert.AreEqual(10, result.Intelligence.Base);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetIntelligence(10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetIntelligence(10);

				Assert.AreEqual(0, original.Intelligence.Base);
			}
		}

		[TestFixture]
		public class SetWisdomMethod : CharacterTests
		{
			[Test]
			public void InvalidValue()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetWisdom(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetWisdom(10);

				Assert.AreEqual(10, result.Wisdom.Base);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetWisdom(10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetWisdom(10);

				Assert.AreEqual(0, original.Wisdom.Base);
			}
		}

		[TestFixture]
		public class SetCharismaMethod : CharacterTests
		{
			[Test]
			public void InvalidValue()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetCharisma(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.SetCharisma(10);

				Assert.AreEqual(10, result.Charisma.Base);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetCharisma(10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetCharisma(10);

				Assert.AreEqual(0, original.Charisma.Base);
			}
		}

		[TestFixture]
		public class AddClassMethod : CharacterTests
		{
			private const int SKILL_ADDEND = 5;
			private const int HIT_DIE_FACES = 6;

			private static MockClass CreateMockClass()
			{
				return new MockClass
				{
					Name = "Mock Class",
					Alignments = new HashSet<Alignment>
										{
											Alignment.ChaoticGood,
											Alignment.ChaoticNeutral,
											Alignment.ChaoticEvil
										},
					HitDie = new Die(HIT_DIE_FACES),
					SkillAddend = SKILL_ADDEND
				};
			}

			[Test]
			public void NullClass()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.AddClass(null));
			}

			[Test]
			public void InvalidLevel()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.AddClass(CreateMockClass(), -1, false, new List<int>()));
			}

			[Test]
			public void NullHitPoints()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.AddClass(CreateMockClass(), 1, false, null));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass);

				Assert.IsTrue(result.Classes.Any(x => x.Class.Equals(mockClass)));
			}

			[Test]
			public void Success_Level()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass);

				Assert.AreEqual(1, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.Level);
			}

			[Test]
			public void Success_IsFavored()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass);

				Assert.AreEqual(true, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.IsFavored);
			}

			[Test]
			public void Success_HitPoints()
			{
				var original =
					createCharacter(new MockSkillLibrary())
						.SetConstitution(10);
				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass);

				var expected = mockClass.HitDie.Faces + original.Constitution.Modifier;
				Assert.AreEqual(expected, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.HitPoints.Sum());
			}

			[Test]
			public void Success_MaxSkillRanks()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass);

				var expected = SKILL_ADDEND + original.Intelligence.Modifier;
				Assert.AreEqual(expected, result.MaxSkillRanks);
			}

			[Test]
			public void Success_Overload()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

				Assert.IsTrue(result.Classes.Any(x => x.Class.Equals(mockClass)));
			}

			[Test]
			public void Success_Overload_Level()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

				Assert.AreEqual(10, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.Level);
			}

			[Test]
			public void Success_Overload_IsFavored()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

				Assert.AreEqual(true, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.IsFavored);
			}

			[Test]
			public void Success_Overload_HitPoints()
			{
				var original =
					createCharacter(new MockSkillLibrary())
						.SetConstitution(10);

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

				Assert.AreEqual(30, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.HitPoints.Sum());
			}

			[Test]
			public void Success_Overload_MaxSkillRanks()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

				var expected = SKILL_ADDEND + original.Intelligence.Modifier;
				Assert.AreEqual(expected, result.MaxSkillRanks);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.AddClass(CreateMockClass());

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());

				var mockClass = CreateMockClass();
				original.AddClass(mockClass);

				Assert.IsFalse(original.Classes.Any(x => x.Class.Equals(mockClass)));
			}
		}

		[TestFixture]
		public class IncrementClassMethod : CharacterTests
		{
			private const int SKILL_ADDEND = 5;
			private const int HIT_DIE_FACES = 6;

			private static MockClass CreateMockClass()
			{
				return new MockClass
				{
					Name = "Mock Class",
					Alignments = new HashSet<Alignment>
										{
											Alignment.ChaoticGood,
											Alignment.ChaoticNeutral,
											Alignment.ChaoticEvil
										},
					HitDie = new Die(HIT_DIE_FACES),
					SkillAddend = SKILL_ADDEND
				};
			}

			[Test]
			public void NullClass()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentNullException>(() => original.IncrementClass(null));
			}

			[Test]
			public void NewClass()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<ArgumentException>(() => original.IncrementClass(new MockClass()));
			}

			[Test]
			public void Success()
			{
				var mockClass = CreateMockClass();
				var original = createCharacter(new MockSkillLibrary()).AddClass(mockClass);

				var firstClass = original.Classes.Select(x => x.Class).First();
				var result = original.IncrementClass(firstClass);

				Assert.IsNotNull(result);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var mockClass = CreateMockClass();
				var original = createCharacter(new MockSkillLibrary()).AddClass(mockClass);
				var result = original.IncrementClass(mockClass);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var mockClass = CreateMockClass();
				var original = createCharacter(new MockSkillLibrary()).AddClass(mockClass);
				var originalCharacterClass = original.Classes.First();

				var result = original.IncrementClass(mockClass);
				var resultCharacterClass = result.Classes.First();

				Assert.AreEqual(originalCharacterClass.Class, resultCharacterClass.Class);
				Assert.AreEqual(1, originalCharacterClass.Level);
			}

			[Test] public void IncrementsLevel()
			{
				var mockClass = CreateMockClass();
				var original = createCharacter(new MockSkillLibrary()).AddClass(mockClass);
				var originalCharacterClass = original.Classes.First();

				var result = original.IncrementClass(mockClass);
				var resultCharacterClass = result.Classes.First();

				Assert.AreEqual(originalCharacterClass.Level + 1, resultCharacterClass.Level);
			}

			[Test]
			public void UpdatesHitPointsWithDefault()
			{
				var mockClass = CreateMockClass();
				var original = 
					createCharacter(new MockSkillLibrary())
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
				var original =
					createCharacter(new MockSkillLibrary())
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
				var original =
					createCharacter(new MockSkillLibrary())
					.SetIntelligence(10)
					.AddClass(mockClass);

				var result = original.IncrementClass(mockClass);
				var resultCharacterClass = result.Classes.First();


				var expected = resultCharacterClass.Level * (SKILL_ADDEND + result.Intelligence.Modifier);
				Assert.AreEqual(expected, result.MaxSkillRanks);
			}
		}

		[TestFixture]
		public class SetDamageMethod : CharacterTests
		{
			[Test]
			public void Fail()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.Throws<Exception>(() => original.SetDamage(-1));
			}

			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetDamage(10);

				Assert.AreEqual(10, result.Damage);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetDamage(10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetDamage(10);

				Assert.AreEqual(0, original.Damage);
			}
		}

		[TestFixture]
		public class AddDamageMethod : CharacterTests
		{
			[Test]
			public void TakeDamage()
			{
				var original = createCharacter(new MockSkillLibrary()).SetDamage(10);

				var result = original.AddDamage(5);

				Assert.AreEqual(15, result.Damage);
			}

			[Test]
			public void HealDamage()
			{
				var original = createCharacter(new MockSkillLibrary()).SetDamage(10);

				var result = original.AddDamage(-5);

				Assert.AreEqual(5, result.Damage);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary()).SetDamage(10);

				var result = original.AddDamage(-1);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary()).SetDamage(10);
				original.AddDamage(-1);

				Assert.IsNull(original.Name);
			}
		}

		[TestFixture]
		public class AppendExperienceMethod : CharacterTests
		{
			[Test]
			public void NotNull()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.IsNotNull(original.Experience);
			}

			[Test]
			public void InitializedToZero()
			{
				var original = createCharacter(new MockSkillLibrary());

				Assert.AreEqual(0, original.Experience.Total);
			}

			[Test]
			public void FailWithNullEvent()
			{
				var original = createCharacter(new MockSkillLibrary());

				IEvent nullEvent = null;
				Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullEvent));
			}

			[Test]
			public void SuccessWithEvent()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.AppendExperience(new Event("Test", "Test", 10));

				Assert.AreEqual(10, result.Experience.Total);
			}

			[Test]
			public void ReturnsNewInstanceWithEvent()
			{
				var original = createCharacter(new MockSkillLibrary());

				var experience = new Event("Test", "Test", 10);
				var result = original.AppendExperience(experience);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchangedWithEvent()
			{
				var original = createCharacter(new MockSkillLibrary());
				var experience = new Event("Test", "Test", 10);
				original.AppendExperience(experience);

				Assert.AreEqual(0, original.Experience.Total);
			}

			[Test]
			public void FailWithNullExperience()
			{
				var original = createCharacter(new MockSkillLibrary());

				IExperience nullExperience = null;
				Assert.Throws<ArgumentNullException>(() => original.AppendExperience(nullExperience));
			}

			[Test]
			public void SuccessWithExperience()
			{
				var original = createCharacter(new MockSkillLibrary());

				var experience = new Experience()
					.Append(new Event("Test", "Test", 10));
				var result = original.AppendExperience(experience);

				Assert.AreEqual(10, result.Experience.Total);
			}

			[Test]
			public void ReturnsNewInstanceWithEmptyExperience()
			{
				var original = createCharacter(new MockSkillLibrary());

				var experience = new Experience()
					.Append(new Event("Test", "Test", 10));
				var result = original.AppendExperience(experience);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchangedWithEmptyExperience()
			{
				var original = createCharacter(new MockSkillLibrary());

				var experience = new Experience()
					.Append(new Event("Test", "Test", 10));
				var result = original.AppendExperience(experience);

				Assert.AreEqual(0, original.Experience.Count());
			}
		}

		[Ignore("Method Not Yet Implemented.")]
		[TestFixture]
		public class SetSkillMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				Assert.Fail("Not Yet Implemented");
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetSkill(null);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetSkill(null);

				Assert.IsNull(original.Name);
			}
		}

		[Ignore("Method Not Yet Implemented.")]
		[TestFixture]
		public class AssignSkillPointMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				Assert.Fail("Not Yet Implemented");
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.AssignSkillPoint(null, -1);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.AssignSkillPoint(null, -1);

				Assert.IsNull(original.Name);
			}
		}

		[Ignore("Method Not Yet Implemented.")]
		[TestFixture]
		public class AddFeatMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				Assert.Fail("Not Yet Implemented");
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.AddFeat(null);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.AddFeat(null);

				Assert.IsNull(original.Name);
			}
		}

		[TestFixture]
		public class SetPurseMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetPurse(10, 10, 10, 10);

				Assert.AreEqual(new Copper(10), result.Purse.Copper);
				Assert.AreEqual(new Silver(10), result.Purse.Silver);
				Assert.AreEqual(new Gold(10), result.Purse.Gold);
				Assert.AreEqual(new Platinum(10), result.Purse.Platinum);
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetPurse(10, 10, 10, 10);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetPurse(10, 10, 10, 10);

				Assert.IsNull(original.Name);
			}
		}

		[Ignore("Method Not Yet Implemented.")]
		[TestFixture]
		public class SetInventoryMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				Assert.Fail("Not Yet Implemented");
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());

				var result = original.SetInventory(null);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.SetInventory(null);

				Assert.IsNull(original.Name);
			}
		}

		[Ignore("Method Not Yet Implemented.")]
		[TestFixture]
		public class EquipArmorMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				Assert.Fail("Not Yet Implemented");
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.EquipArmor(null);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.EquipArmor(null);

				Assert.IsNull(original.Name);
			}
		}

		[Ignore("Method Not Yet Implemented.")]
		[TestFixture]
		public class ReplaceArmorMethod : CharacterTests
		{
			[Test]
			public void Success()
			{
				Assert.Fail("Not Yet Implemented");
			}

			[Test]
			public void ReturnsNewInstance()
			{
				var original = createCharacter(new MockSkillLibrary());
				var result = original.ReplaceArmor(null, null);

				Assert.AreNotSame(original, result);
			}

			[Test]
			public void OriginalUnchanged()
			{
				var original = createCharacter(new MockSkillLibrary());
				original.ReplaceArmor(null, null);

				Assert.IsNull(original.Name);
			}
		}
	}
}
