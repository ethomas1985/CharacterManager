namespace Pathfinder.Interface
{
	public interface IPurse
	{
		int Platinum { get; }
		int Gold { get; }
		int Silver { get; }
		int Copper { get; }

		IPurse Add(int pCopper, int pSilver, int pGold, int pPlatinum);
	}
}