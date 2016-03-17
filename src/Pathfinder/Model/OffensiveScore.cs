using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Model
{
	internal class OffensiveScore : IOffensiveScore
	{
		public OffensiveScore(
			OffensiveType pOffensiveType,
			IAbilityScore pAbilityScore,
			int pBaseAttackBonus,
			int pSizeModifier,
			int pTemporaryModifier,
			int pMiscModifier)
		{
			Type = pOffensiveType;
			Ability = pAbilityScore;
			BaseAttackBonus = pBaseAttackBonus;
			SizeModifier = pSizeModifier;
			TemporaryModifier = pTemporaryModifier;
			MiscModifier = pMiscModifier;
		}

		public OffensiveType Type { get; }
		private IAbilityScore Ability { get; }

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

		public int BaseAttackBonus { get; }
		public int AbilityModifier => Ability?.Modifier ?? 0;
		public int SizeModifier { get; }
		public int MiscModifier { get;}
		public int TemporaryModifier { get; }

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