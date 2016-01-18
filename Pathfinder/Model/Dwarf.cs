using Pathfinder.Enum;

namespace Pathfinder.Model
{
	internal class Dwarf : AbstractCharacter
	{
		public override Race Race { get { return Race.Dwarf; } }
		public override Size BaseSize { get { return Size.Medium; } }

		public override decimal BaseSpeed { get { return 20; } }
		public override decimal ArmoredSpeed { get { return BaseSpeed; } }
	}
}
