using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface ISubFeature : INamed
	{
		string Body { get; }

		FeatureAbilityType AbilityType { get; }
	}
}