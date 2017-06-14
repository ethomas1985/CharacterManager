using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.RaceTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
						() => JsonConvert.DeserializeObject<IRace>("{}"),
						Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Missing Required Attribute: {nameof(IRace.Name)}"));
		}

		[Test]
		public void Expected()
		{
			const string raceName = "Testing Item";
			const string raceAdjective = "Testing Item Adjective";
			const string raceDescription = "Testing Item Description";
			const Size raceSize = Size.Diminutive;
			const int raceBaseSpeed = 60;
			IDictionary<AbilityType, int> raceAbilityModifiers = new Dictionary<AbilityType, int>
			{
				[AbilityType.Strength] = 1,
				[AbilityType.Dexterity] = 2,
				[AbilityType.Constitution] = 3,
				[AbilityType.Intelligence] = 4,
				[AbilityType.Wisdom] = 5,
				[AbilityType.Charisma] = 6,
			};
			IEnumerable<ITrait> raceTraits = new[]
			{
				new Trait("Testing Racial Trait", "Testing Racial Trait Description", new Dictionary<string, int>())
			};
			IEnumerable<ILanguage> raceLanguages = new[]
			{
				new Language("Testing Language")
			};

			var item = $"{{" +
					   $"\"{nameof(IRace.Name)}\": \"{raceName}\"," +
					   $"\"{nameof(IRace.Adjective)}\": \"{raceAdjective}\"," +
					   $"\"{nameof(IRace.Description)}\": \"{raceDescription}\"," +
					   $"\"{nameof(IRace.Size)}\": \"{raceSize}\"," +
					   $"\"{nameof(IRace.BaseSpeed)}\": {raceBaseSpeed}," +
					   $"\"{nameof(IRace.AbilityScores)}\": {JsonConvert.SerializeObject(raceAbilityModifiers)}," +
					   $"\"{nameof(IRace.Traits)}\": {JsonConvert.SerializeObject(raceTraits)}," +
					   $"\"{nameof(IRace.Languages)}\": {JsonConvert.SerializeObject(raceLanguages)}" +
					   $"}}";
			var result = JsonConvert.DeserializeObject<IRace>(item);

			var expected = new Race(raceName, raceAdjective, raceDescription, raceSize, raceBaseSpeed, raceAbilityModifiers, raceTraits, raceLanguages);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
