using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	internal static class DeityMother
	{
		public static IDeity Skepticus()
		{
			return new Deity("Skepticus");
		}
	}
}
