using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Library
{
	internal class SkillLibrary : ILibrary<ISkill>
	{

		private readonly Lazy<IDictionary<string, ISkill>> _library =
			new Lazy<IDictionary<string, ISkill>>(
				() => new Dictionary<string, ISkill>());

		internal SkillLibrary(ISerializer<ISkill, string> pSerializer, string pRaceLibraryDirectory)
		{
			if (!Directory.Exists(pRaceLibraryDirectory))
			{
				throw new DirectoryNotFoundException();
			}

			var files = Directory.EnumerateFiles(pRaceLibraryDirectory, "*.xml");
			foreach (var file in files)
			{
				LoadFile(pSerializer, file);
			}
		}

		private void LoadFile(ISerializer<ISkill, string> pSerializer, string pFile)
		{
			var xml = File.ReadAllText(pFile);
			var race = pSerializer.Deserialize(xml);

			ISkill value;
			if (Library.TryGetValue(race.Name, out value))
			{
				Library[race.Name] = race;
			}
			else
			{
				Library.Add(race.Name, race);
			}
		}

		private IDictionary<string, ISkill> Library => _library.Value;

		public ISkill this[string pKey]
		{
			get
			{
				ISkill value;
				if (Library.TryGetValue(pKey, out value))
				{
					return value;
				}

				// Special Logic for Craft, Knowledge, Profession, and Perform skills.
				var regex = new Regex(@"([\w ]+) \(([\w ]+)\)");
				var match = regex.Match(pKey);
				if (match.Success)
				{
					var baseName = match.Groups[1].Value;
					var specialization = match.Groups[2].Value;
					if (Library.TryGetValue(baseName, out value))
					{
						return CreateSpecialization(value, specialization);
					}
				}

				throw new KeyNotFoundException($"Key := \"{pKey}\"");
			}
		}

		private Skill CreateSpecialization(ISkill value, string specialization)
		{
			var skill = 
				new Skill(
					$"{value.Name} ({specialization})",
					value.AbilityType,
					value.TrainedOnly,
					value.ArmorCheckPenalty,
					value.Description,
					value.Check,
					value.Action,
					value.TryAgain,
					value.Special,
					value.Restriction,
					value.Untrained);

			Library.Add(skill.Name, skill);

			return skill;
		}

		public IEnumerator<ISkill> GetEnumerator()
		{
			return Library.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
