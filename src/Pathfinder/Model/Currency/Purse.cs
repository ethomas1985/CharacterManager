using Pathfinder.Interface.Currency;

namespace Pathfinder.Model.Currency
{
	internal class Purse : IPurse
	{
		public Purse(
			ICopper pCopper = null,
			ISilver pSilver = null,
			IGold pGold = null,
			IPlatinum pPlatinum = null)
		{
#if ALLOW_PURSE_SIMPLIFICATION
			int pCopperValue = pCopper?.Value ?? 0;
			int pSilverValue = pSilver?.ToCopper().Value ?? 0;
			int pGoldValue = pGold?.ToCopper().Value ?? 0;
			int pPlatinumValue = pPlatinum?.ToCopper().Value ?? 0;

			int absoluteCopper = pCopperValue + pSilverValue + pGoldValue + pPlatinumValue;

			Platinum = new Copper(absoluteCopper).ToPlatinum();
			absoluteCopper -= new Copper(absoluteCopper).ToPlatinum().ToCopper().Value;

			Gold = new Copper(absoluteCopper).ToGold();
			absoluteCopper -= new Copper(absoluteCopper).ToGold().ToCopper().Value;

			Silver = new Copper(absoluteCopper).ToSilver();
			absoluteCopper -= new Copper(absoluteCopper).ToSilver().ToCopper().Value;

			Copper = new Copper(absoluteCopper);
#endif
			Copper = pCopper ?? new Copper(0);
			Silver = pSilver ?? new Silver(0);
			Gold = pGold ?? new Gold(0);
			Platinum = pPlatinum ?? new Platinum(0);

		}
		public Purse(int pCopper = 0, int pSilver = 0, int pGold = 0, int pPlatinum = 0) :
			this(new Copper(pCopper), new Silver(pSilver), new Gold(pGold), new Platinum(pPlatinum))
		{}

		public ICopper Copper { get; }

		public ISilver Silver { get; }

		public IGold Gold { get; }

		public IPlatinum Platinum { get; }

		public IPurse Add(ICopper pCopper, ISilver pSilver, IGold pGold, IPlatinum pPlatinum)
		{
			var copper = Copper.Add(pCopper);
			var silver = Silver.Add(pSilver);
			var gold = Gold.Add(pGold);
			var platinum = Platinum.Add(pPlatinum);

			return new Purse(copper, silver, gold, platinum);
		}

		public IPurse Subtract(ICopper pCopper, ISilver pSilver, IGold pGold, IPlatinum pPlatinum)
		{
			var copper = Copper.Subtract(pCopper);
			var silver = Silver.Subtract(pSilver);
			var gold = Gold.Subtract(pGold);
			var platinum = Platinum.Subtract(pPlatinum);

			return new Purse(copper, silver, gold, platinum);
		}
	}
}
