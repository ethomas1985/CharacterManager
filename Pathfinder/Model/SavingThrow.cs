using Pathfinder.Enum;
using Pathfinder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

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
			Ability = pAbilityScore;

			GetBase = pGetBase;
		}

		private Func<int> GetBase { get; }

		public SavingThrowType Type { get; }
		public IAbilityScore Ability { get; }

		public int Score
		{
			get
			{
				return new List<int>
				{

				}.Sum();
			}
		}

		public int Base { get { return GetBase(); } }
		public int AbilityModifier { get { return Ability.Modifier; } }

		public int Resist { get; private set; }
		public int Misc { get; private set; }
		public int Temporary { get; private set; }

		internal int this[string pPropertyName]
		{
			get
			{
				switch (pPropertyName)
				{
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