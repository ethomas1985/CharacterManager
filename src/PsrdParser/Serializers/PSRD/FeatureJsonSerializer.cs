using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace PsrdParser.Serializers.PSRD
{
	internal class FeatureJsonSerializer : JsonSerializer<IEnumerable<IFeature>, string>
	{
		private const string NAME_FIELD = "name";
		private const string SECTIONS_FIELD = "sections";

		public override IEnumerable<IFeature> Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);

			var featuresSection =
				jObject[SECTIONS_FIELD]
					.Children()
					.FirstOrDefault(x => x[NAME_FIELD] != null && ((string) x[NAME_FIELD]).Equals("Class Features"));

			return 
				featuresSection[SECTIONS_FIELD]
					.Children()
					.Select(
						x => new Feature(
							_GetName(x),
							_GetBody(x),
							_GetAbilityTypes(x),
							_GetSubFeatures(x)));
		}

		private static string _GetName(JToken pToken)
		{
			return (string) pToken[NAME_FIELD];
		}

		private static string _GetBody(JToken pToken)
		{
			var body = (string) pToken["body"];

			if (body == null)
			{
				return null;
			}

			var htmlTable = new HtmlDocument();
			htmlTable.Load(new StringReader(body));

			return htmlTable.DocumentNode.InnerText;
		}

		private static FeatureAbilityType _GetAbilityTypes(JToken pToken)
		{
			var value = (string) pToken["ability_types"]?["ability_type"];
			FeatureAbilityType fat; // Stop laughing.
			return Enum.TryParse(value, out fat) ? fat : FeatureAbilityType.Normal;
		}

		private static IEnumerable<ISubFeature> _GetSubFeatures(JToken pToken)
		{
			var subFeatures = 
				pToken[SECTIONS_FIELD]?
					.Children();
			return 
				subFeatures?
					.Select(_CreateSubFeature)
					.ToList();
		}

		private static SubFeature _CreateSubFeature(JToken pToken)
		{
			return new SubFeature(_GetName(pToken), _GetBody(pToken), _GetAbilityTypes(pToken));
		}

		public override string Serialize(IEnumerable<IFeature> pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
