using System;
using Pathfinder.Enums;
using Pathfinder.Interface;

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
			string pCheck, 
			string pAction, 
			string pTryAgain, 
			string pSpecial, 
			string pRestriction, 
			string pUntrained)
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
			return 
				string.Equals(Name, pOther.Name)
				&& AbilityType == pOther.AbilityType
				&& TrainedOnly == pOther.TrainedOnly
				&& ArmorCheckPenalty == pOther.ArmorCheckPenalty
				&& string.Equals(Description, pOther.Description)
				&& string.Equals(Check, pOther.Check)
				&& string.Equals(Action, pOther.Action)
				&& string.Equals(TryAgain, pOther.TryAgain)
				&& string.Equals(Special, pOther.Special)
				&& string.Equals(Restriction, pOther.Restriction)
				&& string.Equals(Untrained, pOther.Untrained);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (int) AbilityType;
				hashCode = (hashCode*397) ^ TrainedOnly.GetHashCode();
				hashCode = (hashCode*397) ^ ArmorCheckPenalty.GetHashCode();
				hashCode = (hashCode*397) ^ (Description?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (Check?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (Action?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (TryAgain?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (Special?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (Restriction?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ (Untrained?.GetHashCode() ?? 0);
				return hashCode;
			}
		}
	}
}
