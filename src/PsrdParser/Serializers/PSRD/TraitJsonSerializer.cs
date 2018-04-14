using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace PsrdParser.Serializers.PSRD
{
	public class TraitJsonSerializer : JsonSerializer<ITrait, string>
	{
		public override ITrait Deserialize(string pValue)
		{
			var jObject = JObject.Parse(pValue);

			var race = getString(jObject, "subtype");
			var name = getString(jObject, "name");

			var body = getString(jObject, "body");
			var description = getString(jObject, "description");

			var text =
				string.IsNullOrEmpty(description)
					? body
					: $"{body}<p>{description}</p>";

			return new Trait(name, text, false, new Dictionary<string, int>());
		}

		public override string Serialize(ITrait pObject)
		{
			throw new NotImplementedException();
		}
	}
}
