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
				getString(jObject, "name"),
				_GetAbilityType(jObject),
				getBoolean(jObject, "trained_only"),
				getBoolean(jObject, "armor_check_penalty"),
				getString(jObject, "description"),
				_GetCheckString(jObject, "name", "Check"),
				getStringFor(jObject, "name", "Action"),
				getStringFor(jObject, "name", "Try Again"),
				getStringFor(jObject, "name", "Special"),
				getStringFor(jObject, "name", "Restriction"),
				getStringFor(jObject, "name", "Untrained"));
		}

		private static string _GetCheckString(JObject pJObject, string pField, string pValue)
		{
			var section = pJObject["sections"].Children().Where(x => x[pField] != null && ((string) x[pField]).Equals(pValue));
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

		private AbilityType _GetAbilityType(JObject pJObject)
		{
			var abilityType = getString(pJObject, "attribute");
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
