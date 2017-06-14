using Pathfinder.Enums;

namespace Pathfinder.Interface {
	public interface ISpellComponent {
		ComponentType ComponentType { get; }
		string Description { get; }
	}
}