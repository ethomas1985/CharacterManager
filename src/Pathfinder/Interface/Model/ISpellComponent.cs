using Pathfinder.Enums;

namespace Pathfinder.Interface.Model {
	public interface ISpellComponent {
		ComponentType ComponentType { get; }
		string Description { get; }
	}
}