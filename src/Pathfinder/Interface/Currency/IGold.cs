using Pathfinder.Model;

namespace Pathfinder.Interface.Currency
{
	public interface IGold : ICurrency
	{
		ICopper ToCopper();
		ISilver ToSilver();

		IPlatinum ToPlatinum();

		IGold Add(IGold pGold);
		IGold Subtract(IGold pGold);
	}
}