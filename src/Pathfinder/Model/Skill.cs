using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Skill : ISkill
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
	}
}
