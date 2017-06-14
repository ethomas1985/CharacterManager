using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Model.Items;

namespace Pathfinder.Serializers.Json
{
	public class InventoryJsonSerializer : AbstractJsonSerializer<IInventory>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IInventory pValue)
		{
			pWriter.WriteStartArray();

			foreach (var pair in pValue)
			{
				pWriter.WriteStartObject();
				
				WriteProperty(pWriter, pSerializer, nameof(KeyValuePair<IItem, int>.Key), pair.Key);
				WriteProperty(pWriter, pSerializer, nameof(KeyValuePair<IItem, int>.Value), pair.Value);

				pWriter.WriteEndObject();
			}

			pWriter.WriteEndArray();
		}
		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			var tokens = JArray.Load(pReader);
			return
				tokens
					.Select(x => pSerializer.Deserialize<KeyValuePair<IItem, int>>(x.CreateReader()))
					.Aggregate(
						(IInventory)new Inventory(),
						(inventory, x) => inventory.Add(x.Key, x.Value));
		}

		protected override IInventory DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException("Purposefully ignored. JsonConverter.ReadJson is Overridden.");
		}
	}
}
