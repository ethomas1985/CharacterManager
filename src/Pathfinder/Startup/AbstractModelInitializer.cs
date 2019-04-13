using MongoDB.Bson.Serialization;
using Pathfinder.Interface.Infrastructure;

namespace Pathfinder.Startup
{
    internal interface IModelInitializer<T> : IModelRegister where T : class {
        void Initializer(BsonClassMap<T> pClassMap);
    }

    public abstract class AbstractModelInitializer<T> : IModelInitializer<T> where T : class
    {
        public void Register()
        {
            BsonClassMap.RegisterClassMap<T>(Initializer);
        }

        public abstract void Initializer(BsonClassMap<T> pClassMap);
    }
}
