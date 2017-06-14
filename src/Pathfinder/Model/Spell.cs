using System;
using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Spell : ISpell, IEquatable<ISpell>
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
			ISet<ISpellComponent> pComponents)
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
		public ISet<ISpellComponent> Components { get; }

		public override string ToString()
		{
			return $"{nameof(Spell)}: {Name} [{School}|{SubSchool}]";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as ISpell);
		}

		public bool Equals(ISpell pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return ComparisonUtilities.Compare(GetType().Name, Name, pOther.Name, nameof(Name))
				&& ComparisonUtilities.Compare(GetType().Name, School, pOther.School, nameof(School))
				&& ComparisonUtilities.Compare(GetType().Name, SubSchool, pOther.SubSchool, nameof(SubSchool))
				&& ComparisonUtilities.CompareEnumerables(GetType().Name, MagicDescriptors, pOther.MagicDescriptors, nameof(MagicDescriptors))
				&& ComparisonUtilities.Compare(GetType().Name, SavingThrow, pOther.SavingThrow, nameof(SavingThrow))
				&& ComparisonUtilities.Compare(GetType().Name, Description, pOther.Description, nameof(Description))
				&& ComparisonUtilities.Compare(GetType().Name, HasSpellResistance, pOther.HasSpellResistance, nameof(HasSpellResistance))
				&& ComparisonUtilities.Compare(GetType().Name, SpellResistance, pOther.SpellResistance, nameof(SpellResistance))
				&& ComparisonUtilities.Compare(GetType().Name, CastingTime, pOther.CastingTime, nameof(CastingTime))
				&& ComparisonUtilities.Compare(GetType().Name, Range, pOther.Range, nameof(Range))
				&& ComparisonUtilities.CompareDictionaries(GetType().Name, LevelRequirements, pOther.LevelRequirements, nameof(LevelRequirements))
				&& ComparisonUtilities.Compare(GetType().Name, Duration, pOther.Duration, nameof(Duration))
				&& ComparisonUtilities.CompareEnumerables(GetType().Name, Components, pOther.Components, nameof(Components));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (int)School;
				hashCode = (hashCode * 397) ^ (int)SubSchool;
				hashCode = (hashCode * 397) ^ (MagicDescriptors != null ? MagicDescriptors.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (SavingThrow != null ? SavingThrow.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ HasSpellResistance.GetHashCode();
				hashCode = (hashCode * 397) ^ (SpellResistance != null ? SpellResistance.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (CastingTime != null ? CastingTime.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Range != null ? Range.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (LevelRequirements != null ? LevelRequirements.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Duration != null ? Duration.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Components != null ? Components.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}
