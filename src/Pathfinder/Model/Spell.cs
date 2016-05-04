using System;
using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Spell : ISpell
	{
		public Spell(
			string pName,
			MagicSchool pSchool,
			MagicSubSchool pSubSchool,
			ISet<MagicDescriptor> pMagicDescriptors,
			string pSavingThrow,
			string pDescription,
			bool pHasSpellResistance,
			string pSpellResistance,
			string pCastingTime,
			string pRange,
			IDictionary<string, int> pLevelRequirements,
			string pDuration,
			ISet<Tuple<ComponentType, string>> pComponents)
		{
			Name = pName;
			School = pSchool;
			SubSchool = pSubSchool;
			MagicDescriptors = pMagicDescriptors;
			SavingThrow = pSavingThrow;
			Description = pDescription;
			HasSpellResistance = pHasSpellResistance;
			SpellResistance = pSpellResistance;
			CastingTime = pCastingTime;
			Range = pRange;
			LevelRequirements = pLevelRequirements;
			Duration = pDuration;
			Components = pComponents;
		}

		public string Name { get; }
		public MagicSchool School { get; }
		public MagicSubSchool SubSchool { get; }
		public ISet<MagicDescriptor> MagicDescriptors { get; }
		public string SavingThrow { get; }
		public string Description { get; }
		public bool HasSpellResistance { get; }
		public string SpellResistance { get; }
		public string CastingTime { get; }
		public string Range { get; }
		public IDictionary<string, int> LevelRequirements { get; }
		public string Duration { get; }
		public ISet<Tuple<ComponentType, string>> Components { get; }
	}
}
