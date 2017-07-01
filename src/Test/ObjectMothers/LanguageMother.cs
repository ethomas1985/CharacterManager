using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
	public static class LanguageMother
	{
		public static ILanguage OldTestese()
		{
			return new Language("Old Test-ese");
		}

		public static ILanguage OldTestist()
		{
			return new Language("Old Test-ish");
		}

		public static ILanguage MiddleTestese()
		{
			return new Language("Middle Test-ese");
		}

		public static ILanguage MockLanguage()
		{
			return new Language("Mock Language");
		}
	}
}