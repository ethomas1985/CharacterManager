using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Model;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.WeaponComponentTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var weaponComponent =
				new WeaponComponent(
					Proficiency.None,
					WeaponType.None,
					Encumbrance.None,
					WeaponSize.Medium,
					DamageType.None,
					new IDice[] { new Dice(1, new Die(6)) },
					20,
					2,
					0,
					new IWeaponSpecial[]
					{
						new WeaponSpecial(
							"Test Weapon Special",
							"Test Description")
					});

			Assert.That(
				() => JsonConvert.SerializeObject(weaponComponent),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var weaponComponent =
				new WeaponComponent(
					Proficiency.None,
					WeaponType.None,
					Encumbrance.None,
					WeaponSize.Medium,
					DamageType.None,
					new IDice[] { new Dice(1, new Die(6)) },
					20,
					2,
					0,
					new IWeaponSpecial[]
					{
						new WeaponSpecial(
							"Test Weapon Special",
							"Test Description")
					});

			var actual = JsonConvert.SerializeObject(weaponComponent);

			var diceString = string.Join(",", weaponComponent.BaseWeaponDamage.Select(JsonConvert.SerializeObject));
			var weaponSpecialsString = string.Join(",", weaponComponent.Specials.Select(JsonConvert.SerializeObject));
			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IWeaponComponent.Proficiency)}\":\"{weaponComponent.Proficiency.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IWeaponComponent.WeaponType)}\":\"{weaponComponent.WeaponType.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IWeaponComponent.Encumbrance)}\":\"{weaponComponent.Encumbrance.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IWeaponComponent.Size)}\":\"{weaponComponent.Size.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IWeaponComponent.DamageType)}\":\"{weaponComponent.DamageType.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(IWeaponComponent.BaseWeaponDamage)}\":[{diceString}],")
					.Append($"\"{nameof(IWeaponComponent.CriticalThreat)}\":{weaponComponent.CriticalThreat},")
					.Append($"\"{nameof(IWeaponComponent.CriticalMultiplier)}\":{weaponComponent.CriticalMultiplier},")
					.Append($"\"{nameof(IWeaponComponent.Range)}\":{weaponComponent.Range},")
					.Append($"\"{nameof(IWeaponComponent.Specials)}\":[{weaponSpecialsString}]")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
