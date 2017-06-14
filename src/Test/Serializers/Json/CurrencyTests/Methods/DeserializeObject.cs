using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface.Currency;
using Pathfinder.Model.Currency;

namespace Pathfinder.Test.Serializers.Json.CurrencyTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void InvalidFormat()
		{
			const string stringValue = "\"THIS IS INVALID\"";
			Assert.That(
				() => JsonConvert.DeserializeObject<ICurrency>(stringValue),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Invalid Formatting: [{nameof(ICurrency)}] {stringValue}"));
		}

		[Test]
		public void InvalidDenomination()
		{
			const string denomination = "dollar";
			string stringValue = $"\"1 {denomination}\"";
			Assert.That(
				() => JsonConvert.DeserializeObject<ICurrency>(stringValue),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Unsupported {nameof(ICurrency.Denomination)}: {denomination}"));
		}

		[Test]
		public void When_Empty_String_Then_Copper()
		{
			var result = JsonConvert.DeserializeObject<ICurrency>("\"\"");
			Assert.That(result.Denomination, Is.EqualTo(Copper.DENOMINATION));
		}

		[Test]
		public void When_Empty_String_Then_Zero()
		{
			var result = JsonConvert.DeserializeObject<ICurrency>("\"\"");
			Assert.That(result.Value, Is.EqualTo(0));
		}

		[Test]
		public void WithDenomination()
		{
			const string value = "\"12 cp\"";
			var result = JsonConvert.DeserializeObject<ICurrency>(value);
			Assert.That(result.Denomination, Is.EqualTo(Copper.DENOMINATION));
		}

		[Test]
		public void WithValue()
		{
			const string value = "\"12 cp\"";
			var result = JsonConvert.DeserializeObject<ICurrency>(value);
			Assert.That(result.Value, Is.EqualTo(12));
		}
	}
}
