using System;
using JetBrains.Annotations;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Events.Character {
	internal abstract class AbstractEvent : IEvent
	{
		protected AbstractEvent(Guid pId, int pVersion)
		{
			Id = pId;
			Version = pVersion;
		}

		public Guid Id { get; }
		public int Version { get; }

		[ContractAnnotation("pOther:null => false")]
		public override bool Equals(object pOther)
		{
			return Equals(pOther as AbstractEvent);
		}

		[ContractAnnotation("pOther:null => false")]
		protected bool Equals(AbstractEvent pOther)
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

			return ComparisonUtilities.Compare(GetType().Name, Id, pOther.Id, nameof(Id)) 
				&& ComparisonUtilities.Compare(GetType().Name, Version, pOther.Version, nameof(Version));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Id.GetHashCode() * 397) ^ Version;
			}
		}
	}
}