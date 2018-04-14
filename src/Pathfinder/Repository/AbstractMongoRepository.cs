using MongoDB.Driver;

namespace Pathfinder.Repository
{
    internal class MongoRepository<TInterface>
    {
        private readonly string _url;
        private readonly string _databaseName;
        private readonly string _collectionName;

        public MongoRepository(string pMongoUrl, string pDatabaseName, string pCollectionName)
        {
            _url = pMongoUrl;
            _databaseName = pDatabaseName;
            _collectionName = pCollectionName;
        }

        public MongoClient Create()
        {
            return new MongoClient(_url);
        }

        public IMongoDatabase GetDatabase()
        {
            return Create().GetDatabase(_databaseName);
        }

        public IMongoCollection<TInterface> GetCollection()
        {
            return GetDatabase().GetCollection<TInterface>(_collectionName);
        }
    }
}
