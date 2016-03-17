using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Language : ILanguage
	{
		public Language(string pName)
		{
			Name = pName;
		}

		public string Name { get; }
	}
}
