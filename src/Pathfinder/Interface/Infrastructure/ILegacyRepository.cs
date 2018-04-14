using System;
using System.Collections.Generic;

namespace Pathfinder.Interface.Infrastructure
{
    [Obsolete("This was pretty dumb interface...")]
    public interface ILegacyRepository<T> : IRepository<T>, IEnumerable<T>
    {
        IEnumerable<string> Keys { get; }

        IEnumerable<T> Values { get; }

        T this[string pKey] { get; }

        bool TryGetValue(string pKey, out T pValue);

        void Save(T pValue, int pVersion);
    }
}
