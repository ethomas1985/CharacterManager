namespace Pathfinder.Interface
{
	public interface IMoney
	{
		int Platinum { get; }
		int Gold { get; }
		int Silver { get; }
		int Copper { get; }

		IMoney Add(int pCopper, int pSilver, int pGold, int pPlatinum);
	}
}