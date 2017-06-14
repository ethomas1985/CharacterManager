namespace Pathfinder.Interface.Currency
{
	public interface ICopper: ICurrency
	{
		ISilver ToSilver();
		IGold ToGold();
		IPlatinum ToPlatinum();

		ICopper Add(ICopper pCopper);
		ICopper Subtract(ICopper pCopper);
	}
}