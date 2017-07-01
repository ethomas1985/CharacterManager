using System;

namespace Pathfinder.Events.Character
{
	internal class CharacterCreated : AbstractEvent, IEquatable<CharacterCreated>
	{
		public CharacterCreated(Guid pId)
			: base(pId, 0)
		{ }

		public override string ToString()
		{
			return $"Character [{Id}] | Created";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as CharacterCreated);
		}

		public bool Equals(CharacterCreated pOther)
		{
			return base.Equals(pOther);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
