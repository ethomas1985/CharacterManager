using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Items;

namespace Pathfinder.Serializers.Json {
    public class InventoryItemJsonSerializer : AbstractJsonSerializer<IInventoryItem>
    {
        protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IInventoryItem pValue)
        {
            pWriter.WriteStartObject();

            WriteProperty(pWriter, pSerializer, nameof(IInventoryItem.Item), pValue.Item);
            WriteProperty(pWriter, pSerializer, nameof(IInventoryItem.Quantity), pValue.Quantity);

            pWriter.WriteEndObject();
        }

        protected override IInventoryItem DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
        {
            IItem item = pSerializer.Deserialize<IItem>(pJobject.SelectToken(nameof(IInventoryItem.Item)).CreateReader());
            int quantity = GetInt(pJobject, nameof(IInventoryItem.Quantity));
            return new InventoryItem(item, quantity);
        }
    }
}
