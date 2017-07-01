using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class SubFeatureJsonSerializer : AbstractJsonSerializer<ISubFeature>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ISubFeature pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ISubFeature.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(ISubFeature.Body), pValue.Body);
			WriteProperty(pWriter, pSerializer, nameof(ISubFeature.AbilityType), pValue.AbilityType);

			pWriter.WriteEndObject();
		}

		protected override ISubFeature DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(ISubFeature.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ISubFeature.Name)}");
			}

			var body = GetString(pJobject, nameof(ISubFeature.Body));

			var featTypeString = GetString(pJobject, nameof(ISubFeature.AbilityType)).ToPascalCase();
			if (!Enum.TryParse(featTypeString, out FeatureAbilityType featType))
			{
				featType = FeatureAbilityType.Normal;
			}

			return new SubFeature(name, body, featType);
		}
	}
}
