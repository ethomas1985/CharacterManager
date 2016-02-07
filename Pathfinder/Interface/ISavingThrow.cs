﻿using Pathfinder.Enum;

namespace Pathfinder.Interface
{
	public interface ISavingThrow
	{
		SavingThrowType Type { get; }

		int Score { get; }

		int Base { get; }
		AbilityType Ability { get; }
		int AbilityModifier { get; }
		int Resist { get; }
		int Misc { get; }
		int Temporary { get; }
	}
}
