using NUnit.Framework;
using Pathfinder.Model.Currency;

namespace Test.Model.Currency
{
	[TestFixture]
	public class PurseTests
	{
		[TestFixture]
		public class ConstructorTests : PurseTests
		{
			[Test]
			public void CopperValue()
			{
				const int expected = 1000;
				var money = new Purse(1000);

				Assert.AreEqual(expected, money.Copper.Value);
			}

			[Test]
			public void SilverValue()
			{
				const int expected = 100;
				var money = new Purse(0, 100);

				Assert.AreEqual(expected, money.Silver.Value);
			}

			[Test]
			public void GoldValue()
			{
				const int expected = 10;
				var money = new Purse(0, 0, 10);

				Assert.AreEqual(expected, money.Gold.Value);
			}

			[Test]
			public void PlatinumValue()
			{
				const int expected = 1;
				var money = new Purse(0, 0, 0, 1);

				Assert.AreEqual(expected, money.Platinum.Value);
			}

			[Test]
			public void Copper()
			{
				const int expected = 1000;
				var money = new Purse(new Copper(expected));

				Assert.AreEqual(expected, money.Copper.Value);
			}

			[Test]
			public void Silver()
			{
				const int expected = 100;
				var money = new Purse(pSilver: new Silver(expected));

				Assert.AreEqual(expected, money.Silver.Value);
			}

			[Test]
			public void Gold()
			{
				const int expected = 10;
				var money = new Purse(pGold: new Gold(expected));

				Assert.AreEqual(expected, money.Gold.Value);
			}

			[Test]
			public void Platinum()
			{
				const int  expected = 1;
				var money = new Purse(pPlatinum: new Platinum(expected));

				Assert.AreEqual(expected, money.Platinum.Value);
			}
		}
	}
}
