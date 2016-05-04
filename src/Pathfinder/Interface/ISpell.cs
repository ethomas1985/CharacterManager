﻿using System;
using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface
{
	public interface ISpell : INamed
	{
		MagicSchool School { get; }
		MagicSubSchool SubSchool { get; }
		ISet<MagicDescriptor> MagicDescriptors { get; }
		string SavingThrow { get; }
		string Description { get; }
		bool HasSpellResistance { get; }
		string SpellResistance { get; }
		string CastingTime { get; }
		string Range { get; }

		IDictionary<string, int> LevelRequirements { get; }

		string Duration { get; }
		ISet<Tuple<ComponentType, string>> Components { get; }
	}
}