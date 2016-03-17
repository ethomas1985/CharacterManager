using System;
using System.Collections.Generic;
using System.IO;
using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class RaceLibrary : ILibrary<IRace>
	{
		private readonly Lazy<IDictionary<string, IRace>> _library =
			new Lazy<IDictionary<string, IRace>>(
				() => new Dictionary<string, IRace>());

		internal RaceLibrary(ISerializer<IRace, string> pSerializer, string pRaceLibraryDirectory)
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

		private void LoadFile(ISerializer<IRace, string> pSerializer, string pFile)
		{
			var xml = File.ReadAllText(pFile);
			var race = pSerializer.Deserialize(xml);

			IRace outRace;
			if (Library.TryGetValue(race.Name, out outRace))
			{
				Library[race.Name] = race;
			}
			else
			{
				Library.Add(race.Name, race);
			}
		}

		private IDictionary<string, IRace> Library => _library.Value;

		public IRace this[string pKey]
		{
			get
			{
				IRace value;
				if (Library.TryGetValue(pKey, out value))
				{
					return value;
				}
				throw new KeyNotFoundException($"Key := \"{pKey}\"");
			}
		}
	}
}
