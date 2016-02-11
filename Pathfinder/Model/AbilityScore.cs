using System;
using Pathfinder.Enum;
using Pathfinder.Interface;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class AbilityScore : IAbilityScore
	{
		public AbilityScore(AbilityType pAbilityType, Func<int> pGetTemporaryModifier)
		{
			Type = pAbilityType;
			GetTemporaryModifier = pGetTemporaryModifier;
		}

		private Func<int> GetTemporaryModifier { get; }

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
				int scoreMinusTen = Score - 10;
				decimal half = scoreMinusTen / 2.0M;
				return Math.Max(-5, (int) Math.Floor(half));
			}
		}

		public int Base { get; internal set; }
		public int Enhanced { get; internal set; }
		public int Inherent { get; internal set; }
		public int Penalty { get; internal set; }
		public int Temporary
		{
			get
			{
				return GetTemporaryModifier();
			}
		}

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