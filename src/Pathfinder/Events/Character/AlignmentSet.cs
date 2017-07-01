using System;
using Pathfinder.Enums;

namespace Pathfinder.Events.Character
{
	internal class AlignmentSet : AbstractEvent, IEquatable<AlignmentSet>
	{
		public AlignmentSet(Guid pId, int pVersion, Alignment pAlignment) : base(pId, pVersion)
		{
			Alignment = pAlignment;
		}

		public Alignment Alignment { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Alignment set to {Alignment} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as AlignmentSet);
		}

		public bool Equals(AlignmentSet pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			if (pOther.GetType() != GetType())
			{
				return false;
			}

			return base.Equals(pOther) && Alignment == pOther.Alignment;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (int)Alignment;
			}
		}
	}
}
