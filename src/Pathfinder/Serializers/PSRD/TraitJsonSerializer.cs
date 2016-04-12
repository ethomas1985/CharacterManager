using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Serializers.PSRD
{
	public class TraitJsonSerializer : JsonSerializer<ITrait, string>
	{
		public override ITrait Deserialize(string pValue)
		{
			var jObject = JObject.Parse(pValue);

			var race = GetString(jObject, "subtype");
			var name = GetString(jObject, "name");

			var body = GetString(jObject, "body");
			var description = GetString(jObject, "description");

			var text =
				string.IsNullOrEmpty(description)
					? body
					: $"{body}<p>{description}</p>";

			return new Trait(name, text, new Dictionary<string, int>());
		}

		public override string Serialize(ITrait pObject)
		{
			throw new NotImplementedException();
		}
	}
}
