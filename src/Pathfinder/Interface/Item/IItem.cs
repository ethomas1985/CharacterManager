using Pathfinder.Enums;
using Pathfinder.Interface.Currency;

namespace Pathfinder.Interface.Item
{
	public interface IItem : INamed
	{
		string Category { get; }
		IPurse Cost { get; }
		string Weight { get; }
		string Description { get; }
		ItemType ItemType { get; set; }
	}
}