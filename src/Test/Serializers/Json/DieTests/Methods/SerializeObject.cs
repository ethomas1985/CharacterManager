using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.DieTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var die = new Die(6);

			Assert.That(
				() => JsonConvert.SerializeObject(die),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var die = new Die(6);

			var actual = JsonConvert.SerializeObject(die);

			var expected = $"\"d{die.Faces}\"";
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
