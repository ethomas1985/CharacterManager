using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Commands.Character
{
	public class AssignCharacterSkillPoint : ICommand
	{
		public AssignCharacterSkillPoint(Guid pId, int pOriginalVersion, ISkill pSkill, int pRank)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Skill = pSkill;
			Rank = pRank;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public ISkill Skill { get; }
		public int Rank { get; }
	}
}