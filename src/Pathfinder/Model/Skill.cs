using Pathfinder.Enum;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Skill : ISkill
	{
		public Skill(
			string pName, 
			AbilityType pAbilityType, 
			string pKeyAbility, 
			string pTrainedOnly, 
			string pArmorCheckPenalty, 
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
			KeyAbility = pKeyAbility;
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
		public string KeyAbility { get; }
		public string TrainedOnly { get; }
		public string ArmorCheckPenalty { get; }
		public string Description { get; }
		public string Check { get; }
		public string Action { get; }
		public string TryAgain { get; }
		public string Special { get; }
		public string Restriction { get; }
		public string Untrained { get; }
	}
}
