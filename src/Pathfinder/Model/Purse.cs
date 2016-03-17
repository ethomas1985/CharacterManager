using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Purse : IPurse
	{
		public const int COPPER_TO_SILVER = 10;
		public const int COPPER_TO_GOLD = 100;
		public const int COPPER_TO_PLATINUM = 1000;

		public Purse(int pCopper = 0, int pSilver = 0, int pGold = 0, int pPlatinum = 0)
		{
			Copper = pCopper;
			Copper += pSilver * COPPER_TO_SILVER;
			Copper += pGold * COPPER_TO_GOLD;
			Copper += pPlatinum * COPPER_TO_PLATINUM;
		}

		public int Copper { get; private set; } //TODO this needs to be the remainder after calculating the other denominations

		public int Silver => Copper / COPPER_TO_SILVER;
		public int Gold => Copper / COPPER_TO_GOLD;
		public int Platinum => Copper / COPPER_TO_PLATINUM;

		public IPurse Add(int pCopper, int pSilver, int pGold, int pPlatinum)
		{
			throw new System.NotImplementedException();
		}
	}
}
