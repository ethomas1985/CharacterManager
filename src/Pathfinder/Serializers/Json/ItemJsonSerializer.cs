using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface.Model.Currency;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class ItemJsonSerializer : AbstractJsonSerializer<IItem>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IItem pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IItem.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(IItem.ItemType), pValue.ItemType);
			WriteProperty(pWriter, pSerializer, nameof(IItem.Category), pValue.Category);
			WriteProperty(pWriter, pSerializer, nameof(IItem.Description), pValue.Description);
			WriteProperty(pWriter, pSerializer, nameof(IItem.Weight), pValue.Weight);
			WriteProperty(pWriter, pSerializer, nameof(IItem.Cost), pValue.Cost);
			WriteProperty(pWriter, pSerializer, nameof(IItem.WeaponComponent), pValue.WeaponComponent);
			WriteProperty(pWriter, pSerializer, nameof(IItem.ArmorComponent), pValue.ArmorComponent);

			pWriter.WriteEndObject();
		}

		protected override IItem DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(IItem.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IItem.Name)}");
			}

			var itemTypeString = GetString(pJobject, nameof(IItem.ItemType)).ToPascalCase();
			if (!Enum.TryParse(itemTypeString, out ItemType featType))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IItem.ItemType)}");
			}

			var category = GetString(pJobject, nameof(IItem.Category));
			var description = GetString(pJobject, nameof(IItem.Description));

			var purseToken = pJobject.SelectToken(nameof(IItem.Cost));
			var purse = purseToken != null
				? pSerializer.Deserialize<IPurse>(purseToken.CreateReader())
				: new Purse(0);

			var weight = GetDecimal(pJobject, nameof(IItem.Weight));

			var weaponComponentToken = pJobject.SelectToken(nameof(IItem.WeaponComponent));
			var weaponComponent = weaponComponentToken != null
				? pSerializer.Deserialize<IWeaponComponent>(weaponComponentToken.CreateReader())
				: null;

			var armorComponentToken = pJobject.SelectToken(nameof(IItem.ArmorComponent));
			var armorComponent = armorComponentToken != null
				? pSerializer.Deserialize<IArmorComponent>(armorComponentToken.CreateReader())
				: null;

			return new Item(name, featType, category, purse, weight, description, weaponComponent, armorComponent);
		}
	}
}
