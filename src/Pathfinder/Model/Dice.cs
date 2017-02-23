using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Dice: IDice
	{
		public Dice(int pDieCount, IDie pDie)
		{
			DieCount = pDieCount;
			Die = pDie;
		}

		public int DieCount { get; }
		public IDie Die { get; }
	}
}
