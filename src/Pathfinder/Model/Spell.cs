using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Spell : ISpell
	{
		public Spell(string pName)
		{
			Name = pName;
		}

		public string Name { get; }
	}
}
