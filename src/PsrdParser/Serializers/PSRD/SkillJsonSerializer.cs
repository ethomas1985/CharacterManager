using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace PsrdParser.Serializers.PSRD
{
	public class SkillJsonSerializer : JsonSerializer<ISkill, string>
	{
		public override ISkill Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);

			return new Skill(
				GetString(jObject, "name"),
				GetAbilityType(jObject),
				GetBoolean(jObject, "trained_only"),
				GetBoolean(jObject, "armor_check_penalty"),
				GetString(jObject, "description"),
				GetCheckString(jObject, "name", "Check"),
				GetStringFor(jObject, "name", "Action"),
				GetStringFor(jObject, "name", "Try Again"),
				GetStringFor(jObject, "name", "Special"),
				GetStringFor(jObject, "name", "Restriction"),
				GetStringFor(jObject, "name", "Untrained"));
		}

		private static string GetCheckString(JObject jObject, string pField, string pValue)
		{
			var section = jObject["sections"].Children().Where(x => x[pField] != null && ((string) x[pField]).Equals(pValue));
			var mainBody =
				section
					.Select(
						x => new
						{
							Header = (string) x["name"],
							Body = (string) x["body"]
						})
					.Select(x => $"<h1>{x.Header}</h1>{x.Body}");
			var sections = section.Select(x => x["sections"]).FirstOrDefault();
			if (sections != null)
			{
				var bodies =
				sections
					.Select(
						x => new
						{
							Header = (string) x["name"],
							Body = (string) x["body"]
						})
					.Select(x => $"<h1>{x.Header}</h1>{x.Body}");
				return string.Concat(mainBody.Union(bodies));
			}
			return string.Concat(mainBody);
		}

		private AbilityType GetAbilityType(JObject jObject)
		{
			var abilityType = GetString(jObject, "attribute");
			switch (abilityType.ToLower())
			{
				case "str":
					return AbilityType.Strength;
				case "dex":
					return AbilityType.Dexterity;
				case "con":
					return AbilityType.Constitution;
				case "int":
					return AbilityType.Intelligence;
				case "wis":
					return AbilityType.Wisdom;
				case "cha":
					return AbilityType.Charisma;
			}

			AbilityType value;
			if (AbilityType.TryParse(abilityType, out value))
			{
				return value;
			}
			throw new JsonException($"Invalid value for AbilityType; was {abilityType}");
		}

		public override string Serialize(ISkill pObject)
		{
			throw new NotImplementedException();
		}
	}
}
