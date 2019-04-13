using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Model;
using Pathfinder.Model.Items;

namespace Pathfinder.Serializers.Json
{
    public class InventoryJsonSerializer : AbstractJsonSerializer<IInventory>
    {
        protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IInventory pValue)
        {
            pWriter.WriteStartArray();

            foreach (var inventoryItem in pValue)
            {
                pSerializer.Serialize(pWriter, inventoryItem);
            }

            pWriter.WriteEndArray();
        }

        public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue,
            JsonSerializer pSerializer)
        {
            var tokens = JArray.Load(pReader);
            return
                tokens
                    .Select(x => pSerializer.Deserialize<IInventoryItem>(x.CreateReader()))
                    .Aggregate(
                        (IInventory)new Inventory(),
                        (inventory, x) => inventory.Add(x.Item, x.Quantity));
        }

        protected override IInventory DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
        {
            throw new NotImplementedException("Purposefully ignored. JsonConverter.ReadJson is Overridden.");
        }
    }
}
