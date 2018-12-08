using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class SpellComponentJsonSerializer : AbstractJsonSerializer<ISpellComponent>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, ISpellComponent pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ISpellComponent.ComponentType), pValue.ComponentType);
			WriteProperty(pWriter, pSerializer, nameof(ISpellComponent.Description), pValue.Description);

			pWriter.WriteEndObject();
		}

		protected override ISpellComponent DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var abilityTypeString = GetString(pJobject, nameof(ISpellComponent.ComponentType));
			if (!Enum.TryParse(abilityTypeString, out ComponentType componentType))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ISpellComponent.ComponentType)}");
			}

			var description = GetString(pJobject, nameof(ISpellComponent.Description));
			return new SpellComponent(componentType, description);
		}
	}
}
