using System;
using Pathfinder.Interface;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class SkillScore : ISkillScore, IEquatable<ISkillScore>
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

				//Tracer.Message(
				//	pMessage: $"{Skill.Name}[{total}] = {string.Join(" + ", Values)}");

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

		public override bool Equals(object pObject)
		{
			return Equals(pObject as ISkillScore);
		}

		public bool Equals(ISkillScore pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}
			return 
				Equals(Skill, pOther.Skill)
				&& AbilityModifier == pOther.AbilityModifier
				&& Ranks == pOther.Ranks
				&& ClassModifier == pOther.ClassModifier
				&& MiscModifier == pOther.MiscModifier
				&& TemporaryModifier == pOther.TemporaryModifier
				&& ArmorClassPenalty == pOther.ArmorClassPenalty;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Skill?.GetHashCode() ?? 0;
				hashCode = (hashCode*397) ^ (AbilityScore?.GetHashCode() ?? 0);
				hashCode = (hashCode*397) ^ Ranks;
				hashCode = (hashCode*397) ^ ClassModifier;
				hashCode = (hashCode*397) ^ MiscModifier;
				hashCode = (hashCode*397) ^ TemporaryModifier;
				hashCode = (hashCode*397) ^ ArmorClassPenalty;
				return hashCode;
			}
		}
	}
}
