using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Library
{
	internal abstract class AbstractRepository<T> : IRepository<T> where T : INamed
	{
		private readonly Lazy<ConcurrentDictionary<string, T>> _library =
			new Lazy<ConcurrentDictionary<string, T>>(
				() => new ConcurrentDictionary<string, T>());

		internal AbstractRepository(ISerializer<T, string> pSerializer, string pLibraryDirectory)
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

		public virtual void Save(T pValue, int pVersion)
		{
			var serialized = Serializer.Serialize(pValue);

			var newPath = Path.Combine(LibraryDirectory, pValue.Name.Replace(" ", "_"));
			newPath = Path.ChangeExtension(newPath, "xml");
			File.WriteAllText(newPath, serialized);

			if (!typeof(IAggregate).IsAssignableFrom(typeof(T)))
			{
				return;
			}

			var asAggregate = (pValue as IAggregate);
			var history = JsonConvert.SerializeObject(asAggregate.GetPendingEvents());

			newPath = Path.Combine(newPath, "xml", pValue.Name.Replace(" ", "_"), "History", DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));
			newPath = Path.ChangeExtension(newPath, "json");
			File.WriteAllText(newPath, history);
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