using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Factories
{
	public static class CharacterFactory
	{
		public static ICharacter Create(IRepository<ISkill> pSkillRepository)
		{
			return new Character(pSkillRepository);
		}
	}
}
