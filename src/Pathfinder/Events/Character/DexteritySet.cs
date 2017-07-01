using System;
using Pathfinder.Enums;

namespace Pathfinder.Events.Character
{
	internal class DexteritySet : AbstractAbilityScoreSet
	{
		public DexteritySet(Guid pId, int pVersion, int pBase, int pEnhanced, int pInherent)
			: base(pId, pVersion, pBase, pEnhanced, pInherent, AbilityType.Dexterity)
		{ }
	}
}