using System;

namespace Pathfinder.Events.Character
{
	internal class AgeSet : AbstractEvent, IEquatable<AgeSet>
	{
		public AgeSet(Guid pId, int pVersion, int pAge)
			: base(pId, pVersion)
		{
			Age = pAge;
		}
		public int Age { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Age Set at {Age} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as AgeSet);
		}

		public bool Equals(AgeSet pOther)
		{
			return base.Equals(pOther)
				&& Age == pOther.Age;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ Age;
			}
		}
	}
}