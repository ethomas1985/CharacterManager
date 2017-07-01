using System;
using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Feature : IFeature, IEquatable<IFeature>
	{
		public Feature(
			string pName,
			string pBody,
			FeatureAbilityType pAbilityType,
			IEnumerable<ISubFeature> pSubFeatures)
		{
			Name = pName;
			Body = pBody;
			AbilityType = pAbilityType;
			SubFeatures = pSubFeatures;
		}

		public string Name { get; }
		public string Body { get; }
		public FeatureAbilityType AbilityType { get; }
		public IEnumerable<ISubFeature> SubFeatures { get; }

		public override string ToString()
		{
			return $"{Name} : {AbilityType}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IFeature);
		}

		public bool Equals(IFeature pOther)
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
				&& ComparisonUtilities.Compare(GetType().Name, AbilityType, pOther.AbilityType, nameof(AbilityType))
				&& ComparisonUtilities.CompareEnumerables(GetType().Name, SubFeatures, pOther.SubFeatures, nameof(SubFeatures));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Body != null ? Body.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (int)AbilityType;
				hashCode = (hashCode * 397) ^ (SubFeatures != null ? SubFeatures.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
