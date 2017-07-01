using System;
using Pathfinder.Interface.Model.Currency;

namespace Pathfinder.Model.Currency
{
	public abstract class AbstractCurrency : ICurrency, IEquatable<ICurrency>
	{
		protected AbstractCurrency(int pValue)
		{
			Value = pValue;
		}

		public int Value { get; }
		public abstract string Denomination { get; }

		public override string ToString()
		{
			return $"{Value} {Denomination}";
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as ICurrency);
		}

		public bool Equals(ICurrency pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return Denomination == pOther.Denomination
				&& Value == pOther.Value;
		}

		public override int GetHashCode()
		{
			var hashCode = Denomination.GetHashCode();
			hashCode = (hashCode*397) ^ Value.GetHashCode();

			return hashCode;
		}
	}
}