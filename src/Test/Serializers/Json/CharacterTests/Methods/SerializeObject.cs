using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Serializers.Json.CharacterTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var character =
				CharacterMother
					.UnitMcTesterFace();

			Assert.That(
				() => JsonConvert.SerializeObject(character),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var testCharacter =
				CharacterMother
					.UnitMcTesterFace();

			var actual = JsonConvert.SerializeObject(testCharacter)
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);
			var expected = Resources.TestCharacter
                .Replace(": ", ":")
                .Replace("    ", string.Empty)
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty);

			//Console.WriteLine($"result output : {Path.GetFullPath("result.json")}");
			//File.WriteAllText("result.json", actual);
			//Console.WriteLine($"expected output : {Path.GetFullPath("expected.json")}");
			//File.WriteAllText("expected.json", expected);

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
