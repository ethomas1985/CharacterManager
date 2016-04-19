using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal class FeatureLibrary : ILibrary<IFeature>
	{
		private readonly Lazy<IDictionary<string, IFeature>> _library =
			new Lazy<IDictionary<string, IFeature>>(
				() => new Dictionary<string, IFeature>());

		public FeatureLibrary(ISerializer<IFeature, string> pSerializer, string pFeatureLibraryDirectory)
		{
			if (!Directory.Exists(pFeatureLibraryDirectory))
			{
				throw new DirectoryNotFoundException();
			}

			var files = Directory.EnumerateFiles(pFeatureLibraryDirectory, "*.xml");
			foreach (var file in files)
			{
				LoadFile(pSerializer, file);
			}
		}

		private IDictionary<string, IFeature> Library => _library.Value;

		private void LoadFile(ISerializer<IFeature, string> pSerializer, string pFile)
		{
			var xml = File.ReadAllText(pFile);
			var feature = pSerializer.Deserialize(xml);

			IFeature outFeature;
			if (Library.TryGetValue(feature.Name, out outFeature))
			{
				Library[feature.Name] = feature;
			}
			else
			{
				Library.Add(feature.Name, feature);
			}
		}

		public IFeature this[string pKey]
		{
			get
			{
				IFeature value;
				if (Library.TryGetValue(pKey, out value))
				{
					return value;
				}
				throw new KeyNotFoundException($"Key := \"{pKey}\"");
			}
		}

		public IEnumerator<IFeature> GetEnumerator()
		{
			return Library.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
