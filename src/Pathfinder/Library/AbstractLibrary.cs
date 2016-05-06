using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Pathfinder.Interface;

namespace Pathfinder.Library
{
	internal abstract class AbstractLibrary<T> : ILibrary<T> where T : INamed
	{
		private readonly Lazy<IDictionary<string, T>> _library =
			new Lazy<IDictionary<string, T>>(
				() => new Dictionary<string, T>());

		internal AbstractLibrary(ISerializer<T, string> pSerializer, string pLibraryDirectory)
		{
			if (!Directory.Exists(pLibraryDirectory))
			{
				throw new DirectoryNotFoundException();
			}
			Serializer = pSerializer;
			LibraryDirectory = pLibraryDirectory;

			Initialize();
		}

		private void Initialize()
		{
			var files = Directory.EnumerateFiles(LibraryDirectory, "*.xml");
			foreach (var file in files)
			{
				LoadFile(Serializer, file);
			}
		}

		private ISerializer<T, string> Serializer { get; }
		private string LibraryDirectory { get; }
		internal IDictionary<string, T> Library => _library.Value;

		public IEnumerable<string> Keys => Library.Keys.ToImmutableList();
		public IEnumerable<T> Values => Library.Values.ToImmutableList();

		public virtual T this[string pKey]
		{
			get
			{
				T value;
				if (Library.TryGetValue(pKey, out value))
				{
					return value;
				}
				throw new KeyNotFoundException($" Invalid {typeof(T).Name} Key := \"{pKey}\"");
			}
		}

		public void Store(T pValue)
		{
			var serialized = Serializer.Serialize(pValue);

			var newPath = Path.Combine(LibraryDirectory, pValue.Name.Replace(" ", "_"));
			newPath = Path.ChangeExtension(newPath, "xml");
			File.WriteAllText(newPath, serialized);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Library.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		protected virtual void LoadFile(ISerializer<T, string> pSerializer, string pFile)
		{
			var xml = File.ReadAllText(pFile);
			var deserialize = pSerializer.Deserialize(xml);

			T outRace;
			if (Library.TryGetValue(deserialize.Name, out outRace))
			{
				Library[deserialize.Name] = deserialize;
			}
			else
			{
				Library.Add(deserialize.Name, deserialize);
			}
		}
	}
}