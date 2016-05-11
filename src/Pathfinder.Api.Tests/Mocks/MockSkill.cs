using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Api.Tests.Mocks
{
	internal class MockSkill : ISkill
	{
		public string Name { get; set; }
		public AbilityType AbilityType { get; }
		public bool TrainedOnly { get; }
		public bool ArmorCheckPenalty { get; }
		public string Description { get; }
		public string Check { get; }
		public string Action { get; }
		public string TryAgain { get; }
		public string Special { get; }
		public string Restriction { get; }
		public string Untrained { get; }
	}
}
