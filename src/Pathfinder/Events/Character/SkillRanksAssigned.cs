using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character {
	internal class SkillRanksAssigned : AbstractEvent, IEquatable<SkillRanksAssigned>
	{
		public SkillRanksAssigned(Guid pId, int pVersion, ISkill pSkill, int pRanks)
			: base(pId, pVersion)
		{
			Skill = pSkill;
			Ranks = pRanks;
		}
		public ISkill Skill { get; }
		public int Ranks { get; }

		public override string ToString()
		{
			var pluralOrSingular = Ranks == 1 ? "Rank" : "Ranks";
			return $"Character [{Id}] | {nameof(Skill)} '{Skill.Name}' Assigned {Ranks} {pluralOrSingular}| Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as SkillRanksAssigned);
		}

		public bool Equals(SkillRanksAssigned pOther)
		{
			return base.Equals(pOther)
				   && Equals(Skill, pOther.Skill)
				   && Ranks == pOther.Ranks;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397)
							   ^ (Skill != null ? Skill.GetHashCode() : 0)
							   ^ Ranks.GetHashCode();
				return hashCode;
			}
		}
	}
}