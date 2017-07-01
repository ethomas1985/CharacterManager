using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class TraitJsonSerializer : AbstractJsonSerializer<ITrait>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ITrait pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ITrait.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(ITrait.Text), pValue.Text);

			WriteProperty(pWriter, pSerializer, nameof(ITrait.PropertyModifiers), pValue.PropertyModifiers);

			pWriter.WriteEndObject();
		}

		protected override ITrait DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(ITrait.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ITrait.Name)}");
			}

			var text = GetString(pJobject, nameof(ITrait.Text));

			var propertyModifiers = GetValuesFromHashObject<string, int>(pSerializer, pJobject, nameof(ITrait.PropertyModifiers));

			return new Trait(name, text, propertyModifiers);
		}
	}
}
