using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Enums;

namespace Pathfinder.Model
{
	internal class AbilityScore : IAbilityScore, IEquatable<IAbilityScore>
	{
		public AbilityScore(
			AbilityType pAbilityType,
			int pBase,
			int pEnhanced = 0,
			int pInherent = 0,
			int pTemporaryModifier = 0,
			int pPenalty = 0,
			int pMaximumBound = -1)
		{
			Type = pAbilityType;
			Base = pBase;
			Enhanced = pEnhanced;
			Inherent = pInherent;
			Temporary = pTemporaryModifier;
			Penalty = pPenalty;
			MaximumBound = pMaximumBound;
		}

		public AbilityType Type { get; }

		public int Score
		{
			get
			{
				var score = Values.Sum();

				//Tracer.Message(
				//	pMessage: $"{Type} = {string.Join(" + ", Values)} = {score}");

				return score;
			}
		}

		public int Modifier
		{
			get
			{
				var scoreMinusTen = Score - 10;
				var half = scoreMinusTen / 2.0M;
				var modifier = Math.Max(-5, (int)Math.Floor(half));

				return MaximumBound == -1
					? modifier
					: Math.Min(modifier, MaximumBound);
			}
		}

		public int Base { get; }
		public int Enhanced { get; }
		public int Inherent { get; }
		public int Penalty { get; }
		public int Temporary { get; }

		public int MaximumBound { get; }

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

		public override bool Equals(object pObj)
		{
			return Equals(pObj as IAbilityScore);
		}

		public bool Equals(IAbilityScore pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var equal =
				Type == pOther.Type
				&& Base == pOther.Base
				&& Enhanced == pOther.Enhanced
				&& Inherent == pOther.Inherent
				&& Penalty == pOther.Penalty
				&& Temporary == pOther.Temporary;

			if (!equal)
			{
				Tracer.Message(pMessage: $"{GetType().Name}.{Type} this :: {ToString()}");
				Tracer.Message(pMessage: $"{GetType().Name}.{Type} that :: {pOther.ToString()}");
			}

			return equal;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (int)Type;
				hashCode = (hashCode * 397) ^ Base;
				hashCode = (hashCode * 397) ^ Enhanced;
				hashCode = (hashCode * 397) ^ Inherent;
				hashCode = (hashCode * 397) ^ Penalty;
				hashCode = (hashCode * 397) ^ Temporary;
				return hashCode;
			}
		}
	}
}