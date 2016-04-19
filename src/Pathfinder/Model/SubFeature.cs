using System.Collections.Generic;
using Pathfinder.Enum;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class SubFeature : Feature, ISubFeature
	{
		public SubFeature(
			string pName,
			string pBody,
			FeatureAbilityTypes pAbilityType) : base(pName, pBody, pAbilityType, null)
		{
		}
	}
}
