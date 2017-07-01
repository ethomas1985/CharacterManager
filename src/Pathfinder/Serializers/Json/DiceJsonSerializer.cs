using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class DiceJsonSerializer : AbstractJsonSerializer<IDice>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IDice pValue)
		{
			pWriter.WriteValue($"{pValue.DieCount}d{pValue.Die.Faces}");
		}

		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			var stringValue = pReader.Value.ToString();
			var regex = new Regex(@"(\d+)d(\d+)");
			Match isMatch;
			if (string.IsNullOrWhiteSpace(stringValue) || !(isMatch = regex.Match(stringValue)).Success)
			{
				throw new JsonException($"Invalid Formatting: [{nameof(IDice)}] \"{stringValue}\"");
			}
			var count = isMatch.Groups[1].Value.AsInt();
			if (count < 1)
			{
				throw new JsonException("Invalid Input: The number of dice must be greater than one.");
			}
			var faces = isMatch.Groups[2].Value.AsInt();
			if (faces < 4)
			{
				throw new JsonException("Invalid Input: The number of faces on a Die must be greater than four.");
			}

			return new Dice(count, new Die(faces));
		}

		protected override IDice DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			throw new NotImplementedException("Purposefully ignored. JsonConverter.ReadJson is Overridden.");
		}
	}
}
