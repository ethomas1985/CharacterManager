using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Api.Tests.Mocks
{
	internal class MockRace : IRace
	{
		public string Name { get; set; }
		public string Adjective { get; }
		public Size Size { get; }
		public int BaseSpeed { get; }
		public IDictionary<AbilityType, int> AbilityScores { get; }
		public IEnumerable<ITrait> Traits { get; }
		public IEnumerable<ILanguage> Languages { get; }
		public string Description { get; }

		public bool TryGetAbilityScore(AbilityType pAbilityType, out int pValue)
		{
			throw new System.NotImplementedException();
		}

		public override string ToString()
		{
			return $"{nameof(MockRace)}: {Name}";
		}
	}
}
