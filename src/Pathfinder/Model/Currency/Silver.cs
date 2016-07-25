using Pathfinder.Interface.Currency;

namespace Pathfinder.Model.Currency
{
	internal class Silver : BaseCurrency, ISilver
	{
		public Silver(int value) : base(value)
		{
		}

		public override string Denomination => "sp";

		public ICopper ToCopper()
		{
			return new Copper(Value * 10);
		}

		public IGold ToGold()
		{
			return new Gold(Value / 10);
		}

		public IPlatinum ToPlatinum()
		{
			return new Platinum(Value * 100);
		}

		public ISilver Add(ISilver pSilver)
		{
			return new Silver(Value + pSilver.Value);
		}

		public ISilver Subtract(ISilver pSilver)
		{
			return new Silver(Value - pSilver.Value);
		}
	}
}