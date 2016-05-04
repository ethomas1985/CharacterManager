using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace PsrdParser.Serializers.PSRD
{
	public class ClassJsonSerializer : JsonSerializer<IClass, string>
	{
		private const string NAME_FIELD = "name";
		private const string SECTIONS_FIELD = "sections";

		public override IClass Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);

			return new Class(
				GetString(jObject, NAME_FIELD),
				GetAlignments(jObject, "alignment"),
				GetHitDie(jObject, "hit_die"),
				GetSkills(jObject, NAME_FIELD, "Class Skills"),
				GetClassLevels(jObject),
				GetClassFeatures(jObject)
				);
		}

		private static ISet<Alignment> GetAlignments(JObject jObject, string pField)
		{
			var value = GetString(jObject, pField);

			switch (value)
			{
				case "Lawful good.":
					return new HashSet<Alignment> { Alignment.LawfulGood };
				case "Any lawful.":
					return new HashSet<Alignment>
						   {
							   Alignment.LawfulGood,
							   Alignment.LawfulNeutral,
							   Alignment.LawfulEvil
						   };
				case "Any nonlawful.":
					return new HashSet<Alignment>
						   {
							   Alignment.NeutralGood,
							   Alignment.ChaoticGood,
							   Alignment.Neutral,
							   Alignment.ChaoticNeutral,
							   Alignment.NeutralEvil,
							   Alignment.ChaoticEvil
						   };
				case "Any neutral.":
					return new HashSet<Alignment>
						{
							Alignment.NeutralGood,
							Alignment.LawfulNeutral,
							Alignment.Neutral,
							Alignment.ChaoticNeutral,
							Alignment.NeutralEvil
						};
				case "Any.":
				case "All.":
					return new HashSet<Alignment>
						{
							Alignment.LawfulGood,
							Alignment.NeutralGood,
							Alignment.ChaoticGood,
							Alignment.LawfulNeutral,
							Alignment.Neutral,
							Alignment.ChaoticNeutral,
							Alignment.LawfulEvil,
							Alignment.NeutralEvil,
							Alignment.ChaoticEvil
						};
				default:
					throw new Exception($"Unknown Alignment Set: {value}");
			}
		}

		private static IDie GetHitDie(JObject jObject, string pField)
		{
			var regex = new Regex(@"d(\d+)");

			var value = GetString(jObject, pField);

			var match = regex.Match(value);
			Assert.IsTrue(match.Success, $"{value} does not match the Regex Pattern.");
			Assert.IsTrue(match.Groups[1].Success, $"Group 1 was not a success.");

			int faces;
			var tryParsed = int.TryParse(match.Groups[1].Value, out faces);
			Assert.IsTrue(tryParsed, $"int.TryParse failed.");
			Assert.IsTrue(faces > 0, $"faces must be greater than 0");

			return new Die(faces);
		}

		private ISet<string> GetSkills(JObject jObject, string pField, string pValue)
		{
			var skills = new HashSet<string>();
			var lineRegex = new Regex(@"(?:<p>)?The \w+'s class skills are (.*).?(?:</p>)?");
			var value = GetStringFor(jObject, pField, pValue);

			var lineMatch = lineRegex.Match(value);
			Assert.IsTrue(lineMatch.Success, $"{value} does not match the Line Regex Pattern.");
			Assert.IsTrue(lineMatch.Groups[1].Success, $"Line Group 1 was not a success.");

			var line = lineMatch.Groups[1].Value;
			var split = line.Split(',').Select(x => x.Replace(" and ", "").Trim());
			var itemRegex = new Regex(@"([\w ]+(?: \([\w ]+\))?) \((?:Str|Dex|Con|Int|Wis|Cha)\)");
			foreach (var token in split)
			{
				var itemMatch = itemRegex.Match(token);
				Assert.IsTrue(itemMatch.Success, $"{value} does not match the Item Regex Pattern.");

				skills.Add(itemMatch.Groups[1].Value);
			}

			return skills;
		}

		private IEnumerable<IClassLevel> GetClassLevels(JObject jObject)
		{
			var tableSection = GetTableSectionBody(jObject);

			var htmlTable = new HtmlDocument();
			htmlTable.Load(new StringReader((string) tableSection["body"]));

			var nodes = htmlTable.DocumentNode.SelectNodes("//table/tr");

			return nodes.Select(
				rowNode => rowNode.Elements("td")
					.Select(td => td.InnerText.Trim()).ToArray())
					.Select(cells =>
						new ClassLevel(
							AsInt(cells[0]),
							AsEnumerableOfInts(cells[1]),
							AsInt(cells[2]),
							AsInt(cells[3]),
							AsInt(cells[4]),
							GetSpecials(cells[5]),
							GetSpellsPerDay(cells.Skip(6).ToArray())))
					.Cast<IClassLevel>()
					.ToList();
		}

		private IDictionary<int, int> GetSpellsPerDay(IEnumerable<string> pValues)
		{
			return
				pValues
					.Select((x, i) => new { Index = i, Count = AsIntWithDefault(x) })
					.ToDictionary(k => k.Index, v => v.Count);
		}

		private static IEnumerable<string> GetSpecials(string pValue)
		{
			var textInfo = new CultureInfo("en-US", false).TextInfo;
			return
				pValue
					.Split(',')
					.Select(
						x => textInfo.ToTitleCase(x.Trim()))
					.ToList();
		}

		private static int AsInt(string pValue)
		{
			var stripped = Regex.Replace(pValue, @"(\d+)(?:st|nd|rd|th)", "$1");
			return int.Parse(stripped);
		}

		private static int AsIntWithDefault(string pValue)
		{
			int value;
			return int.TryParse(pValue, out value) ? value : 0;
		}

		private static IEnumerable<int> AsEnumerableOfInts(string pValue)
		{
			return pValue.Split('/').Select(AsInt).ToList();
		}

		[NotNull]
		private static JToken GetTableSectionBody(JObject jObject)
		{
			var classSkills = GetClassSkillsSection(jObject);

			Assert.IsTrue(classSkills != null, $"{classSkills} != null");
			var skillsPerRankSection =
				classSkills[SECTIONS_FIELD]
					.Children()
					.FirstOrDefault(x => x[NAME_FIELD] != null && ((string) x[NAME_FIELD]).Equals("Skill Ranks per Level"));

			Assert.IsTrue(skillsPerRankSection != null, $"{skillsPerRankSection} != null");
			var tableSection =
				skillsPerRankSection[SECTIONS_FIELD]
					.Children()
					.FirstOrDefault(x => x[NAME_FIELD] != null && ((string) x[NAME_FIELD]).StartsWith("Table: "));
			return tableSection;
		}

		[NotNull]
		private static JToken GetClassSkillsSection(JObject jObject)
		{
			var classSkills =
				jObject[SECTIONS_FIELD]
					.Children()
					.FirstOrDefault(x => x[NAME_FIELD] != null && ((string) x[NAME_FIELD]).Equals("Class Skills"));
			return classSkills;
		}

		private IEnumerable<string> GetClassFeatures(JObject jObject)
		{
			var featuresSection =
				jObject[SECTIONS_FIELD]
					.Children()
					.FirstOrDefault(x => x[NAME_FIELD] != null && ((string) x[NAME_FIELD]).Equals("Class Features"));

			return featuresSection[SECTIONS_FIELD]
					.Children()
					.Select(x => (string) x["name"])
					.ToList();
		}

		public override string Serialize(IClass pObject)
		{
			throw new NotImplementedException();
		}
	}
}
