using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.AbilityScoreTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var abilityScore = new AbilityScore(AbilityType.Strength, 0);
			Assert.That(
				() => JsonConvert.SerializeObject(abilityScore),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var abilityScore = new AbilityScore(AbilityType.Strength, 0);

			var actual = JsonConvert.SerializeObject(abilityScore);

			Assert.That(actual,
				Is.EqualTo(
					$"{{" +
					$"\"{nameof(IAbilityScore.Type)}\":\"{AbilityType.Strength.ToString().ToCamelCase()}\"," +
					$"\"{nameof(IAbilityScore.Score)}\":0," +
					$"\"{nameof(IAbilityScore.Modifier)}\":-5," +
					$"\"{nameof(IAbilityScore.Base)}\":0," +
					$"\"{nameof(IAbilityScore.Enhanced)}\":0," +
					$"\"{nameof(IAbilityScore.Inherent)}\":0," +
					$"\"{nameof(IAbilityScore.Penalty)}\":0," +
					$"\"{nameof(IAbilityScore.Temporary)}\":0" +
					$"}}"));
		}
	}
}
