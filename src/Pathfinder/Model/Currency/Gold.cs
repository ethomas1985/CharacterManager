using Pathfinder.Interface.Currency;

namespace Pathfinder.Model.Currency
{
	internal class Gold : BaseCurrency, IGold
	{
		public Gold(int value) : base(value)
		{
		}

		public override string Denomination => "gp";

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