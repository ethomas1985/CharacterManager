using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character
{
	internal class RaceSet : AbstractEvent, IEquatable<RaceSet>
	{
		public RaceSet(Guid pId, int pVersion, IRace pRace)
			: base(pId, pVersion)
		{
			Race = pRace;
		}
		public IRace Race { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Race Set at {Race} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as RaceSet);
		}

		public bool Equals(RaceSet pOther)
		{
			return base.Equals(pOther)
				&& Equals(Race, pOther.Race);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397) ^ (Race != null ? Race.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
