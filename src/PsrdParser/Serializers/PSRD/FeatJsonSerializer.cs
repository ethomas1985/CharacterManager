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
	public class FeatJsonSerializer : JsonSerializer<IFeat, string>
	{
		private const string NAME_FIELD = "name";
		private const string SECTIONS_FIELD = "sections";

		public override IFeat Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);

			return
				new Feat(
					GetName(jObject),
					GetFeatType(jObject),
					GetPrerequisites(jObject),
					GetDescription(jObject),
					GetBenefit(jObject),
					GetSpecial(jObject));
		}

		private static string GetName(JToken jToken)
		{
			return (string) jToken[NAME_FIELD];
		}
		private static FeatType GetFeatType(JToken jToken)
		{
			var featTypes = jToken["feat_types"];
			var featType = (string) featTypes["feat_type"];

			FeatType type;
			return Enum.TryParse(featType, out type) ? type : FeatType.General;
		}
		private static IEnumerable<string> GetPrerequisites(JToken jToken)
		{
			var prerequisites = 
				jToken[SECTIONS_FIELD]
					.FirstOrDefault(
						x => SectionFieldEquals((string) x["name"], "Prerequisites"));
			if (prerequisites == null)
			{
				return null;
			}

			var value = (string) prerequisites["description"];
			return 
				value
					.Split(',')
					.Select(x => x.Trim())
					.Select(x => x.EndsWith(".") ? x.Substring(0, x.Length-1) : x);
		}
		private static string GetDescription(JToken jToken)
		{
			return (string) jToken["description"];
		}
		private static string GetBenefit(JToken jToken)
		{
			var benefits = jToken[SECTIONS_FIELD]
				.FirstOrDefault(
					x => SectionFieldEquals((string) x["name"], "Benefits"));
			if (benefits == null)
			{
				return null;
			}

			var value = (string) benefits["body"];

			var htmlTable = new HtmlDocument();
			htmlTable.Load(new StringReader(value));

			return htmlTable.DocumentNode.InnerText;
		}
		private static string GetSpecial(JToken jToken)
		{
			var special = 
				jToken[SECTIONS_FIELD]
					.FirstOrDefault(
						x => SectionFieldEquals((string) x["name"], "Special"));
			if (special == null)
			{
				return null;
			}

			var value = (string) special["body"];

			var htmlTable = new HtmlDocument();
			htmlTable.Load(new StringReader(value));

			return htmlTable.DocumentNode.InnerText;
		}

		private static bool SectionFieldEquals(string pField, string pValue)
		{
			return pField != null && (pField).Equals(pValue);
		}

		public override string Serialize(IFeat pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
