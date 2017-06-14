using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class ExperienceJsonSerializer : AbstractJsonSerializer<IExperience>
	{
		protected override void SerializeToJson(
			JsonWriter pWriter, JsonSerializer pSerializer, IExperience pValue)
		{
			pWriter.WriteStartArray();

			foreach (IEvent experienceEvent in pValue)
			{
				pSerializer.Serialize(pWriter, experienceEvent);
			}

			pWriter.WriteEndArray();
		}

		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			IExperience experience = new Experience();

			var tokens = JArray.Load(pReader);
			return
				tokens
					.Aggregate(
						experience,
						(current, token) => current.Append(pSerializer.Deserialize<IEvent>(token.CreateReader())));
		}

		protected override IExperience DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException("Purposefully ignored. JsonConverter.ReadJson is Overridden.");
		}
	}
}
