using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Feature : IFeature
	{
		public Feature(
			string pName,
			string pBody,
			FeatureAbilityType pAbilityType,
			IEnumerable<ISubFeature> subFeatures)
		{
			Name = pName;
			Body = pBody;
			AbilityType = pAbilityType;

			SubFeatures = subFeatures;
		}

		public string Name { get; }
		public string Body { get; }
		public FeatureAbilityType AbilityType { get; }
		public IEnumerable<ISubFeature> SubFeatures { get; }
	}
}
