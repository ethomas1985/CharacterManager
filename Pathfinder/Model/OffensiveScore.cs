using Pathfinder.Enum;
using Pathfinder.Interface;
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
			Func<int> pGetBaseAttackBonus)
		{
			Type = pOffensiveType;
			Ability = pAbilityScore;
			GetBaseAttackBonus = pGetBaseAttackBonus;
		}

		private Func<int> GetBaseAttackBonus { get; }

		public OffensiveType Type { get; }
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

		public int BaseAttackBonus { get { return GetBaseAttackBonus(); } }
		public int AbilityModifier { get { return Ability.Modifier; } }
		public int SizeModifier { get; private set; }
		public int MiscModifier { get; private set; }
		public int TemporaryModifier { get; private set; }

		internal int this[string pPropertyName]
		{
			get
			{
				switch (pPropertyName)
				{
					case nameof(Score):
						return Score;
					case nameof(BaseAttackBonus):
						return BaseAttackBonus;
					case nameof(AbilityModifier):
						return AbilityModifier;
					case nameof(SizeModifier):
						return SizeModifier;
					case nameof(MiscModifier):
						return MiscModifier;
					case nameof(TemporaryModifier):
						return TemporaryModifier;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
			set
			{
				switch (pPropertyName)
				{
					case nameof(SizeModifier):
						SizeModifier = value;
						break;
					case nameof(MiscModifier):
						MiscModifier = value;
						break;
					case nameof(TemporaryModifier):
						TemporaryModifier = value;
						break;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
		}
	}
}