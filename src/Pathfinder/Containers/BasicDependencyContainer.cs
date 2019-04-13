using System;
using System.Collections.Generic;

namespace Pathfinder.Containers
{
    public class BasicDependencyContainer : IDependencyContainer
    {
        public BasicDependencyContainer()
        {
            Registry = new Dictionary<Type, object>();
        }

        private Dictionary<Type, object> Registry { get; }

        public IDependencyContainer Register<TInterface, TClass>() where TClass : class, TInterface where TInterface : class
        {
            RegisterInstance<TInterface, TClass>(Activator.CreateInstance<TClass>());
            
            return this;
        }

        public IDependencyContainer RegisterInstance<TInterface, TClass>(TClass pSingleton) where TClass : class, TInterface where TInterface : class
        {
            var typeKey = typeof(TInterface);
            if (Registry.ContainsKey(typeKey))
            {
                throw new Exception($"An implementation has already be registered for the type \"{typeKey.FullName}\"");
            }
            Registry[typeKey] = pSingleton;

            return this;
        }

        public T Get<T>() where T : class
        {
            if (!Registry.TryGetValue(typeof(T), out var outValue))
            {
                throw new KeyNotFoundException($"No singleton as be registered for the given key: \"{typeof(T).FullName}\"");
            }
            return (T) outValue;
        }
    }
}
