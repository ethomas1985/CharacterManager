using System.Collections.Generic;
using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface IFeature
	{
		string Name { get; }
		string Body { get; }

		FeatureAbilityTypes AbilityType { get; } 

		IEnumerable<ISubFeature> SubFeatures { get; } 
	}
}