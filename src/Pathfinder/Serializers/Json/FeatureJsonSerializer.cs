using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class FeatureJsonSerializer : AbstractJsonSerializer<IFeature>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IFeature pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IFeature.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(IFeature.Body), pValue.Body);
			WriteProperty(pWriter, pSerializer, nameof(IFeature.AbilityType), pValue.AbilityType);

			if (pValue.SubFeatures != null)
			{
				WriteArrayProperty(pWriter, pSerializer, nameof(IFeature.SubFeatures), pValue.SubFeatures);
			}

			pWriter.WriteEndObject();
		}

		protected override IFeature DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(IFeature.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IFeature.Name)}");
			}

			var body = GetString(pJobject, nameof(IFeature.Body));

			var featTypeString = GetString(pJobject, nameof(IFeature.AbilityType)).ToPascalCase();
			if (!Enum.TryParse(featTypeString, out FeatureAbilityType featType))
			{
				featType = FeatureAbilityType.Normal;
			}

			var subFeatures = GetValuesFromArray<ISubFeature>(pSerializer, pJobject, nameof(IFeature.SubFeatures));
			return new Feature(name, body, featType, subFeatures );
		}
	}
}
