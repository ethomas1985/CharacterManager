using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character
{
	internal class ClassAdded : AbstractEvent, IEquatable<ClassAdded>
	{
		public ClassAdded(Guid pId, int pVersion, ICharacterClass pClass)
			: base(pId, pVersion)
		{
			Class = pClass;
		}

		public ICharacterClass Class { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | {nameof(Class)} '{Class}' Added | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ClassAdded);
		}

		public bool Equals(ClassAdded pOther)
		{
			return base.Equals(pOther)
				   && Equals(Class, pOther.Class);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397) ^ (Class != null ? Class.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}