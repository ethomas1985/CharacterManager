using Pathfinder.Interface;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading.Tasks;
using Pathfinder.Utilities;

namespace Pathfinder.Library
{
	internal abstract class AbstractLibrary<T> : ILibrary<T> where T : INamed
	{
		private readonly Lazy<ConcurrentDictionary<string, T>> _library =
			new Lazy<ConcurrentDictionary<string, T>>(
				() => new ConcurrentDictionary<string, T>());

		internal AbstractLibrary(ISerializer<T, string> pSerializer, string pLibraryDirectory)
		{
			Tracer.Message($"{GetType().FullName}: Path := \"{Path.GetFullPath(pLibraryDirectory)}\"");
			if (!Directory.Exists(pLibraryDirectory))
			{
				Directory.CreateDirectory(pLibraryDirectory);
			}
			Serializer = pSerializer;
			LibraryDirectory = pLibraryDirectory;

			Initialize();
		}

		private void Initialize()
		{
			var files = Directory.EnumerateFiles(LibraryDirectory, "*.xml");
			Parallel.ForEach(files, x => LoadFile(Serializer, x));
		}

		protected ISerializer<T, string> Serializer { get; }
		protected string LibraryDirectory { get; }
		internal ConcurrentDictionary<string, T> Library => _library.Value;

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

		public bool TryGetValue(string pKey, out T pValue)
		{
			return Library.TryGetValue(pKey, out pValue);
		}

		public virtual void Store(T pValue)
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

			Library.TryAdd(deserialize.Name, deserialize);
		}
	}
}