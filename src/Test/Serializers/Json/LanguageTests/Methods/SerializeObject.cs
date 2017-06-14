using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Model;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.LanguageTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var language = new Language("Testing Language");
			Assert.That(
				() => JsonConvert.SerializeObject(language),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			const string name = "Testing Language";
			var language = new Language(name);
			var actual = JsonConvert.SerializeObject(language);

			Assert.That(actual, Is.EqualTo($"\"{name}\""));
		}
	}
}
