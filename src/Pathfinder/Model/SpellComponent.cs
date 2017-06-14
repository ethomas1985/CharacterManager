using System;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class SpellComponent : ISpellComponent, IEquatable<ISpellComponent>
	{
		public SpellComponent(
			ComponentType pComponentType,
			string pDescription)
		{
			ComponentType = pComponentType;
			Description = pDescription;
		}

		public ComponentType ComponentType { get; }
		public string Description { get; }

		public override string ToString()
		{
			return $"{nameof(SpellComponent)}: [{ComponentType}] {Description}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ISpellComponent);
		}

		public bool Equals(ISpellComponent pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return ComparisonUtilities.Compare(GetType().Name, ComponentType, pOther.ComponentType, nameof(ComponentType))
				&& ComparisonUtilities.Compare(GetType().Name, Description, pOther.Description, nameof(Description));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((int)ComponentType * 397) ^ (Description != null ? Description.GetHashCode() : 0);
			}
		}
	}
}
