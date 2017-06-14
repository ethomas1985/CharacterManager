using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;

namespace Pathfinder.Test.Serializers.Json.WeaponSpecialTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
					    () => JsonConvert.DeserializeObject<IWeaponSpecial>("{}"),
					    Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Missing Required Attribute: {nameof(IWeaponSpecial.Name)}"));
		}

		[Test]
		public void WithName()
		{
			const string name = "Testing Weapon Special";
			var value = $"{{" +
						$"\"{nameof(IWeaponSpecial.Name)}\": \"{name}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponSpecial>(value);
			Assert.That(actual.Name, Is.EqualTo(name));
		}

		[Test]
		public void WithDescription()
		{
			const string name = "Testing Weapon Special";
			const string description = "Testing Weapon Special Description";
			var value = $"{{" +
						$"\"{nameof(IWeaponSpecial.Name)}\": \"{name}\"," +
						$"\"{nameof(IWeaponSpecial.Description)}\": \"{description}\"" +
						$"}}";

			var actual = JsonConvert.DeserializeObject<IWeaponSpecial>(value);
			Assert.That(actual.Description, Is.EqualTo(description));
		}
	}
}
