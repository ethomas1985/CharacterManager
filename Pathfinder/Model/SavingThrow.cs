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
			SavingThrowType fortitude,
			IAbilityScore pAbilityScore,
			Func<int> pGetBase)
		{
			Type = fortitude;
			AbilityScore = pAbilityScore;

			GetBase = pGetBase;
		}

		private Func<int> GetBase { get; }
		private IAbilityScore AbilityScore { get; }

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

		public int Base { get { return GetBase(); } }
		public AbilityType Ability { get { return AbilityScore.Ability; } }
		public int AbilityModifier { get { return AbilityScore.Modifier; } }
		public int Resist { get; internal set; }
		public int Misc { get; internal set; }
		public int Temporary { get; internal set; }

		internal int this[string pPropertyName]
		{
			get
			{
				switch (pPropertyName)
				{
					case nameof(Base):
						return Base;
					case nameof(Score):
						return Score;
					case nameof(AbilityModifier):
						return AbilityModifier;
					case nameof(Resist):
						return Resist;
					case nameof(Misc):
						return Misc;
					case nameof(Temporary):
						return Temporary;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
			set
			{
				switch (pPropertyName)
				{
					case nameof(Resist):
						Resist = value;
						break;
					case nameof(Misc):
						Misc = value;
						break;
					case nameof(Temporary):
						Temporary = value;
						break;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
		}
	}
}