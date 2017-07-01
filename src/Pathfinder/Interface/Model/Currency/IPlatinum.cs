namespace Pathfinder.Interface.Model.Currency
{
	public interface IPlatinum : ICurrency
	{
		ICopper ToCopper();
		ISilver ToSilver();
		IGold ToGold();

		IPlatinum Add(IPlatinum pPlatinum);
		IPlatinum Subtract(IPlatinum pPlatinum);
	}
}