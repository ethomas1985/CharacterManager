using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace PsrdParser.Serializers.PSRD
{
	public class SpellJsonSerializer : JsonSerializer<ISpell, string>
	{
		private static readonly IDictionary<string, ComponentType> ComponentMapping =
			new Dictionary<string, ComponentType>
			{
				["V"] = ComponentType.Verbal,
				["S"] = ComponentType.Somatic,
				["M"] = ComponentType.Material,
				["F"] = ComponentType.Focus,
				["DF"] = ComponentType.DivineFocus,
			};

		public override ISpell Deserialize(string pValue)
		{
			Assert.ArgumentIsNotEmpty(pValue, nameof(pValue));

			var jObject = JObject.Parse(pValue);

			return
				new Spell(
					_GetName(jObject),
					_GetMagicSchool(jObject),
					_GetSubMagicSchool(jObject),
					_GetMagicDescriptors(jObject),
					_GetSavingThrow(jObject),
					_GetDescription(jObject),
					_HasSpellResistance(jObject),
					_GetSpellResistance(jObject),
					_GetCastingTime(jObject),
					_GetRange(jObject),
					_GetLevelRequirements(jObject),
					_GetDuration(jObject),
					_GetComponents(jObject));
		}

		private static string _GetName(JObject pJObject)
		{
			return getString(pJObject, "name");
		}

		private static MagicSchool _GetMagicSchool(JObject pJObject)
		{
			MagicSchool value;
			return Enum.TryParse((string) pJObject["school"], out value) ? value : MagicSchool.Universal;
		}

		private static MagicSubSchool _GetSubMagicSchool(JObject pJObject)
		{
			MagicSubSchool value;
			return Enum.TryParse((string) pJObject["school"], out value) ? value : MagicSubSchool.None;
		}

		private static ISet<MagicDescriptor> _GetMagicDescriptors(JObject pJObject)
		{
			var values = new HashSet<MagicDescriptor>();

			var jToken = pJObject["descriptor"];
			if (jToken == null)
			{
				return null;
			}

			var descriptors = jToken.Values().Select(x => (string) x).Where(x => x != null);

			foreach (var descriptor in descriptors)
			{
				MagicDescriptor value;
				if (Enum.TryParse(descriptor, true, out value))
				{
					values.Add(value);
				}
			}

			return values;
		}

		private static string _GetSavingThrow(JObject pJObject)
		{
			return getString(pJObject, "saving_throw");
		}

		private static string _GetDescription(JObject pJObject)
		{
			return getString(pJObject, "description");
		}

		private static bool _HasSpellResistance(JObject pJObject)
		{
			var str = (string) pJObject["spell_resistance"];
			str = str?.Split(' ').FirstOrDefault();

			bool value;
			if (bool.TryParse(str, out value))
			{
				return value;
			}

			return "yes".Equals(str, StringComparison.InvariantCultureIgnoreCase);
		}

		private static string _GetSpellResistance(JObject pJObject)
		{
			var str = (string) pJObject["spell_resistance"];
			var theRest = str?.Split(' ').Skip(1).ToArray();

			return theRest?.Length > 0 ? string.Join(" ", theRest) : null;
		}

		private static string _GetCastingTime(JObject pJObject)
		{
			return getString(pJObject, "casting_time");
		}

		private static string _GetRange(JObject pJObject)
		{
			return getString(pJObject, "range");
		}

		private static IDictionary<string, int> _GetLevelRequirements(JObject pJObject)
		{
			var values = new Dictionary<string, int>();

			var levels =
				pJObject["levels"].Children()
					.Select(
						x => new
						{
							ClassName = (string) x["class"],
							Level = (int) x["level"]
						});

			foreach (var classLevel in levels)
			{
				values[classLevel.ClassName] = classLevel.Level;
			}

			return values;
		}

		private static string _GetDuration(JObject pJObject)
		{
			return getString(pJObject, "duration");
		}

		private static ISet<Tuple<ComponentType, string>> _GetComponents(JObject pJObject)
		{
			var values = new HashSet<Tuple<ComponentType, string>>();

			var token = pJObject["components"];
			if (token == null)
			{
				return null;
			}
			var components =
				token.Children()
					.Select(
						x => new
						{
							Type = (string) x["type"],
							Text = _CapitalizeFirstCharacter((string) x["text"])
						})
					.Where(x => x.Type != null);

			foreach (var component in components)
			{
				ComponentType componentType;
				if (!ComponentMapping.TryGetValue(component.Type, out componentType))
				{
					if (!Enum.TryParse(component.Type, out componentType))
					{
						throw new InvalidCastException($"Invalid ComponentType: {component.Type}");
					}
				}

				values.Add(new Tuple<ComponentType, string>(componentType, component.Text));
			}

			return values;
		}

		private static string _CapitalizeFirstCharacter(string pValue)
		{
			if (string.IsNullOrWhiteSpace(pValue))
			{
				return string.Empty;
			}

			return char.ToUpper(pValue[0]) + pValue.Substring(1);
		}

		public override string Serialize(ISpell pObject)
		{
			throw new NotImplementedException();
		}
	}
}
