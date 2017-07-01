using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.LanguageTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresValue()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<ILanguage>("{}"),
				Throws.Exception.TypeOf<JsonException>());
		}

		[Test]
		public void Expected()
		{
			const string testingLanguage = "Testing Language";
			var result = JsonConvert.DeserializeObject<ILanguage>($"\"{testingLanguage}\"");
			Assert.That(
				result,
				Is.EqualTo(new Language(testingLanguage)));
		}
	}
}
