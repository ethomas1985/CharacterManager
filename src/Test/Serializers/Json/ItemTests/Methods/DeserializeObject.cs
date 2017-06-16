using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface.Item;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.ItemTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
						() => JsonConvert.DeserializeObject<IItem>("{}"),
						Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Missing Required Attribute: {nameof(IItem.Name)}"));
		}

		[Test]
		public void RequiresItemType()
		{
			const string testingItem = "Testing Item";
			Assert.That(
						() => JsonConvert.DeserializeObject<IItem>($"{{ \"Name\": \"{testingItem}\" }}"),
						Throws.Exception
							  .TypeOf<JsonException>()
							  .With.Message.EqualTo($"Missing Required Attribute: {nameof(IItem.ItemType)}"));
		}

		[Test]
		public void Expected()
		{
			const string itemName = "Testing Item";
			const ItemType itemType = ItemType.Armor;
			const string itemCategory = "Unit Testing";
			const string itemDescription = "For Unit Testing";
			const decimal itemWeight = 12.0m;

			var itemPurse = new Purse(1, 2, 3, 4);

			var weaponComponent = new WeaponComponent(
				Proficiency.None, WeaponType.Unarmed, Encumbrance.None, WeaponSize.Medium, DamageType.Bludgeoning,
				new[] { new Dice(1, new Die(6)) }, 20, 2, 100,
				new[] { new WeaponSpecial("Weapon Special Name", "Weapon Special Description") });

			var armorComponent = new ArmorComponent(1, 2, 3, 4, 0.20m, 25);

			var item = $"{{" +
				$"\"{nameof(IItem.Name)}\": \"{itemName}\"," +
				$"\"{nameof(IItem.ItemType)}\": \"{itemType.ToString().ToCamelCase()}\"," +
				$"\"{nameof(IItem.Category)}\": \"{itemCategory}\"," +
				$"\"{nameof(IItem.Description)}\": \"{itemDescription}\"," +
				$"\"{nameof(IItem.Weight)}\": {itemWeight}," +
				$"\"{nameof(IItem.Cost)}\": {JsonConvert.SerializeObject(itemPurse)}," +
				$"\"{nameof(IItem.WeaponComponent)}\": {JsonConvert.SerializeObject(weaponComponent)}," +
				$"\"{nameof(IItem.ArmorComponent)}\": {JsonConvert.SerializeObject(armorComponent)}," +
				$"}}";
			var result = JsonConvert.DeserializeObject<IItem>(item);

			var expected = new Item(itemName, itemType, itemCategory, itemPurse, itemWeight, itemDescription, weaponComponent, armorComponent);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
