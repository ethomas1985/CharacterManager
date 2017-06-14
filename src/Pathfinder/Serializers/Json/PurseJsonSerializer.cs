using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Currency;
using Pathfinder.Model.Currency;

namespace Pathfinder.Serializers.Json
{
	public class PurseJsonSerializer : AbstractJsonSerializer<IPurse>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IPurse pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IPurse.Copper), pValue.Copper.Value);
			WriteProperty(pWriter, pSerializer, nameof(IPurse.Silver), pValue.Silver.Value);
			WriteProperty(pWriter, pSerializer, nameof(IPurse.Gold), pValue.Gold.Value);
			WriteProperty(pWriter, pSerializer, nameof(IPurse.Platinum), pValue.Platinum.Value);

			pWriter.WriteEndObject();
		}

		protected override IPurse DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var copper = GetInt(pJobject, nameof(IPurse.Copper));

			var silver = GetInt(pJobject, nameof(IPurse.Silver));

			var gold = GetInt(pJobject, nameof(IPurse.Gold));

			var platinum = GetInt(pJobject, nameof(IPurse.Platinum));

			var purse = new Purse(copper, silver, gold, platinum);
			return purse;
		}
	}
}
