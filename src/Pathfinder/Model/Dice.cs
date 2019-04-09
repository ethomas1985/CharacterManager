using System;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Dice: IDice, IEquatable<IDice>
	{
		public Dice(int pDieCount, IDie pDie)
		{
			DieCount = pDieCount;
			Die = pDie;
		}

		public int DieCount { get; }
		public IDie Die { get; }

		public override bool Equals(object pObject)
		{
			return Equals(pObject as IDice);
		}

		public bool Equals(IDice pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return ComparisonUtilities.Compare(GetType().Name, DieCount, pOther.DieCount, nameof(DieCount))
				&& ComparisonUtilities.Compare(GetType().Name, Die, pOther.Die, nameof(Die));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (DieCount * 397) ^ (Die != null ? Die.GetHashCode() : 0);
			}
		}

		public override string ToString()
		{
			// I am relying on my implementation of IDie's ToString() to return a string like "d{faces}".
			return $"{DieCount}{Die}";
		}

        public static Dice Copy(IDice pOther)
        {
            return new Dice(pOther.DieCount, pOther.Die);
        }
	}
}
