using System;
using System.Collections;
using NUnit.Core;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model;
using NUnit.Framework;

namespace Test.Model
{
	/// <summary>
	/// Summary description for SavingThrowTests
	/// </summary>
	[TestFixture]
	public class SavingThrowTests
	{
		[TestFixture]
		public class ScoreReadonlyProperty : SavingThrowTests
		{
			[Test]
			public void Expected()
			{
				var ability = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
				var getBase = new Func<int>(() => 1);
				var getResist = new Func<int>(() => 1);
				var getTemporary = new Func<int>(() => 1);
				var savingThrow =
					new SavingThrow(
						SavingThrowType.Fortitude,
						ability,
						getBase, getResist,
						getTemporary);


				Assert.AreEqual(4, savingThrow.Score);
			}
		}

		[TestFixture]
		public class BaseReadonlyProperty : SavingThrowTests
		{
			[Test]
			public void Expected()
			{
				var ability = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
				var getBase = new Func<int>(() => 1);
				var getResist = new Func<int>(() => 1);
				var getTemporary = new Func<int>(() => 1);
				var savingThrow =
					new SavingThrow(
						SavingThrowType.Fortitude,
						ability,
						getBase, getResist,
						getTemporary);


				Assert.AreEqual(1, savingThrow.Base);
			}
		}

		[TestFixture]
		public class AbilityReadonlyProperty : SavingThrowTests
		{
			[Test]
			public void Expected()
			{
				var ability = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
				var getBase = new Func<int>(() => 1);
				var getResist = new Func<int>(() => 1);
				var getTemporary = new Func<int>(() => 1);
				var savingThrow =
					new SavingThrow(
						SavingThrowType.Fortitude,
						ability,
						getBase, getResist,
						getTemporary);


				Assert.AreEqual(AbilityType.Dexterity, savingThrow.Ability);
			}
		}

		[TestFixture]
		public class AbilityModifierReadonlyProperty : SavingThrowTests
		{
			[Test]
			public void Expected()
			{
				var ability = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
				var getBase = new Func<int>(() => 1);
				var getResist = new Func<int>(() => 1);
				var getTemporary = new Func<int>(() => 1);
				var savingThrow =
					new SavingThrow(
						SavingThrowType.Fortitude,
						ability,
						getBase, getResist,
						getTemporary);


				Assert.AreEqual(ability.Modifier, savingThrow.AbilityModifier);
			}
		}

		[TestFixture]
		public class ResistProperty : SavingThrowTests
		{
			[Test]
			public void Expected()
			{
				var ability = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
				var getBase = new Func<int>(() => 1);
				var getResist = new Func<int>(() => 1);
				var getTemporary = new Func<int>(() => 1);
				var savingThrow =
					new SavingThrow(
						SavingThrowType.Fortitude,
						ability,
						getBase, getResist,
						getTemporary);


				Assert.AreEqual(1, savingThrow.Resist);
			}
		}

		[TestFixture]
		public class MiscProperty : SavingThrowTests
		{
			[Test]
			public void Expected()
			{
				var ability = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
				var getBase = new Func<int>(() => 1);
				var getResist = new Func<int>(() => 1);
				var getTemporary = new Func<int>(() => 1);
				var savingThrow =
					new SavingThrow(
						SavingThrowType.Fortitude,
						ability,
						getBase, getResist,
						getTemporary)
					{
						Misc = 1
					};

				Assert.AreEqual(1, savingThrow.Misc);
			}
		}

		[TestFixture]
		public class TemporaryProperty : SavingThrowTests
		{
			[Test]
			public void Expected()
			{
				var ability = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
				var getBase = new Func<int>(() => 1);
				var getResist = new Func<int>(() => 1);
				var getTemporary = new Func<int>(() => 1);
				var savingThrow =
					new SavingThrow(
						SavingThrowType.Fortitude,
						ability,
						getBase, getResist,
						getTemporary);


				Assert.AreEqual(1, savingThrow.Temporary);
			}
		}

		[TestFixture]
		public class IndexerTests : SavingThrowTests
		{
			[TestFixture]
			public class GetterClasses : IndexerTests
			{
				[Test]
				[TestCaseSource(typeof(TestCases), nameof(TestCases.IndexerGetterCases))]
				public int GetProperty(
					string pIndex,
					SavingThrowType pType,
					IAbilityScore pAbility,
					Func<int> pGetBase,
					Func<int> pGetResist,
					Func<int> pGetTemporary,
					Action<ISavingThrow> pSetter)
				{
					var savingThrow =
						new SavingThrow(
							pType,
							pAbility,
							pGetBase,
							pGetResist,
							pGetTemporary);
					pSetter(savingThrow);

					return savingThrow[pIndex];
				}
			}

