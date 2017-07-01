using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class EventJsonSerializer : AbstractJsonSerializer<IExperienceEvent>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IExperienceEvent pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IExperienceEvent.Title), pValue.Title);
			WriteProperty(pWriter, pSerializer, nameof(IExperienceEvent.Description), pValue.Description);
			WriteProperty(pWriter, pSerializer, nameof(IExperienceEvent.ExperiencePoints), pValue.ExperiencePoints);

			pWriter.WriteEndObject();
		}

		protected override IExperienceEvent DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var title = GetString(pJobject, nameof(IExperienceEvent.Title));
			if (string.IsNullOrWhiteSpace(title))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IExperienceEvent.Title)}");
			}
			var description = GetString(pJobject, nameof(IExperienceEvent.Description)) ?? string.Empty;

			var xp = GetInt(pJobject, nameof(IExperienceEvent.ExperiencePoints));

			return new ExperienceEvent(title, description, xp);
		}
	}
}
