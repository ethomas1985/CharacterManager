using System;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class SubFeature : ISubFeature, IEquatable<ISubFeature>
	{
		public SubFeature(
			string pName,
			string pBody,
			FeatureAbilityType pAbilityType)
		{
			Name = pName;
			Body = pBody;
			AbilityType = pAbilityType;
		}

		public string Name { get; }
		public string Body { get; }
		public FeatureAbilityType AbilityType { get; }

		public override string ToString()
		{
			return $"{Name} : {AbilityType}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ISubFeature);
		}

		public bool Equals(ISubFeature pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return ComparisonUtilities.Compare<string>(GetType().Name, Name, pOther.Name, nameof(Name))
				&& ComparisonUtilities.Compare<string>(GetType().Name, Body, pOther.Body, nameof(Body))
				&& ComparisonUtilities.Compare(GetType().Name, AbilityType, pOther.AbilityType, nameof(AbilityType));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Body != null ? Body.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (int)AbilityType;
				return hashCode;
			}
		}
	}
}