			[TestFixture]
			public class SetterClasses : IndexerTests
			{
				[Test]
				[TestCaseSource(typeof(TestCases), nameof(TestCases.IndexerSetterCases))]
				public object SetProperty(
					string pIndex,
					SavingThrowType pType,
					IAbilityScore pAbility,
					Func<int> pGetBase,
					Func<int> pGetResist,
					Func<int> pGetTemporary,
					int pValue)
				{
					var savingThrow = 
						new SavingThrow(
							pType,
							pAbility,
							pGetBase,
							pGetResist,
							pGetTemporary)
					{
						[pIndex] = pValue
					};

					return savingThrow[pIndex];
				}
			}
		}

		public static class TestCases
		{
			public static IEnumerable IndexerGetterCases
			{
				get
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 12 };
					var getBase = new Func<int>(() => 1);
					var getResist = new Func<int>(() => 1);
					var getTemporary = new Func<int>(() => 1);

					yield return
						new TestCaseData(
							nameof(SavingThrow.Score),
							SavingThrowType.Fortitude,
							dexScore,
							getBase,
							getResist,
							getTemporary,
							new Action<ISavingThrow>(pThrow => { }))
							.Returns(4);
					yield return
						new TestCaseData(
							nameof(SavingThrow.Base),
							SavingThrowType.Fortitude,
							dexScore,
							getBase,
							getResist,
							getTemporary,
							new Action<ISavingThrow>(pThrow => { }))
							.Returns(1);
					yield return
						new TestCaseData(
							nameof(SavingThrow.Ability),
							SavingThrowType.Fortitude,
							dexScore,
							getBase,
							getResist,
							getTemporary,
							new Action<ISavingThrow>(pThrow => { }))
							.Throws(typeof(ArgumentException));
					yield return
						new TestCaseData(
							nameof(SavingThrow.AbilityModifier),
							SavingThrowType.Fortitude,
							dexScore,
							getBase,
							getResist,
							getTemporary,
							new Action<ISavingThrow>(pThrow => { }))
							.Returns(1);
					yield return
						new TestCaseData(
							nameof(SavingThrow.Resist),
							SavingThrowType.Fortitude,
							dexScore,
							getBase,
							getResist,
							getTemporary,
							new Action<ISavingThrow>(pThrow => { }))
							.Returns(1);
					yield return
						new TestCaseData(
							nameof(SavingThrow.Misc),
							SavingThrowType.Fortitude,
							dexScore,
							getBase,
							getResist,
							getTemporary,
							new Action<ISavingThrow>(pThrow => ((SavingThrow) pThrow).Misc = 1))
							.Returns(1);
					yield return
						new TestCaseData(
							nameof(SavingThrow.Temporary),
							SavingThrowType.Fortitude,
							dexScore,
							getBase,
							getResist,
							getTemporary,
							new Action<ISavingThrow>(pThrow => { }))
							.Returns(1);
				}
			}

			public static IEnumerable IndexerSetterCases
			{
				get
				{
					var dexScore = new AbilityScore(AbilityType.Dexterity) { Base = 10 };
					var getBase = new Func<int>(() => 1);
					var getResist = new Func<int>(() => 1);
					var getTemporary = new Func<int>(() => 1);

					yield return
						new TestCaseData(
							nameof(SavingThrow.Score), SavingThrowType.Fortitude, dexScore, getBase, getResist, getTemporary, 1)
							.Throws(typeof(ArgumentException));
					yield return
						new TestCaseData(
							nameof(SavingThrow.Base), SavingThrowType.Fortitude, dexScore, getBase, getResist, getTemporary, 1)
							.Throws(typeof(ArgumentException));
					yield return
						new TestCaseData(
							nameof(SavingThrow.Ability), SavingThrowType.Fortitude, dexScore, getBase, getResist, getTemporary, 1)
							.Throws(typeof(ArgumentException));
					yield return
						new TestCaseData(
							nameof(SavingThrow.AbilityModifier), SavingThrowType.Fortitude, dexScore, getBase, getResist, getTemporary, 1)
							.Throws(typeof(ArgumentException));
					yield return
						new TestCaseData(
							nameof(SavingThrow.Resist), SavingThrowType.Fortitude, dexScore, getBase, getResist, getTemporary, 1)
							.Throws(typeof(ArgumentException));
					yield return
						new TestCaseData(
							nameof(SavingThrow.Misc), SavingThrowType.Fortitude, dexScore, getBase, getResist, getTemporary, 1)
							.Returns(1);
					yield return
						new TestCaseData(
							nameof(SavingThrow.Temporary), SavingThrowType.Fortitude, dexScore, getBase, getResist, getTemporary, 1)
							.Throws(typeof(ArgumentException));
				}
			}
		}
	}
}
