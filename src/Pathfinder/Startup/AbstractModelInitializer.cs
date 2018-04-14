using MongoDB.Bson.Serialization;
using Pathfinder.Interface.Infrastructure;

namespace Pathfinder.Startup
{
    public abstract class AbstractModelInitializer<T> : IModelInitializer<T>
        where T : class
    {
        public BsonClassMap<T> Register()
        {
            return BsonClassMap.RegisterClassMap<T>(Initializer);
                //.AddKnownType(typeof(TClass));
        }

        public abstract void Initializer(BsonClassMap<T> pClassMap);
    }
}
