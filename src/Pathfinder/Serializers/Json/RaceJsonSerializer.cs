using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class RaceJsonSerializer : AbstractJsonSerializer<IRace>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IRace pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IRace.Name), pValue.Name);
			WriteProperty(pWriter, pSerializer, nameof(IRace.Adjective), pValue.Adjective);
			WriteProperty(pWriter, pSerializer, nameof(IRace.Description), pValue.Description);
			WriteProperty(pWriter, pSerializer, nameof(IRace.Size), pValue.Size);
			WriteProperty(pWriter, pSerializer, nameof(IRace.BaseSpeed), pValue.BaseSpeed);

			if (pValue.AbilityScores.Any())
			{
				pWriter.WritePropertyName(nameof(IRace.AbilityScores));
				pWriter.WriteStartObject();
				foreach (var keyValue in pValue.AbilityScores)
				{
					WriteProperty(pWriter, pSerializer, keyValue.Key.ToString(), keyValue.Value);
				}
				pWriter.WriteEndObject();
			}

			if (pValue.Traits.Any())
			{
				WriteSimpleArray(pWriter, pSerializer, nameof(IRace.Traits), pValue.Traits);
			}

			if (pValue.Languages.Any())
			{
				WriteSimpleArray(pWriter, pSerializer, nameof(IRace.Languages), pValue.Languages);
			}

			pWriter.WriteEndObject();
		}

		protected override IRace DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var name = GetString(pJobject, nameof(IRace.Name));
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IRace.Name)}");
			}

			var adjective = GetString(pJobject, nameof(IRace.Adjective));
			var description = GetString(pJobject, nameof(IRace.Description));
			var size =
				pSerializer.Deserialize<Size>(
					pJobject.SelectToken(nameof(IRace.Size)).CreateReader());
			var baseSpeed = GetInt(pJobject, nameof(IRace.BaseSpeed));

			var abilityScores = GetValuesFromHashObject<AbilityType, int>(pSerializer, pJobject, nameof(IRace.AbilityScores));

			var traitTokens = pJobject.SelectToken(nameof(IRace.Traits)).Children();
			var traits = traitTokens.Select(x => pSerializer.Deserialize<ITrait>(x.CreateReader()));

			var languages = GetValuesFromArray<ILanguage>(pSerializer, pJobject, nameof(IRace.Languages));

			return
				new Race(name, adjective, description, size, baseSpeed, abilityScores, traits, languages);
		}
	}
}
