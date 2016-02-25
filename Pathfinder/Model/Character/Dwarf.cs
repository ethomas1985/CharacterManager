using Pathfinder.Enum;

namespace Pathfinder.Model.Character
{
	/// <summary>
	/// +2 Constitution
	/// +2 Wisdom
	/// -2 Charisma
	/// 
	/// Size: Medium
	/// 
	/// Base speed: 20 ft.
	/// ArmoredSpeed: BaseSpeed -- ignores armed penalties to speed
	/// 
	/// </summary>
	internal class Dwarf : AbstractCharacter
	{
		public override Race Race { get { return Race.Dwarf; } }
		public override Size BaseSize { get { return Size.Medium; } }

		public override decimal BaseSpeed { get { return 20; } }
		public override decimal ArmoredSpeed { get { return BaseSpeed; } }

		protected override int GetRaceConstitutionModifier()
		{
			return 2;
		}

		protected override int GetRaceWisdomModifier()
		{
			return 2;
		}

		protected override int GetRaceCharismaModifier()
		{
			return -2;
		}

		protected override AbstractCharacter CopyCore()
		{
			return new Dwarf();
		}
	}
}
