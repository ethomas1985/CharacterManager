using System;
using Pathfinder.Enums;

namespace Pathfinder.Events.Character
{
	internal class StrengthSet : AbstractAbilityScoreSet
	{
		public StrengthSet(Guid pId, int pVersion, int pBase, int pEnhanced, int pInherent)
			: base(pId, pVersion, pBase, pEnhanced, pInherent, AbilityType.Strength)
		{ }
	}
}