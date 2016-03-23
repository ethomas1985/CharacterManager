using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pathfinder.Interface;

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
				throw new KeyNotFoundException($"Key := \"{pKey}\"");
			}
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
