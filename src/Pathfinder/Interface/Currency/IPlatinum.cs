using Pathfinder.Model;

namespace Pathfinder.Interface.Currency
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