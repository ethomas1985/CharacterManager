using System;
using Pathfinder.Enum;
using Pathfinder.Model;
using NUnit.Framework;

namespace Test.Model
{
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
	}
}
