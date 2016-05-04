using System;
using System.Collections;
using System.Collections.Generic;
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

			LibraryDirectory = pLibraryDirectory;

			Initialize(pSerializer);
		}

		private void Initialize(ISerializer<T, string> pSerializer)
		{
			var files = Directory.EnumerateFiles(LibraryDirectory, "*.xml");
			foreach (var file in files)
			{
				LoadFile(pSerializer, file);
			}
		}

		private string LibraryDirectory { get; }
		internal IDictionary<string, T> Library => _library.Value;

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