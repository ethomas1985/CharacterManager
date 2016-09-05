namespace Pathfinder.Interface.Currency
{
	public interface ICurrency
	{
		string Denomination { get; }
		int Value { get; }
	}
}