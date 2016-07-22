namespace Pathfinder.Interface
{
	public interface IWeaponDamage
	{
		int DieCount { get; }
		IDie Die { get; }
	}
}