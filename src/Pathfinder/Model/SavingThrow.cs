using Pathfinder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class SavingThrow : ISavingThrow
	{
		public SavingThrow(
			SavingThrowType pSavingThrowType,
			IAbilityScore pAbilityScore,
			int pBase,
			int pResist,
			int pTemporary,
			int pMiscellanous)
		{
			Type = pSavingThrowType;
			AbilityScore = pAbilityScore;

			Base = pBase;
			Resist = pResist;
			Temporary = pTemporary;
			Misc = pMiscellanous;
		}

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

		public SavingThrowType Type { get; }
		private IAbilityScore AbilityScore { get; }
		public int Base { get; }
		public int AbilityModifier
		{
			get
			{
				if (AbilityScore == null)
				{
					return 0;
				}

				if (AbilityScore.Type != Ability)
				{
					throw new InvalidOperationException(
						$"{Type} Saving Throws require the {Ability} Score; not {AbilityScore.Type}.");
				}

				return AbilityScore.Modifier;
			}
		}

		public int Resist { get; }
		public int Misc { get; }
		public int Temporary { get; }
	}
}