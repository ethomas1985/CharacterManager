using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character
{
	internal class ExperienceAdded : AbstractEvent, IEquatable<ExperienceAdded>
	{
		public ExperienceAdded(Guid pId, int pVersion, IExperienceEvent pExperienceEvent)
			: base(pId, pVersion)
		{
			ExperienceEvent = pExperienceEvent;
		}
		public IExperienceEvent ExperienceEvent { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(ExperienceEvent)} '{ExperienceEvent.Title}' Logged | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ExperienceAdded);
		}

		public bool Equals(ExperienceAdded pOther)
		{
			return base.Equals(pOther)
				   && ExperienceEvent.Equals(pOther.ExperienceEvent);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397) ^ (ExperienceEvent != null ? ExperienceEvent.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}