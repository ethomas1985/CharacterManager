using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class DieJsonSerializer : AbstractJsonSerializer<IDie>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IDie pValue)
		{
			pWriter.WriteValue($"d{pValue.Faces}");
		}

		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			var stringValue = pReader.Value.ToString();
			var regex = new Regex(@"d(\d+)");
			Match isMatch;
			if (string.IsNullOrWhiteSpace(stringValue) || !(isMatch = regex.Match(stringValue)).Success)
			{
				throw new JsonException($"Invalid Formatting: [{nameof(IDie)}] \"{stringValue}\"");
			}
			var faces = isMatch.Groups[1].Value.AsInt();
			if (faces < 4)
			{
				throw new JsonException("Invalid Input: The number of faces on a Die must be greater than four.");
			}

			return new Die(faces);
		}

		protected override IDie DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException("Purposefully ignored. JsonConverter.ReadJson is Overridden.");
		}
	}
}
