using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Feat : IFeat
	{
		public Feat(
			string name,
			FeatType featType,
			IEnumerable<string> prerequisites,
			string description,
			string benefit,
			string special)
		{
			Name = name;
			FeatType = featType;
			Prerequisites = prerequisites;
			Description = description;
			Benefit = benefit;
			Special = special;
		}

		public string Name { get; }
		public FeatType FeatType { get; }
		public IEnumerable<string> Prerequisites { get; }
		public string Description { get; }
		public string Benefit { get; }
		public string Special { get; }
	}
}
