using Pathfinder.Interface.Currency;

namespace Pathfinder.Model.Currency
{
	internal class Platinum : AbstractCurrency, IPlatinum
	{
		internal const string DENOMINATION = "pp";

		public Platinum(int pValue) : base(pValue)
		{
		}

		public override string Denomination => DENOMINATION;

		public ICopper ToCopper()
		{
			return new Copper(Value * 1000);
		}

		public ISilver ToSilver()
		{
			return new Silver(Value * 100);
		}

		public IGold ToGold()
		{
			return new Gold(Value * 10);
		}

		public IPlatinum Add(IPlatinum pPlatinum)
		{
			return new Platinum(Value + pPlatinum.Value);
		}

		public IPlatinum Subtract(IPlatinum pPlatinum)
		{
			return new Platinum(Value - pPlatinum.Value);
		}
	}
}