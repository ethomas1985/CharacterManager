using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
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
					_GetName(jObject),
					_GetFeatType(jObject),
					_GetPrerequisites(jObject),
					_GetDescription(jObject),
					_GetBenefit(jObject),
					_GetSpecial(jObject));
		}

		private static string _GetName(JToken pJToken)
		{
			return (string) pJToken[NAME_FIELD];
		}
		private static FeatType _GetFeatType(JToken pJToken)
		{
			var featTypes = pJToken["feat_types"];
			var featType = (string) featTypes["feat_type"];

			FeatType type;
			return Enum.TryParse(featType, out type) ? type : FeatType.General;
		}
		private static IEnumerable<string> _GetPrerequisites(JToken pJToken)
		{
			var prerequisites = 
				pJToken[SECTIONS_FIELD]
					.FirstOrDefault(
						x => _SectionFieldEquals((string) x["name"], "Prerequisites"));
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
		private static string _GetDescription(JToken pJToken)
		{
			return (string) pJToken["description"];
		}
		private static string _GetBenefit(JToken pJToken)
		{
			var benefits = pJToken[SECTIONS_FIELD]
				.FirstOrDefault(
					x => _SectionFieldEquals((string) x["name"], "Benefits"));
			if (benefits == null)
			{
				return null;
			}

			var value = (string) benefits["body"];

			var htmlTable = new HtmlDocument();
			htmlTable.Load(new StringReader(value));

			return htmlTable.DocumentNode.InnerText;
		}
		private static string _GetSpecial(JToken pJToken)
		{
			var special = 
				pJToken[SECTIONS_FIELD]
					.FirstOrDefault(
						x => _SectionFieldEquals((string) x["name"], "Special"));
			if (special == null)
			{
				return null;
			}

			var value = (string) special["body"];

			var htmlTable = new HtmlDocument();
			htmlTable.Load(new StringReader(value));

			return htmlTable.DocumentNode.InnerText;
		}

		private static bool _SectionFieldEquals(string pField, string pValue)
		{
			return pField != null && (pField).Equals(pValue);
		}

		public override string Serialize(IFeat pObject)
		{
			throw new System.NotImplementedException();
		}
	}
}
