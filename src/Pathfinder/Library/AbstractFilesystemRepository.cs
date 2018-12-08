using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
    internal abstract class AbstractFilesystemRepository<T> : ILegacyRepository<T> where T : INamed
    {
        protected internal const string XML = "*.xml";

        private readonly Lazy<ConcurrentDictionary<string, T>> _library =
            new Lazy<ConcurrentDictionary<string, T>>(
                () => new ConcurrentDictionary<string, T>());

        internal AbstractFilesystemRepository(ISerializer<T, string> pSerializer, string pLibraryDirectory,
            string pFileType = XML)
        {
            //Tracer.Message($"{GetType().FullName}: Path := \"{Path.GetFullPath(pLibraryDirectory)}\"");
            if (!Directory.Exists(pLibraryDirectory))
            {
                Directory.CreateDirectory(pLibraryDirectory);
            }

            Serializer = pSerializer;
            LibraryDirectory = pLibraryDirectory;
            FileType = pFileType;

            Initialize();
        }

        private void Initialize()
        {
            var files = Directory.EnumerateFiles(LibraryDirectory, FileType).ToList();
            LogTo.Debug($"{GetType().Name}|{typeof(T).Name}|{nameof(Initialize)}|{nameof(LibraryDirectory)}|{LibraryDirectory}");
            LogTo.Debug($"{GetType().Name}|{typeof(T).Name}|{nameof(Initialize)}|# of {nameof(files)}|{files.Count()}");

            Parallel.ForEach(files, x =>
            {
                LogTo.Debug($"{GetType().Name}|{typeof(T).Name}|{nameof(Initialize)}|{nameof(x)}|{x}");
                try
                {
                    LoadFile(Serializer, x);
                }
                catch (Exception e)
                {
                    LogTo.Exception(e);
                    throw new Exception($"Exception deserializing file \"{x}\"", e);
                }
            });
        }

        protected ISerializer<T, string> Serializer { get; }
        protected string LibraryDirectory { get; }
        protected string FileType { get; }
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

            newPath = Path.Combine(newPath, "xml", pValue.Name.Replace(" ", "_"), "History",
                                   DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));
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

        public IQueryable<T> GetQueryable()
        {
            throw new NotImplementedException();
        }

        public void Insert(T pValue)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<T> pValues)
        {
            throw new NotImplementedException();
        }

        public void Update(T pValue)
        {
            throw new NotImplementedException();
        }

        public void Replace()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return Values;
        }

        public T Get(string pId)
        {
            return Values.FirstOrDefault(x => string.Equals(x.Name, pId, StringComparison.InvariantCultureIgnoreCase));
        }

        //public IEnumerable<T> GetList(Expression<Func<T, bool>> pPredicate)
        //{
        //    return Values.Where(pPredicate.Compile()).ToList();
        //}
    }
}
