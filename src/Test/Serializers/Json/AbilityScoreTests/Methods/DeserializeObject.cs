using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Serializers.Json.AbilityScoreTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresType()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IAbilityScore>("{}"),
				Throws.Exception
					.TypeOf(typeof(JsonException))
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(IAbilityScore.Type)}"));
		}

		[Test]
		public void WithType()
		{
			const string value = "{" +
								 "	Type: \"strength\"" +
								 "}";
			var result = JsonConvert.DeserializeObject<IAbilityScore>(value);
			Assert.That(result.Type, Is.EqualTo(AbilityType.Strength));
		}

		[Test]
		public void WithBase()
		{
			const string value = "{" +
								 "	Type: \"strength\"," +
								 "	Base: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IAbilityScore>(value);
			Assert.That(result.Base, Is.EqualTo(12));
		}

		[Test]
		public void WithEnhanced()
		{
			const string value = "{" +
								 "	Type: \"strength\"," +
								 "	Enhanced: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IAbilityScore>(value);
			Assert.That(result.Enhanced, Is.EqualTo(12));
		}

		[Test]
		public void WithInherent()
		{
			const string value = "{" +
								 "	Type: \"strength\"," +
								 "	Inherent: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IAbilityScore>(value);
			Assert.That(result.Inherent, Is.EqualTo(12));
		}
	}
}
