using Pathfinder.Enums;
using Pathfinder.Interface;
using System;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Skill : ISkill, IEquatable<ISkill>
	{
		public Skill(
			string pName,
			AbilityType pAbilityType,
			bool pTrainedOnly,
			bool pArmorCheckPenalty,
			string pDescription,
			string pCheck = null,
			string pAction = null,
			string pTryAgain = null,
			string pSpecial = null,
			string pRestriction = null,
			string pUntrained = null)
		{
			Name = pName;
			AbilityType = pAbilityType;
			TrainedOnly = pTrainedOnly;
			ArmorCheckPenalty = pArmorCheckPenalty;
			Description = pDescription;
			Check = pCheck;
			Action = pAction;
			TryAgain = pTryAgain;
			Special = pSpecial;
			Restriction = pRestriction;
			Untrained = pUntrained;
		}


		public string Name { get; }
		public AbilityType AbilityType { get; }
		public bool TrainedOnly { get; }
		public bool ArmorCheckPenalty { get; }
		public string Description { get; }
		public string Check { get; }
		public string Action { get; }
		public string TryAgain { get; }
		public string Special { get; }
		public string Restriction { get; }
		public string Untrained { get; }

		public override bool Equals(object pObject)
		{
			return Equals(pObject as ISkill);
		}

		public bool Equals(ISkill pOther)
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
			result &= ComparisonUtilities.Compare(GetType().Name, AbilityType, pOther.AbilityType, nameof(AbilityType));
			result &= ComparisonUtilities.Compare(GetType().Name, TrainedOnly, pOther.TrainedOnly, nameof(TrainedOnly));
			result &= ComparisonUtilities.Compare(GetType().Name, ArmorCheckPenalty, pOther.ArmorCheckPenalty, nameof(ArmorCheckPenalty));
			result &= ComparisonUtilities.Compare(GetType().Name, Description, pOther.Description, nameof(Description));
			result &= ComparisonUtilities.Compare(GetType().Name, Check, pOther.Check, nameof(Check));
			result &= ComparisonUtilities.Compare(GetType().Name, Action, pOther.Action, nameof(Action));
			result &= ComparisonUtilities.Compare(GetType().Name, TryAgain, pOther.TryAgain, nameof(TryAgain));
			result &= ComparisonUtilities.Compare(GetType().Name, Special, pOther.Special, nameof(Special));
			result &= ComparisonUtilities.Compare(GetType().Name, Restriction, pOther.Restriction, nameof(Restriction));
			result &= ComparisonUtilities.Compare(GetType().Name, Untrained, pOther.Untrained, nameof(Untrained));

			return result;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (int)AbilityType;
				hashCode = (hashCode * 397) ^ TrainedOnly.GetHashCode();
				hashCode = (hashCode * 397) ^ ArmorCheckPenalty.GetHashCode();
				hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Check?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Action?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (TryAgain?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Special?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Restriction?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Untrained?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}
