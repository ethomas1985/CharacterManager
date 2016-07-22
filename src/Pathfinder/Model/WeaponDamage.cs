using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class WeaponDamage : IWeaponDamage
	{
		public WeaponDamage(int pDieCount, IDie pDie)
		{
			DieCount = pDieCount;
			Die = pDie;
		}

		public int DieCount { get; }
		public IDie Die { get; }
	}
}
