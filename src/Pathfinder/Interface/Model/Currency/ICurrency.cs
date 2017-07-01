namespace Pathfinder.Interface.Model.Currency
{
	public interface ICurrency
	{
		string Denomination { get; }
		int Value { get; }
	}
}