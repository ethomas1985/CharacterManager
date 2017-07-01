using Pathfinder.Interface.Model.Currency;

namespace Pathfinder.Model.Currency
{
	internal class Copper : AbstractCurrency, ICopper
	{
		internal const string DENOMINATION = "cp";

		public Copper(int pValue) : base(pValue)
		{
		}

		public override string Denomination => DENOMINATION;

		public ISilver ToSilver()
		{
			return new Silver(Value / 10);
		}

		public IGold ToGold()
		{
			return new Gold(Value / 100);
		}

		public IPlatinum ToPlatinum()
		{
			return new Platinum(Value / 1000);
		}

		public ICopper Add(ICopper pCopper)
		{
			return new Copper(Value + pCopper.Value);
		}

		public ICopper Subtract(ICopper pCopper)
		{
			return new Copper(Value - pCopper.Value);
		}
	}
}