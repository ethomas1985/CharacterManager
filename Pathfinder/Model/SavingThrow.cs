using Pathfinder.Enum;
using Pathfinder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class SavingThrow : ISavingThrow
	{
		public SavingThrow(
			SavingThrowType pSavingThrowType,
			Func<IAbilityScore> pGetAbilityScore,
			Func<int> pGetBase,
			Func<int> pGetResist,
			Func<int> pGetTemporary)
		{
			Type = pSavingThrowType;
			GetAbilityScore = pGetAbilityScore;

			GetBase = pGetBase;
			GetResist = pGetResist;
			GetTemporary = pGetTemporary;
		}

		private Func<int> GetBase { get; }
		private Func<int> GetResist { get; }
		private Func<int> GetTemporary { get; }
		private Func<IAbilityScore> GetAbilityScore { get; }

		public SavingThrowType Type { get; }

		public int Score
		{
			get
			{
				var values = new List<int>
				{
					Base,
					AbilityModifier,
					Resist,
					Misc,
					Temporary
				};

				var score = values.Sum();

				Tracer.Message(pMessage: $"{Type} = {string.Join(" + ", values)} = {score}");

				return score;
			}
		}

		public int Base => GetBase();
		public AbilityType Ability
		{
			get
			{
				switch (Type)
				{
					case SavingThrowType.Fortitude:
						return AbilityType.Constitution;
					case SavingThrowType.Reflex:
						return AbilityType.Dexterity;
					case SavingThrowType.Will:
						return AbilityType.Wisdom;
					default:
						// Type is not nullable, and there are only the three options. Got to throw to shut the compiler up.
						throw new Exception("Invalid SavingThrowType.");
				}
			}
		}
		public int AbilityModifier
		{
			get
			{
				var abilityScore = GetAbilityScore?.Invoke();
				if (abilityScore == null)
				{
					return 0;
				}

				if (abilityScore.Type != Ability)
				{
					throw new InvalidOperationException(
						$"{Type} Saving Throws require the {Ability} Score; not {abilityScore.Type}.");
                }

				return abilityScore.Modifier;
			}
		}

		public int Resist => GetResist();
		public int Misc { get; internal set; }
		public int Temporary => GetTemporary();
	}
}