using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Pathfinder.Enum;
using Pathfinder.Interface;
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
							GetName(x),
							GetBody(x),
							GetAbilityTypes(x),
							GetSubFeatures(x)));
		}

		private static string GetName(JToken pToken)
		{
			return (string) pToken[NAME_FIELD];
		}

		private static string GetBody(JToken pToken)
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

		private static FeatureAbilityTypes GetAbilityTypes(JToken pToken)
		{
			var value = (string) pToken["ability_types"]?["ability_type"];
			FeatureAbilityTypes fat; // Stop laughing.
			return Enum.TryParse(value, out fat) ? fat : FeatureAbilityTypes.Normal;
		}

		private static IEnumerable<ISubFeature> GetSubFeatures(JToken pToken)
		{
			var subFeatures = 
				pToken[SECTIONS_FIELD]?
					.Children();
			return 
				subFeatures?
					.Select(CreateSubFeature)
					.ToList();
		}

		private static SubFeature CreateSubFeature(JToken pToken)
		{
			return new SubFeature(GetName(pToken), GetBody(pToken), GetAbilityTypes(pToken));
		}

		public override string Serialize(IEnumerable<IFeature> pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
