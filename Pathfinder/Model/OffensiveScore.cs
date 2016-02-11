using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Model
{
	internal class OffensiveScore : IOffensiveScore
	{
		public OffensiveScore(
			OffensiveType pOffensiveType,
			IAbilityScore pAbilityScore,
			Func<int> pGetBaseAttackBonus,
			Func<int> pGetSizeModifier,
			Func<int> pGetTemporaryModifier)
		{
			Type = pOffensiveType;
			Ability = pAbilityScore;
			GetBaseAttackBonus = pGetBaseAttackBonus;
			GetSizeModifier = pGetSizeModifier;
			GetTemporaryModifier = pGetTemporaryModifier;
		}

		private Func<int> GetBaseAttackBonus { get; }
		private Func<int> GetSizeModifier { get; }
		private Func<int> GetTemporaryModifier { get; }

		public OffensiveType Type { get; }
		public IAbilityScore Ability { get; }

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

		public int BaseAttackBonus { get { return GetBaseAttackBonus(); } }
		public int AbilityModifier { get { return Ability.Modifier; } }
		public int SizeModifier { get { return GetSizeModifier(); } }
		public int MiscModifier { get; internal set; }
		public int TemporaryModifier { get { return GetTemporaryModifier(); } }

		private IEnumerable<int> Values
			=> new List<int>
			{
				BaseAttackBonus,
				AbilityModifier,
				SizeModifier,
				MiscModifier,
				TemporaryModifier
			};

		public override string ToString()
		{
			return $"{Type} = {string.Join(" + ", Values)} = {Score}";
		}
	}
}