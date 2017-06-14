using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;

namespace Pathfinder.Test.Serializers.Json.ClassLevelTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void WithLevel()
		{
			var value =
				$"{{ \"{nameof(IClassLevel.Level)}\": {12} }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.Level, Is.EqualTo(12));
		}

		[Test]
		public void WithBaseAttackBonus()
		{
			var value =
				$"{{ \"{nameof(IClassLevel.BaseAttackBonus)}\": [ 7, 5 ] }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.BaseAttackBonus, Is.EqualTo(new[] { 7, 5 }));
		}

		[Test]
		public void WithFortitude()
		{
			var value =
				$"{{ \"{nameof(IClassLevel.Fortitude)}\": {12} }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.Fortitude, Is.EqualTo(12));
		}

		[Test]
		public void WithReflex()
		{
			var value =
				$"{{ \"{nameof(IClassLevel.Reflex)}\": {12} }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.Reflex, Is.EqualTo(12));
		}

		[Test]
		public void WithWill()
		{
			var value =
				$"{{ \"{nameof(IClassLevel.Will)}\": {12} }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.Will, Is.EqualTo(12));
		}

		[Test]
		public void WithSpecials()
		{
			const string specialText = "Testing Special";

			var value =
				$"{{ \"{nameof(IClassLevel.Specials)}\": [\"{specialText}\"] }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.Specials, Is.EqualTo(new[] { specialText }));
		}

		[Test]
		public void WithSpellsPerDay()
		{
			var value =
				$"{{ \"{nameof(IClassLevel.SpellsPerDay)}\": {{ \"1\": 1 }} }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.SpellsPerDay, Is.EqualTo(new Dictionary<int, int> { [1] = 1 }));
		}

		[Test]
		public void WithSpellsKnown()
		{
			var value =
				$"{{ \"{nameof(IClassLevel.SpellsKnown)}\": {{ \"1\": 1 }} }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.SpellsKnown, Is.EqualTo(new Dictionary<int, int> { [1] = 1 }));
		}

		[Test]
		public void WithSpells()
		{
			const string testSpell = "Test Spell";
			var value =
				$"{{ \"{nameof(IClassLevel.Spells)}\": {{ \"1\": [ \"{testSpell}\" ] }} }}";
			var result = JsonConvert.DeserializeObject<IClassLevel>(value);
			Assert.That(result.Spells,
				Is.EqualTo(new Dictionary<int, IEnumerable<string>> { [1] = new[] { testSpell } }));
		}
	}
}
