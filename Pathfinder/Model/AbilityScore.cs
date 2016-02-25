using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Model
{
	internal class AbilityScore : IAbilityScore
	{
		public AbilityScore(
			AbilityType pAbilityType,
			Func<int> pGetTemporaryModifier,
			int pBase,
			int pEnhanced = 0,
			int pInherent = 0)
		{
			Type = pAbilityType;
			GetTemporaryModifier = pGetTemporaryModifier;
			Base = pBase;
			Enhanced = pEnhanced;
			Inherent = pInherent;
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

		public int Base { get; }
		public int Enhanced { get; }
		public int Inherent { get; }
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