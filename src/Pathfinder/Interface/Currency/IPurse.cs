namespace Pathfinder.Interface.Currency
{
	public interface IPurse
	{
		IPlatinum Platinum { get; }
		IGold Gold { get; }
		ISilver Silver { get; }
		ICopper Copper { get; }

		IPurse Add(ICopper pCopper, ISilver pSilver, IGold pGold, IPlatinum pPlatinum);
		IPurse Subtract(ICopper pCopper, ISilver pSilver, IGold pGold, IPlatinum pPlatinum);
	}
}