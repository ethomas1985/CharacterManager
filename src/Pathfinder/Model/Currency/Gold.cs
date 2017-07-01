using Pathfinder.Interface.Model.Currency;

namespace Pathfinder.Model.Currency
{
	internal class Gold : AbstractCurrency, IGold
	{
		internal const string DENOMINATION = "gp";

		public Gold(int pValue) : base(pValue)
		{
		}

		public override string Denomination => DENOMINATION;

		public ICopper ToCopper()
		{
			return new Copper(Value * 100);
		}

		public ISilver ToSilver()
		{
			return new Silver(Value * 10);
		}

		public IPlatinum ToPlatinum()
		{
			return new Platinum(Value / 10);
		}

		public IGold Add(IGold pGold)
		{
			return new Gold(Value + pGold.Value);
		}

		public IGold Subtract(IGold pGold)
		{
			return new Gold(Value - pGold.Value);
		}
	}
}