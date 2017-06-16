using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class EventJsonSerializer : AbstractJsonSerializer<IEvent>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IEvent pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IEvent.Title), pValue.Title);
			WriteProperty(pWriter, pSerializer, nameof(IEvent.Description), pValue.Description);
			WriteProperty(pWriter, pSerializer, nameof(IEvent.ExperiencePoints), pValue.ExperiencePoints);

			pWriter.WriteEndObject();
		}

		protected override IEvent DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var title = GetString(pJobject, nameof(IEvent.Title));
			if (string.IsNullOrWhiteSpace(title))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IEvent.Title)}");
			}
			var description = GetString(pJobject, nameof(IEvent.Description)) ?? string.Empty;

			var xp = GetInt(pJobject, nameof(IEvent.ExperiencePoints));

			return new Event(title, description, xp);
		}
	}
}
