using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.WeaponSpecialTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var weaponSpecial =
				new WeaponSpecial(
					"Test Weapon Special",
					"Test Description");

			Assert.That(
						() => JsonConvert.SerializeObject(weaponSpecial),
						Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var weaponSpecial =
				new WeaponSpecial(
								  "Test Weapon Special",
								  "Test Description");

			var actual = JsonConvert.SerializeObject(weaponSpecial);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(IWeaponSpecial.Name)}\":\"{weaponSpecial.Name}\",")
					.Append($"\"{nameof(IWeaponSpecial.Description)}\":\"{weaponSpecial.Description}\"")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
