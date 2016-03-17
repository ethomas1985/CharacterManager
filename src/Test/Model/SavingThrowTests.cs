using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Model;

namespace Test.Model
{
	[TestFixture]
	public class SavingThrowTests
	{
		[Test]
		public void Score()
		{
			var ability = new AbilityScore(AbilityType.Constitution, 10, 0);
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					ability,
					1,
					1,
					1,
					1);


			Assert.AreEqual(4, savingThrow.Score);
		}
		[Test]
		public void Base()
		{
			var ability = new AbilityScore(AbilityType.Constitution, 10, 0);
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					ability,
					1,
					1,
					1,
					1);


			Assert.AreEqual(1, savingThrow.Base);
		}
		[Test]
		public void Ability()
		{
			var ability = new AbilityScore(AbilityType.Constitution, 10, 0);
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					ability,
					1,
					1,
					1,
					1);


			Assert.AreEqual(AbilityType.Constitution, savingThrow.Ability);
		}
		[Test]
		public void AbilityModifier()
		{
			var ability = new AbilityScore(AbilityType.Constitution, 10, 0);
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					ability,
					1,
					1,
					1,
					1);


			Assert.AreEqual(ability.Modifier, savingThrow.AbilityModifier);
		}
		[Test]
		public void Resist()
		{
			var ability = new AbilityScore(AbilityType.Constitution, 12, 0);
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					ability,
					1,
					1,
					1,
					1);


			Assert.AreEqual(1, savingThrow.Resist);
		}
		[Test]
		public void Miscellaneous()
		{
			var ability = new AbilityScore(AbilityType.Constitution, 12, 0);
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					ability,
					1,
					1,
					1,
					1);

			Assert.AreEqual(1, savingThrow.Misc);
		}
		[Test]
		public void Temporary()
		{
			var ability = new AbilityScore(AbilityType.Constitution, 12, 0);
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					ability,
					1,
					1,
					1,
					1);


			Assert.AreEqual(1, savingThrow.Temporary);
		}
	}
}
