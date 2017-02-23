using System;

namespace Pathfinder.Enums
{

	[Flags]
	public enum DamageType
	{
		None,
		Bludgeoning,
		Piercing,
		Slashing,

		//BludgeoningAndPiercing, // Bludgeoning & Piercing
		//BludgeoningAndSlashing, // Bludgeoning & Slashing

		//PiercingAndSlashing, // Piercing & Slashing

		//BludgeoningOrPiercing, //Bludgeoning | Piercing
		//BludgeoningOrSlashing, //Bludgeoning |Slashing

		//PiercingOrSlashing, //Piercing | Slashing
	}
}