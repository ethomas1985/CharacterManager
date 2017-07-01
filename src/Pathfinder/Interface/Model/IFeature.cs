using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface.Model
{
	public interface IFeature : INamed
	{
		string Body { get; }
		FeatureAbilityType AbilityType { get; }
		IEnumerable<ISubFeature> SubFeatures { get; }
	}
}