using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character
{
	internal class ClassLevelRaised : AbstractEvent, IEquatable<ClassLevelRaised>
	{
		public ClassLevelRaised(Guid pId, int pVersion, IClass pClass, int pHitPoints)
			: base(pId, pVersion)
		{
			Class = pClass;
			HitPoints = pHitPoints;
		}
		public IClass Class { get; }
		public int HitPoints { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Class)} '{Class}' Level Raised with {HitPoints} {nameof(HitPoints)} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ClassLevelRaised);
		}

		public bool Equals(ClassLevelRaised pOther)
		{
			return base.Equals(pOther)
				&& Class.Equals(pOther.Class)
				&& HitPoints == pOther.HitPoints;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397)
					^ (Class != null ? Class.GetHashCode() : 0)
					^ HitPoints.GetHashCode();
				return hashCode;
			}
		}
	}
}