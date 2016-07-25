using Pathfinder.Interface.Currency;

namespace Pathfinder.Model
{
	public abstract class BaseCurrency : ICurrency
	{
		protected BaseCurrency(int value)
		{
			Value = value;
		}

		public int Value { get; }
		public abstract string Denomination { get; }

		public override string ToString()
		{
			return Value > 0 ? $"{Value} {Denomination}" : string.Empty;
		}
	}
}