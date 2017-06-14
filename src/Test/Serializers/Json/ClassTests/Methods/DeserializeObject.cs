using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.ClassTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IClass>("{}"),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(IClass.Name)}"));
		}
		[Test]
		public void RequiresHitDie()
		{
			const string name = "Test Class";
			Assert.That(
				() => JsonConvert.DeserializeObject<IClass>($"{{ \"{nameof(IClass.Name)}\": \"{name}\" }}"),
				Throws.Exception
						.TypeOf<JsonException>()
						.With.Message.EqualTo($"Missing Required Attribute: {nameof(IClass.HitDie)}"));
		}

		[Test]
		public void WithName()
		{
			const string name = "Test Class";
			var value =
				 "{" +
				$"\"{nameof(IClass.Name)}\": \"{name}\"," +
				$"\"{nameof(IClass.HitDie)}\": \"d8\"" +
				 "}";
			var result = JsonConvert.DeserializeObject<IClass>(value);
			Assert.That(result.Name, Is.EqualTo(name));
		}

		[Test]
		public void WithHitDie()
		{
			const string name = "Test Class";
			var value =
				"{" +
				$"\"{nameof(IClass.Name)}\": \"{name}\"," +
				$"\"{nameof(IClass.HitDie)}\": \"d8\"" +
				"}";
			var result = JsonConvert.DeserializeObject<IClass>(value);
			Assert.That(result.HitDie, Is.EqualTo(new Die(8)));
		}

		[Test]
		public void WithAlignments()
		{
			const string name = "Test Class";
			var value =
				"{" +
				$"\"{nameof(IClass.Name)}\": \"{name}\"," +
				$"\"{nameof(IClass.HitDie)}\": \"d8\"," +
				$"\"{nameof(IClass.Alignments)}\": [" +
				$"\"{Alignment.LawfulGood.ToString().ToCamelCase()}\", " +
				$"\"{Alignment.NeutralGood.ToString().ToCamelCase()}\", " +
				$"\"{Alignment.ChaoticGood.ToString().ToCamelCase()}\"]" +
				"}";
			var result = JsonConvert.DeserializeObject<IClass>(value);
			Assert.That(
				result.Alignments,
				Is.EquivalentTo(
					new List<Alignment> { Alignment.LawfulGood, Alignment.NeutralGood, Alignment.ChaoticGood }));
		}

		[Test]
		public void WithSkillAddend()
		{
			const string name = "Test Class";
			var value =
				"{" +
				$"\"{nameof(IClass.Name)}\": \"{name}\"," +
				$"\"{nameof(IClass.HitDie)}\": \"d8\"," +
				$"\"{nameof(IClass.SkillAddend)}\": 3" +
				"}";
			var result = JsonConvert.DeserializeObject<IClass>(value);
			Assert.That(result.SkillAddend, Is.EqualTo(3));
		}

		[Test]
		public void WithSkills()
		{
			const string name = "Test Class";
			const string testSkill = "Test Skill";
			var value =
				"{" +
				$"\"{nameof(IClass.Name)}\": \"{name}\"," +
				$"\"{nameof(IClass.HitDie)}\": \"d8\"," +
				$"\"{nameof(IClass.Skills)}\": [\"{testSkill}\"]" +
				"}";
			var result = JsonConvert.DeserializeObject<IClass>(value);
			Assert.That(result.Skills, Is.EquivalentTo(new[] { testSkill }));
		}

		[Test]
		public void WithClassLevels()
		{
			const string name = "Test Class";
			var classLevel
				= new ClassLevel(
					pLevel: 1,
					pBaseAttackBonus: new[] { 1 },
					pFortitude: 1,
					pReflex: 1,
					pWill: 1,
					pSpecials: new[] { "Test Special" },
					pSpellsKnown: new Dictionary<int, int> { [1] = 1 },
					pSpellsPerDay: new Dictionary<int, int> { [1] = 1 },
					pSpells: new Dictionary<int, IEnumerable<string>> { [1] = new[] { "Test Spell" } });

			var value =
				"{" +
				$"\"{nameof(IClass.Name)}\": \"{name}\"," +
				$"\"{nameof(IClass.HitDie)}\": \"d8\"," +
				$"\"{nameof(IClass.ClassLevels)}\": [{JsonConvert.SerializeObject(classLevel)}]" +
				"}";
			var result = JsonConvert.DeserializeObject<IClass>(value);
			Assert.That(result.ClassLevels,
				Is.EquivalentTo(new[] { classLevel }));
		}

		[Test]
		public void WithFeatures()
		{
			const string name = "Test Class";
			const string testFeature = "Test Feature";
			var value =
				$"{{" +
				$"\"{nameof(IClass.Name)}\": \"{name}\"," +
				$"\"{nameof(IClass.HitDie)}\": \"d8\"," +
				$"\"{nameof(IClass.Features)}\": [\"{testFeature}\"]" +
				$"}}";
			var result = JsonConvert.DeserializeObject<IClass>(value);
			Assert.That(result.Features, Is.EquivalentTo(new[] { testFeature }));
		}
	}
}
