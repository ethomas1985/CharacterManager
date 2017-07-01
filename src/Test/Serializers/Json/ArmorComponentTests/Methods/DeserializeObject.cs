using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Test.Serializers.Json.ArmorComponentTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void Fail()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<IArmorComponent>("{}"),
				Throws.Nothing);
		}

		[Test]
		public void WithArmorBonus()
		{
			const string value = "{" +
								 "	ArmorBonus: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IArmorComponent>(value);
			Assert.That(result.ArmorBonus, Is.EqualTo(12));
		}

		[Test]
		public void WithShieldBonus()
		{
			const string value = "{" +
								 "	ShieldBonus: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IArmorComponent>(value);
			Assert.That(result.ShieldBonus, Is.EqualTo(12));
		}

		[Test]
		public void WithMaximumDexterityBonus()
		{
			const string value = "{" +
								 "	MaximumDexterityBonus: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IArmorComponent>(value);
			Assert.That(result.MaximumDexterityBonus, Is.EqualTo(12));
		}

		[Test]
		public void WithArmorCheckPenalty()
		{
			const string value = "{" +
								 "	ArmorCheckPenalty: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IArmorComponent>(value);
			Assert.That(result.ArmorCheckPenalty, Is.EqualTo(12));
		}

		[Test]
		public void WithArcaneSpellFailureChance()
		{
			const string value = "{" +
								 "	ArcaneSpellFailureChance: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IArmorComponent>(value);
			Assert.That(result.ArcaneSpellFailureChance, Is.EqualTo(12));
		}

		[Test]
		public void WithSpeedModifier()
		{
			const string value = "{" +
								 "	SpeedModifier: 12" +
								 "}";
			var result = JsonConvert.DeserializeObject<IArmorComponent>(value);
			Assert.That(result.SpeedModifier, Is.EqualTo(12));
		}
	}
}
