using System;

namespace Pathfinder.Enums
{
	public enum DamageType
	{
		Bludgeoning,
		Piercing,
		Slashing,

		BludgeoningAndPiercing,
		BludgeoningAndSlashing,

		PiercingAndSlashing,

		BludgeoningOrPiercing,
		BludgeoningOrSlashing,

		PiercingOrSlashing,
	}
}