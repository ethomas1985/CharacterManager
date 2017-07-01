using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.WeaponComponentTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void WithProficiency()
		{
			const Proficiency proficiency = Proficiency.Exotic;
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.Proficiency)}\": \"{proficiency.ToString().ToCamelCase()}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.Proficiency, Is.EqualTo(proficiency));
		}
		[Test]
		public void WithWeaponType()
		{
			const WeaponType weaponType = WeaponType.Unarmed;
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.WeaponType)}\": \"{weaponType.ToString().ToCamelCase()}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.WeaponType, Is.EqualTo(weaponType));
		}
		[Test]
		public void WithEncumbrance()
		{
			var encumbrance = Encumbrance.TwoHanded;
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.Encumbrance)}\": \"{encumbrance.ToString().ToCamelCase()}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.Encumbrance, Is.EqualTo(encumbrance));
		}
		[Test]
		public void WithSize()
		{
			var weaponSize = WeaponSize.Large;
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.Size)}\": \"{weaponSize.ToString().ToCamelCase()}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.Size, Is.EqualTo(weaponSize));
		}
		[Test]
		public void WithDamageType()
		{
			const DamageType damageType = (DamageType.Bludgeoning | DamageType.Piercing);
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.DamageType)}\": \"{damageType.ToString().ToCamelCase()}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.DamageType, Is.EqualTo(damageType));
		}
		[Test]
		public void WithBaseWeaponDamage()
		{
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.BaseWeaponDamage)}\": [\"1d6\"]" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.BaseWeaponDamage, Is.EquivalentTo(new List<IDice> { new Dice(1, new Die(6)) }));
		}
		[Test]
		public void WithCriticalThreat()
		{
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.CriticalThreat)}\": 20" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.CriticalThreat, Is.EqualTo(20));
		}
		[Test]
		public void WithCriticalMultiplier()
		{
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.CriticalMultiplier)}\": 2" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.CriticalMultiplier, Is.EqualTo(2));
		}
		[Test]
		public void WithRange()
		{
			const int range = 100;
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.Range)}\": \"{range}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.Range, Is.EqualTo(range));
		}
		[Test]
		public void WithSpecials()
		{
			var weaponSpecial = new WeaponSpecial("Testing Weapon Special", "Testing Description");
			var value = $"{{" +
						$"\"{nameof(IWeaponComponent.Specials)}\": [" +
						$"{JsonConvert.SerializeObject(weaponSpecial)}" +
						$"]" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponComponent>(value);
			Assert.That(actual.Specials, Is.EquivalentTo(new List<IWeaponSpecial> { weaponSpecial }));
		}
	}
}
