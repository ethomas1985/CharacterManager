using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Items;

namespace Pathfinder.Test.Serializers.Json.ArmorComponentTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var weaponComponent =
				new ArmorComponent(1, 1, 6, 6, 0.20m, 30);

			Assert.That(
				() => JsonConvert.SerializeObject(weaponComponent),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var weaponComponent =
				new ArmorComponent(1, 1, 6, 6, 0.20m, 30);

			var actual = JsonConvert.SerializeObject(weaponComponent);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IArmorComponent.ArmorBonus)}\":{weaponComponent.ArmorBonus},")
					.Append($"\"{nameof(IArmorComponent.ShieldBonus)}\":{weaponComponent.ShieldBonus},")
					.Append($"\"{nameof(IArmorComponent.MaximumDexterityBonus)}\":{weaponComponent.MaximumDexterityBonus},")
					.Append($"\"{nameof(IArmorComponent.ArmorCheckPenalty)}\":{weaponComponent.ArmorCheckPenalty},")
					.Append($"\"{nameof(IArmorComponent.ArcaneSpellFailureChance)}\":{weaponComponent.ArcaneSpellFailureChance},")
					.Append($"\"{nameof(IArmorComponent.SpeedModifier)}\":{weaponComponent.SpeedModifier}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
