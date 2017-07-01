using System;

namespace Pathfinder.Events.Character
{
	internal class EyesSet : AbstractEvent, IEquatable<EyesSet>
	{
		public EyesSet(Guid pId, int pVersion, string pEyes)
			: base(pId, pVersion)
		{
			Eyes = pEyes;
		}

		public string Eyes { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Eyes)} set to {Eyes} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as EyesSet);
		}

		public bool Equals(EyesSet pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return base.Equals(pOther)
				   && string.Equals(Eyes, pOther.Eyes);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Eyes != null ? Eyes.GetHashCode() : 0);
			}
		}
	}
}