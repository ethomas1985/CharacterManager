using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class SubFeature : Feature, ISubFeature
	{
		public SubFeature(
			string pName,
			string pBody,
			FeatureAbilityType pAbilityType) : base(pName, pBody, pAbilityType, null)
		{
		}
	}
}
