using System;
using Pathfinder.Interface;
using System.Collections.Generic;
using System.IO;

namespace Pathfinder.Library
{
	internal class TraitLibrary : ILibrary<ITrait>
	{
		private readonly Lazy<IDictionary<string, ITrait>> _library = 
			new Lazy<IDictionary<string, ITrait>>(
				() => new Dictionary<string, ITrait>());

		public TraitLibrary(ISerializer<ITrait, string> pSerializer, string pTraitLibraryDirectory)
		{
			if (!Directory.Exists(pTraitLibraryDirectory))
			{
				throw new DirectoryNotFoundException();
			}

			var files = Directory.EnumerateFiles(pTraitLibraryDirectory, "*.xml");
			foreach (var file in files)
			{
				LoadFile(pSerializer, file);
			}
		}

		private void LoadFile(ISerializer<ITrait, string> pSerializer, string pFile)
		{
			var xml = File.ReadAllText(pFile);
			var trait = pSerializer.Deserialize(xml);

			ITrait value;
			if (Library.TryGetValue(trait.Name, out value))
			{
				Library[trait.Name] = trait;
			}
			else
			{
				Library.Add(trait.Name, trait);
			}
		}

		internal IDictionary<string, ITrait> Library => _library.Value;

		public ITrait this[string pKey]
		{
			get
			{
				ITrait value;
				if (Library.TryGetValue(pKey, out value))
				{
					return value;
				}
				throw new KeyNotFoundException($"Key := \"{pKey}\"");
			}
		}
	}
}
