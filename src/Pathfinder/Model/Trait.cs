using System;
using System.Collections.Generic;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Trait : ITrait, IEquatable<ITrait>
	{
		public Trait(string pName, string pText, bool pConditional, IDictionary<string, int> pPropertyModifiers)
		{
			Name = pName;
			Text = pText;
			PropertyModifiers = pPropertyModifiers;
			Conditional = pConditional;
		}

		public string Name { get; }
		public string Text { get; }
		public bool Conditional { get; }
		public IDictionary<string, int> PropertyModifiers { get; }

		public override string ToString()
		{
			return $"{nameof(Trait)}: {Name}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ITrait);
		}

		public bool Equals(ITrait pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var result = ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name));
			result &= ComparisonUtilities.Compare(GetType().Name, Text, pOther.Text, nameof(Text));
			result &= ComparisonUtilities.Compare(GetType().Name, Conditional, pOther.Conditional, nameof(Conditional));
			result &= ComparisonUtilities.CompareDictionaries(GetType().Name, PropertyModifiers, pOther.PropertyModifiers, nameof(PropertyModifiers));

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Conditional.GetHashCode();
				hashCode = (hashCode * 397) ^ (PropertyModifiers != null ? PropertyModifiers.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
