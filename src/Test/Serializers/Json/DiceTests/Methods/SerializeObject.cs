using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.DiceTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var dice = new Dice(1, new Die(6));
			Assert.That(
				() => JsonConvert.SerializeObject(dice),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var dice = new Dice(1, new Die(6));

			var actual = JsonConvert.SerializeObject(dice);

			const string expected = "\"1d6\"";

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
