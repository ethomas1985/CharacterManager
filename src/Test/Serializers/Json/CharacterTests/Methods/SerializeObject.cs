using Newtonsoft.Json;
using NUnit.Framework;

namespace Pathfinder.Test.Serializers.Json.CharacterTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var character =
				CharacterJsonSerializerUtils
					.GetTestCharacter();

			Assert.That(
				() => JsonConvert.SerializeObject(character),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var testCharacter =
				CharacterJsonSerializerUtils
					.GetTestCharacter();

			var actual = JsonConvert.SerializeObject(testCharacter, Formatting.Indented);
			var expected = Resources.TestCharacter.Replace("\t", "  ");

			//Console.WriteLine($"result output : {Path.GetFullPath("result.json")}");
			//File.WriteAllText("result.json", actual);
			//Console.WriteLine($"expected output : {Path.GetFullPath("expected.json")}");
			//File.WriteAllText("expected.json", expected);

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}