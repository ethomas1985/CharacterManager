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
			Func<int> pGetBase,
			Func<int> pGetResist,
			Func<int> pGetTemporary)
		{
			Type = fortitude;
			AbilityScore = pAbilityScore;

			GetBase = pGetBase;
			GetResist = pGetResist;
			GetTemporary = pGetTemporary;
		}

		private Func<int> GetBase { get; }
		private Func<int> GetResist { get; }
		private Func<int> GetTemporary { get; }
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

		public int Base => GetBase();
		public AbilityType Ability => AbilityScore.Type;
		public int AbilityModifier => AbilityScore.Modifier;

		public int Resist => GetResist();
		public int Misc { get; internal set; }
		public int Temporary => GetTemporary();
	}
}