using System;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Events.Character
{
	internal class ArmorEquiped : AbstractEvent, IEquatable<ArmorEquiped>
	{
		public ArmorEquiped(Guid pId, int pVersion, IItem pArmor)
			: base(pId, pVersion)
		{
			Armor = pArmor;
		}
		public IItem Armor { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | '{Armor}' Equiped | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ArmorEquiped);
		}

		public bool Equals(ArmorEquiped pOther)
		{
			return base.Equals(pOther)
				   && Equals(Armor, pOther.Armor);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397) ^ (Armor != null ? Armor.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}