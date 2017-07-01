namespace Pathfinder.Interface.Model.Currency
{
	public interface ISilver : ICurrency
	{
		ICopper ToCopper();

		IGold ToGold();
		IPlatinum ToPlatinum();

		ISilver Add(ISilver pSilver);
		ISilver Subtract(ISilver pSilver);
	}
}