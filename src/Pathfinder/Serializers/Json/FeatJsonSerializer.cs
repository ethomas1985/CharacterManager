using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class FeatJsonSerializer : AbstractJsonSerializer<IFeat>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IFeat pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IFeat.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(IFeat.FeatType), pValue.FeatType);
			WriteProperty(pWriter, pSerializer, nameof(IFeat.Description), pValue.Description);
			WriteProperty(pWriter, pSerializer, nameof(IFeat.Benefit), pValue.Benefit);
			WriteProperty(pWriter, pSerializer, nameof(IFeat.Special), pValue.Special);
			WriteProperty(pWriter, pSerializer, nameof(IFeat.Prerequisites), pValue.Prerequisites);

			if (pValue.IsSpecialized)
			{
				WriteProperty(pWriter, pSerializer, nameof(IFeat.Specialization), pValue.Specialization);
			}

			pWriter.WriteEndObject();
		}

		protected override IFeat DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(IFeat.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IFeat.Name)}");
			}

			var featTypeString = GetString(pJobject, nameof(IFeat.FeatType)).ToPascalCase();
			if (!Enum.TryParse(featTypeString, out FeatType featType))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IFeat.FeatType)}");
			}

			var description = GetString(pJobject, nameof(IFeat.Description));
			var benefit = GetString(pJobject, nameof(IFeat.Benefit));
			var special = GetString(pJobject, nameof(IFeat.Special));

			var prerequisites = pJobject[nameof(IFeat.Prerequisites)]?.Children().Select(x => x.Value<string>());

			var specialization = GetString(pJobject, nameof(IFeat.Specialization));

			return new Feat(name, featType, prerequisites, description, benefit, special, specialization);
		}
	}
}
