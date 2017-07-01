using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.OffensiveScoreTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var offensiveScore = new OffensiveScore(OffensiveType.Melee, new AbilityScore(AbilityType.Strength, 10), 1, (int)Size.Medium, 0, 0);

			Assert.That(
				() => JsonConvert.SerializeObject(offensiveScore),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var offensiveScore = new OffensiveScore(OffensiveType.Melee, new AbilityScore(AbilityType.Strength, 10), 1, (int)Size.Medium, 0, 0);
			var actual = JsonConvert.SerializeObject(offensiveScore);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IOffensiveScore.Type)}\":\"{offensiveScore.Type.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IOffensiveScore.AbilityModifier)}\":{offensiveScore.AbilityModifier},")
					.Append($"\"{nameof(IOffensiveScore.BaseAttackBonus)}\":{offensiveScore.BaseAttackBonus},")
					.Append($"\"{nameof(IOffensiveScore.SizeModifier)}\":{offensiveScore.SizeModifier},")
					.Append($"\"{nameof(IOffensiveScore.MiscModifier)}\":{offensiveScore.MiscModifier},")
					.Append($"\"{nameof(IOffensiveScore.TemporaryModifier)}\":{offensiveScore.TemporaryModifier},")
					.Append($"\"{nameof(IOffensiveScore.Score)}\":{offensiveScore.Score}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
