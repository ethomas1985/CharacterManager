using System;
using System.Collections;
using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Test.Model
{
	[TestFixture]
	public class DefenseScoreTests
	{
		[TestFixture]
		public class ConstructorTests : DefenseScoreTests
		{

		}

		[TestFixture]
		public class ScorePropertyTests : DefenseScoreTests
		{
			[Test]
			[TestCaseSource(
				typeof(DefenseScoreTestCase), 
				nameof(DefenseScoreTestCase.ScoreCases))]
			public int Getter(
				DefensiveType pType,
				IAbilityScore pDexterityScore,
				Size pSize,
				int pArmorBonus,
				int pShieldBonus,
				int pNatural,
				int pDeflect,
				int pDodge,
				int pMisc,
				int pTemporary)
			{
				return new DefenseScore(
					pType, 
					pDexterityScore,
					() => (int) pSize,
					() => pArmorBonus,
					() => pShieldBonus)
				{
					Natural = pNatural,
					Deflect = pDeflect,
					Dodge = pDodge,
					MiscModifier = pMisc,
					Temporary = pTemporary
				}.Score;
			}

			[Test]
			[TestCaseSource(
				typeof(DefenseScoreTestCase),
				nameof(DefenseScoreTestCase.CombatManeuverDefenseScoreCases))]
			public int Getter_CombatManueverDefense(
				IAbilityScore pDexterityScore,
				IAbilityScore pStrengthScore,
				Size pSize,
				int pBaseAttackBonus,
				int pNatural,
				int pDeflect,
				int pDodge,
				int pMisc,
				int pTemporary)
			{
				return new DefenseScore(pDexterityScore, pStrengthScore, () => (int)pSize, () => pBaseAttackBonus)
				{
					Natural = pNatural,
					Deflect = pDeflect,
					Dodge = pDodge,
					MiscModifier = pMisc,
					Temporary = pTemporary
				}.Score;
			}

		}

		[TestFixture]
		public class IndexerTests : DefenseScoreTests
		{
			[TestFixture]
			public class GetterTests : IndexerTests
			{
				[Test]
				public void GetScore()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var size = new Func<int>(() => (int) Size.Small);

					var defenseScore = 
						new DefenseScore(
							DefensiveType.ArmorClass, 
							dexScore, 
							size, 
							() => 1,
							() => 1);

					Assert.AreEqual(13, defenseScore[nameof(IDefenseScore.Score)]);
				}

				[Test]
				public void GetDexterityModifier()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var size = new Func<int>(() => (int) Size.Small);

					var defenseScore = new DefenseScore(DefensiveType.ArmorClass, dexScore, size, null, null);

					Assert.AreEqual(dexScore.Modifier, defenseScore[nameof(IDefenseScore.DexterityModifier)]);
				}

				[Test]
				public void GetStrengthModifier()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var strScore = new AbilityScore(AbilityType.Strength) { Base = 10 };
					var size = new Func<int>(() => (int)Size.Small);
					var bab = new Func<int>(() => 1);

					var defenseScore = new DefenseScore(dexScore, strScore, size, bab);

					Assert.AreEqual(strScore.Modifier, defenseScore[nameof(IDefenseScore.StrengthModifier)]);
				}

				[Test]
				public void GetBaseAttackBonus()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var strScore = new AbilityScore(AbilityType.Strength) { Base = 10 };
					var size = new Func<int>(() => (int)Size.Small);
					var bab = new Func<int>(() => 1);

					var defenseScore = new DefenseScore(dexScore, strScore, size, bab);

					Assert.AreEqual(1, defenseScore[nameof(IDefenseScore.BaseAttackBonus)]);
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetSizeModifier(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null);

					return defenseScore[nameof(IDefenseScore.SizeModifier)];
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetArmorBonus(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, () => pValue, null);

					return defenseScore[nameof(IDefenseScore.ArmorBonus)];
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetShieldBound(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, () => pValue);

					return defenseScore[nameof(IDefenseScore.ShieldBonus)];
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetNatural(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null)
					{
						Natural = pValue
					};

					return defenseScore[nameof(IDefenseScore.Natural)];
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetDeflect(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null)
					{
						Deflect = pValue
					};

					return defenseScore[nameof(IDefenseScore.Deflect)];
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetDodge(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null)
					{
						Dodge = pValue
					};

					return defenseScore[nameof(IDefenseScore.Dodge)];
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetMiscModifier(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null)
					{
						MiscModifier = pValue
					};

					return defenseScore[nameof(IDefenseScore.MiscModifier)];
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int GetTemporary(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null)
					{
						Temporary = pValue
					};

					return defenseScore[nameof(IDefenseScore.Temporary)];
				}
			}

			[TestFixture]
			public class SetterTests : IndexerTests
			{
				[Test]
				public void SetScore()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var size = new Func<int>(() => (int) Size.Small);
					var defenseScore = new DefenseScore(DefensiveType.ArmorClass, dexScore, size, null, null);

					Assert.That(
						() => defenseScore[nameof(IDefenseScore.Score)] = 1,
						Throws.TypeOf<ArgumentException>());
				}

				[Test]
				public void SetDexterityModifier()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var size = new Func<int>(() => (int) Size.Small);

					var defenseScore = new DefenseScore(DefensiveType.ArmorClass, dexScore, size, null, null);
					Assert.That(
						() => defenseScore[nameof(IDefenseScore.Score)] = 1,
						Throws.TypeOf<ArgumentException>());
				}

				[Test]
				public void SetSizeModifier()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var size = new Func<int>(() => (int) Size.Small);

					var defenseScore = new DefenseScore(DefensiveType.ArmorClass, dexScore, size, null, null);

					Assert.That(
						() => defenseScore[nameof(IDefenseScore.Score)] = 1,
						Throws.TypeOf<ArgumentException>());
				}
				[Test]
				public void SetStrengthModifier()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var strScore = new AbilityScore(AbilityType.Strength) { Base = 10 };
					var size = new Func<int>(() => (int)Size.Small);
					var bab = new Func<int>(() => 10);

					var defenseScore = new DefenseScore(dexScore, strScore, size, bab);
					Assert.That(
						() => defenseScore[nameof(IDefenseScore.StrengthModifier)] = 1,
						Throws.TypeOf<ArgumentException>());
				}

				[Test]
				public void SetBaseAttackBonus()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var strScore = new AbilityScore(AbilityType.Strength) { Base = 10 };
					var size = new Func<int>(() => (int)Size.Small);
					var bab = new Func<int>(() => 10);

					var defenseScore = new DefenseScore(dexScore, strScore, size, bab);
					Assert.That(
						() => defenseScore[nameof(IDefenseScore.BaseAttackBonus)] = 1,
						Throws.TypeOf<ArgumentException>());
				}

				[Test]
				public void SetArmorBonus()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var strScore = new AbilityScore(AbilityType.Strength) { Base = 10 };
					var size = new Func<int>(() => (int)Size.Small);
					var bab = new Func<int>(() => 10);

					var defenseScore = new DefenseScore(dexScore, strScore, size, bab);
					Assert.That(
						() => defenseScore[nameof(IDefenseScore.ArmorBonus)] = 1,
						Throws.TypeOf<ArgumentException>());
				}

				[Test]
				public void SetShieldBonus()
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var strScore = new AbilityScore(AbilityType.Strength) { Base = 10 };
					var size = new Func<int>(() => (int)Size.Small);
					var bab = new Func<int>(() => 10);

					var defenseScore = new DefenseScore(dexScore, strScore, size, bab);
					Assert.That(
						() => defenseScore[nameof(IDefenseScore.ShieldBonus)] = 1,
						Throws.TypeOf<ArgumentException>());
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int SetDeflect(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null);

					defenseScore[nameof(IDefenseScore.Deflect)] = pValue;

					return defenseScore.Deflect;
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int SetDodge(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null);

					defenseScore[nameof(IDefenseScore.Dodge)] = pValue;

					return defenseScore.Dodge;
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int SetMiscModifier(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null);

					defenseScore[nameof(IDefenseScore.MiscModifier)] = pValue;

					return defenseScore.MiscModifier;
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int SetNatural(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null);

					defenseScore[nameof(IDefenseScore.Natural)] = pValue;

					return defenseScore.Natural;
				}

				[Test]
				[TestCaseSource(typeof(DefenseScoreTestCase), nameof(DefenseScoreTestCase.IndexerCases))]
				public int SetTemporory(
					DefensiveType pType,
					IAbilityScore pDexterityScore,
					Func<int> pSize,
					int pValue)
				{
					var defenseScore = new DefenseScore(pType, pDexterityScore, pSize, null, null);

					defenseScore[nameof(IDefenseScore.Temporary)] = pValue;

					return defenseScore.Temporary;
				}
			}
		}

		public static class DefenseScoreTestCase
		{
			public static IEnumerable ScoreCases
			{
				get
				{
					const Size size = Size.Medium;
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };

					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Throws(typeof(Exception));
					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 1, 0, 0, 0, 0, 0, 0).Throws(typeof(Exception));
					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 0, 1, 0, 0, 0, 0, 0).Throws(typeof(Exception));
					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 0, 0, 1, 0, 0, 0, 0).Throws(typeof(Exception));
					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 0, 0, 0, 1, 0, 0, 0).Throws(typeof(Exception));
					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Throws(typeof(Exception));
					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 0, 0, 0, 0, 0, 1, 0).Throws(typeof(Exception));
					//yield return new TestCaseData(DefensiveType.INVALID, dexScore, size, 0, 0, 0, 0, 0, 0, 1).Throws(typeof(Exception));

					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 1, 0, 0, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 1, 0, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 1, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 1, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 1, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 0, 0, 0, 0, 0, 0, 1).Returns(11);

					// Touch ignores Armor, Shield, and Natural 
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 1, 0, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 1, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.Touch, dexScore, size, 1, 1, 1, 0, 0, 0, 1).Returns(11);

					// Flat-footed ignores Dexterity and Dodge
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 1, 0, 0, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 1, 0, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 1, 0, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 1, 1, 0, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 1, 0, 0).Returns(10);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 1, 1, 0).Returns(11);
					yield return new TestCaseData(DefensiveType.FlatFooted, dexScore, size, 0, 0, 0, 0, 1, 0, 1).Returns(11);
				}
			}

			public static IEnumerable CombatManeuverDefenseScoreCases
			{
				get
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var strScore = new AbilityScore(AbilityType.Strength) { Base = 11 };
					var size = Size.Medium;

					// Combat Maneuver Defense uses BaseAttackBonus and Strength Modifier and ignore Natural 
					yield return new TestCaseData(dexScore, strScore, size, 0, 0, 0, 0, 0, 0).Returns(10);
					yield return new TestCaseData(dexScore, strScore, size, 1, 1, 0, 0, 0, 0).Returns(12);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 0, 0, 0).Returns(11);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 1, 0, 0, 0).Returns(12);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 1, 0, 0).Returns(12);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 0, 1, 0).Returns(12);
					yield return new TestCaseData(dexScore, strScore, size, 0, 1, 0, 0, 0, 1).Returns(12);
				}
			}

			public static IEnumerable ConstructorCases
			{
				get
				{
					yield return new TestCaseData(
						DefensiveType.INVALID,
						new AbilityScore(AbilityType.Dexterity) { Base = 1 },
						new Func<int>(() => (int) Size.Small));
				}
			}

			public static IEnumerable IndexerCases
			{
				get
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var size = new Func<int>(() => (int) Size.Small);

					yield return new TestCaseData(DefensiveType.ArmorClass, dexScore, size, 1).Returns(1);
				}
			}
		}
	}
}
