using Pathfinder.Enums;

namespace Pathfinder.Interface.Model
{
	public interface ISubFeature : INamed
	{
		string Body { get; }

		FeatureAbilityType AbilityType { get; }
	}
}