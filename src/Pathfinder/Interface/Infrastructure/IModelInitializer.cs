using MongoDB.Bson.Serialization;

namespace Pathfinder.Interface.Infrastructure
{
    internal interface IModelInitializer<T> where T : class
    {
        BsonClassMap<T> Register();

        void Initializer(BsonClassMap<T> pClassMap);
    }
}
