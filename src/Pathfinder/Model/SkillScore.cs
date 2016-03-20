using Pathfinder.Enum;
using Pathfinder.Interface;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class SkillScore : ISkillScore
	{
		public SkillScore(
			ISkill pSkill, 
			IAbilityScore pAbilityScore,
			int pRanks,
			int pClassModifier,
			int pMiscModifier,
			int pTemporaryModifier,
			int pArmorClassPenalty)
		{
			Assert.ArgumentNotNull(pSkill, nameof(pSkill));
			Assert.ArgumentNotNull(pAbilityScore, nameof(pAbilityScore));
			Assert.AreEqual(pSkill.AbilityType, pAbilityScore.Type);

			Skill = pSkill;
			AbilityScore = pAbilityScore;
			Ranks = pRanks;
			ClassModifier = pClassModifier;
			MiscModifier = pMiscModifier;
			TemporaryModifier = pTemporaryModifier;
			ArmorClassPenalty = pArmorClassPenalty;
		}

		public ISkill Skill { get; }
		private IAbilityScore AbilityScore { get; }
		public AbilityType Ability => AbilityScore.Type;

		public int Ranks { get; }
		public int AbilityModifier => AbilityScore.Modifier;
		public int ClassModifier { get; }
		public int MiscModifier { get; }
		public int TemporaryModifier { get; }
		public int ArmorClassPenalty { get; }
		public int Total
		{
			get
			{
				var total = Values.Sum();

				Tracer.Message(
					pMessage: $"{Skill.Name}[{total}] = {string.Join(" + ", Values)}");

				return total;
			}
		}

		private IEnumerable<int> Values
			=> new List<int>
			{
						Ranks,
						AbilityModifier,
						ClassModifier,
						MiscModifier,
						TemporaryModifier,
						ArmorClassPenalty,
			};

		public override string ToString()
		{
			return $"{Skill.Name}[{Total}] = {string.Join(" + ", Values)}";
		}
	}
}
