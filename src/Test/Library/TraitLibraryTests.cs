using System.Linq;
using NUnit.Framework;
using Pathfinder.Library;
using Pathfinder.Properties;
using Pathfinder.Serializers;
using Pathfinder.Serializers.Xml;

namespace Test.Library
{
	[TestFixture]
	public class TraitLibraryTests
	{
		[Test]
		public void Singleton()
		{
			Assert.IsNotNull(new TraitLibrary(new TraitXmlSerializer(), Settings.Default.TraitLibrary));
		}

		[Test]
		public void LoadsFiles()
		{
			var traitLibrary = new TraitLibrary(new TraitXmlSerializer(), Settings.Default.TraitLibrary);
			Assert.IsTrue(traitLibrary.Library.Any());
		}
	}
}
