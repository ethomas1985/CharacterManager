using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class WeaponSpecial : IWeaponSpecial
	{
		public WeaponSpecial(string pName, string pDescription)
		{
			Name = pName;
			Description = pDescription;
		}
		public string Name { get; }
		public string Description { get; }
	}
}