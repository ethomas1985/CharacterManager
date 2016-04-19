using System.Collections.Generic;
using Pathfinder.Enum;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Feature : IFeature
	{
		public Feature(
			string pName,
			string pBody,
			FeatureAbilityTypes pAbilityType,
			IEnumerable<ISubFeature> subFeatures)
		{
			Name = pName;
			Body = pBody;
			AbilityType = pAbilityType;

			SubFeatures = subFeatures;
		}

		public string Name { get; }
		public string Body { get; }
		public FeatureAbilityTypes AbilityType { get; }
		public IEnumerable<ISubFeature> SubFeatures { get; }
	}
}
