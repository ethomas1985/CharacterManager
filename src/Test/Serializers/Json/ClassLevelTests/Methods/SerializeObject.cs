using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.ClassLevelTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		private static ClassLevel CreateTestingClassLevel()
		{
			return new ClassLevel(
				1,
				new List<int>
				{
					1
				},
				0,
				0,
				0,
				new List<string>(),
				new Dictionary<int, int>
				{
					[1] = 1
				},
				new Dictionary<int, int>
				{
					[1] = 1
				},
				new Dictionary<int, IEnumerable<string>>
				{
					[1] = new List<string> { "Test Spell" }
				});
		}

		[Test]
		public void Success()
		{
			var characterClass = CreateTestingClassLevel();

			Assert.That(
				() => JsonConvert.SerializeObject(characterClass),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var classLevel = CreateTestingClassLevel();
			var actual = JsonConvert.SerializeObject(classLevel);

			var expectedBaseAttackBonuses = 
				classLevel.BaseAttackBonus != null
					? string.Join(",", classLevel.BaseAttackBonus)
					: string.Empty;
			var expectedSpecials =
				classLevel.Specials != null
					? string.Join(",", classLevel.Specials.Select(x => $"\"{x}\""))
					: string.Empty;
			var expectedSpellsPerDay = 
				classLevel.SpellsPerDay != null
					? string.Join(",", classLevel.SpellsPerDay.Select(x => $"\"{x.Key}\":{x.Value}"))
					: string.Empty;
			var expectedSpellsKnown =
				classLevel.SpellsKnown != null
					? string.Join(",", classLevel.SpellsKnown.Select(x => $"\"{x.Key}\":{x.Value}"))
					: string.Empty;
			var expectedSpells =
				classLevel.Spells != null
					? string.Join(",", classLevel.Spells.Select(x => $"\"{x.Key}\":[{string.Join(",", x.Value.Select(y => $"\"{y}\""))}]"))
					: string.Empty;
			var expected = 
				new StringBuilder("{")
					.Append($"\"{nameof(IClassLevel.Level)}\":{classLevel.Level},")
					.Append($"\"{nameof(IClassLevel.BaseAttackBonus)}\":[{expectedBaseAttackBonuses}],")
					.Append($"\"{nameof(IClassLevel.Fortitude)}\":{classLevel.Fortitude},")
					.Append($"\"{nameof(IClassLevel.Reflex)}\":{classLevel.Reflex},")
					.Append($"\"{nameof(IClassLevel.Will)}\":{classLevel.Will},")
					.Append($"\"{nameof(IClassLevel.Specials)}\":[{expectedSpecials}],")
					.Append($"\"{nameof(IClassLevel.SpellsPerDay)}\":{{{expectedSpellsPerDay}}},")
					.Append($"\"{nameof(IClassLevel.SpellsKnown)}\":{{{expectedSpellsKnown}}},")
					.Append($"\"{nameof(IClassLevel.Spells)}\":{{{expectedSpells}}}")
					.Append("}")
					.ToString();

			Assert.That(actual,Is.EqualTo(expected));
		}
	}
}
