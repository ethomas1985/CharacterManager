using System.Linq;
using NUnit.Framework;
using Pathfinder.Library;
using Pathfinder.Serializers.Xml;

namespace Pathfinder.Test.Library
{
	[TestFixture]
	public class TraitLibraryTests
	{
		[Test]
		public void Singleton()
		{
			Assert.IsNotNull(new TraitLibrary(new TraitXmlSerializer(), "../../../../resources/Traits/"));
		}

		[Test]
		public void LoadsFiles()
		{
			var traitLibrary = new TraitLibrary(new TraitXmlSerializer(), "../../../../resources/Traits/");
			Assert.IsTrue(traitLibrary.Library.Any());
		}
	}
}
