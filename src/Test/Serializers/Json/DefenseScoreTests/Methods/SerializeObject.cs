using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.DefenseScoreTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		private static DefenseScore CreateTestingDefenseScore()
		{
			return new DefenseScore(
				pDefensiveType: DefensiveType.ArmorClass,
				pArmorBonus: 1,
				pShieldBonus: 1,
				pDexterity: new AbilityScore(AbilityType.Dexterity, 1),
				pSize: (int)Size.Medium,
				pNaturalBonus: 1,
				pDeflectBonus: 1,
				pDodgeBonus: 1,
				pTemporaryBonus: 1);
		}

		[Test]
		public void Success()
		{
			var defenseScore = CreateTestingDefenseScore();

			Assert.That(
				() => JsonConvert.SerializeObject(defenseScore),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var defenseScore = CreateTestingDefenseScore();

			var actual = JsonConvert.SerializeObject(defenseScore);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IDefenseScore.Type)}\":\"{defenseScore.Type.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IDefenseScore.Score)}\":{defenseScore.Score},")
					.Append($"\"{nameof(IDefenseScore.ArmorBonus)}\":{defenseScore.ArmorBonus},")
					.Append($"\"{nameof(IDefenseScore.ShieldBonus)}\":{defenseScore.ShieldBonus},")
					.Append($"\"{nameof(IDefenseScore.DexterityModifier)}\":{defenseScore.DexterityModifier},")
					.Append($"\"{nameof(IDefenseScore.SizeModifier)}\":{defenseScore.SizeModifier},")
					.Append($"\"{nameof(IDefenseScore.DeflectBonus)}\":{defenseScore.DeflectBonus},")
					.Append($"\"{nameof(IDefenseScore.DodgeBonus)}\":{defenseScore.DodgeBonus},")
					.Append($"\"{nameof(IDefenseScore.NaturalBonus)}\":{defenseScore.NaturalBonus},")
					.Append($"\"{nameof(IDefenseScore.TemporaryBonus)}\":{defenseScore.TemporaryBonus}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
