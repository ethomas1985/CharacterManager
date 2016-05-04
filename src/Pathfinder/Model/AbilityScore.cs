using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Enums;

namespace Pathfinder.Model
{
	internal class AbilityScore : IAbilityScore
	{
		public AbilityScore(
			AbilityType pAbilityType,
			int pBase,
			int pEnhanced = 0,
			int pInherent = 0,
			int pTemporaryModifier = 0,
			int pPenalty = 0)
		{
			Type = pAbilityType;
			Base = pBase;
			Enhanced = pEnhanced;
			Inherent = pInherent;
			Temporary = pTemporaryModifier;
			Penalty = pPenalty;
		}

		public AbilityType Type { get; }

		public int Score
		{
			get
			{
				var score = Values.Sum();

				Tracer.Message(
					pMessage: $"{Type} = {string.Join(" + ", Values)} = {score}");

				return score;
			}
		}

		public int Modifier
		{
			get
			{
				var scoreMinusTen = Score - 10;
				var half = scoreMinusTen / 2.0M;
				return Math.Max(-5, (int) Math.Floor(half));
			}
		}

		public int Base { get; }
		public int Enhanced { get; }
		public int Inherent { get; }
		public int Penalty { get; }
		public int Temporary { get; }

		private IEnumerable<int> Values
			=> new List<int>
			{
				Base,
				Enhanced,
				Inherent,
				Temporary,
				Penalty*-1
			};

		public override string ToString()
		{
			return $"{Type}[{Score}][{Modifier}] = {string.Join(" + ", Values)} = {Score}";
		}
	}
}