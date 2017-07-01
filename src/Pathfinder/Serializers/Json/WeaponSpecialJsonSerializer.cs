using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class WeaponSpecialJsonSerializer : AbstractJsonSerializer<IWeaponSpecial>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IWeaponSpecial pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IWeaponSpecial.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponSpecial.Description), pValue.Description);

			pWriter.WriteEndObject();
		}

		protected override IWeaponSpecial DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(IWeaponSpecial.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IWeaponSpecial.Name)}");
			}

			var description = GetString(pJobject, nameof(IWeaponSpecial.Description));

			return new WeaponSpecial(name, description);
		}
	}
}
