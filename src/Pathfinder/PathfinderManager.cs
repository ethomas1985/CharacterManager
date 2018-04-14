using System;
using System.Collections.Generic;

namespace Pathfinder
{
    public class PathfinderManager
    {
        internal PathfinderManager()
        {
            Registry = new Dictionary<Type, object>();
        }

        private Dictionary<Type, object> Registry { get; }

        public void Register<T>() where T : class, new()
        {
            Register(Activator.CreateInstance<T>());
        }

        public void Register<T>(T pSingleton) where T : class
        {
            var typeKey = typeof(T);
            if (Registry.ContainsKey(typeKey))
            {
                throw new Exception($"An implmentaion has already be registered for the type \"{typeKey.FullName}\"");
            }
            Registry[typeKey] = pSingleton;
        }

        public T Get<T>() where T : class
        {
            if (!Registry.TryGetValue(typeof(T), out var outValue))
            {
                throw new KeyNotFoundException($"No singlton as be registered for the given key: \"{typeof(T).FullName}\"");
            }
            return outValue as T;
        }
    }
}
