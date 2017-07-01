using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class ClassJsonSerializer : AbstractJsonSerializer<IClass>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IClass pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IClass.Name), pValue.Name);

			WriteArrayProperty(pWriter, pSerializer, nameof(IClass.Alignments), pValue.Alignments.Select(x => x.ToString()));

			WriteProperty(pWriter, pSerializer, nameof(IClass.HitDie), pValue.HitDie);

			WriteProperty(pWriter, pSerializer, nameof(IClass.SkillAddend), pValue.SkillAddend);

			WriteArrayProperty(pWriter, pSerializer, nameof(IClass.Skills), pValue.Skills);

			WriteArrayProperty(pWriter, pSerializer, nameof(IClass.ClassLevels), pValue.ClassLevels);

			WriteArrayProperty(pWriter, pSerializer, nameof(IClass.Features), pValue.Features);

			pWriter.WriteEndObject();
		}

		protected override IClass DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var nameToken = GetString(pJobject, nameof(IClass.Name));
			if (string.IsNullOrEmpty(nameToken))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IClass.Name)}");
			}

			var hitdieString = GetString(pJobject, nameof(IClass.HitDie));
			if (string.IsNullOrWhiteSpace(hitdieString))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(IClass.HitDie)}");
			}

			var match = new Regex(@"d(\d+)").Match(hitdieString);
			if (!match.Success)
			{
				throw new JsonException($"Invalid Formatting: [{nameof(IClass.HitDie)}] {hitdieString}");
			}
			var hitDie = new Die(match.Groups[1].Value.AsInt());

			var alignments =
				new HashSet<Alignment>(
					GetValuesFromArray<Alignment>(pSerializer, pJobject, nameof(IClass.Alignments)));

			var skillAddend = GetInt(pJobject, nameof(IClass.SkillAddend));

			var skills = 
				new HashSet<string>(
					GetValuesFromArray<string>(pSerializer, pJobject, nameof(IClass.Skills)));

			var classLevels = GetValuesFromArray<IClassLevel>(pSerializer, pJobject, nameof(IClass.ClassLevels));

			var features = GetValuesFromArray<string>(pSerializer, pJobject, nameof(IClass.Features));

			return new Class(
				nameToken,
				alignments,
				hitDie,
				skillAddend,
				skills,
				classLevels,
				features);
		}
	}
}
