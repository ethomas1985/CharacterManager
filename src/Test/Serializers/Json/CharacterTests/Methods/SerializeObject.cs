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