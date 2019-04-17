using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Items;

namespace Pathfinder.Repository
{
    internal interface IItemPersistenceStore : IRepository<IItem>
    {

    }

    public class ItemMongoRepository : IItemPersistenceStore
    {
        private readonly MongoRepository<Item> _store;

        public ItemMongoRepository(IMongoSettings pSettings)
        {
            _store = new MongoRepository<Item>(pSettings, "items");
        }

        private IMongoCollection<Item> GetCollection()
        {
            return _store.GetCollection();
        }

        public IQueryable<IItem> GetQueryable()
        {
            return GetCollection().AsQueryable();
        }

        private static Item ConvertToConcrete(IItem pValue)
        {
            return pValue as Item
                ?? Item.Copy(pValue);
        }

        public void Insert(IItem pValue)
        {
            GetCollection().InsertOne(ConvertToConcrete(pValue));
        }

        public void Insert(IEnumerable<IItem> pValues)
        {
            GetCollection().InsertMany(pValues.Select(ConvertToConcrete));
        }

        public void Update(IItem pValue)
        {
            var filter = new FilterDefinitionBuilder<Item>()
                .Eq(nameof(Item.Name), pValue.Name);

            GetCollection().ReplaceOne(filter, ConvertToConcrete(pValue));
        }

        public void Replace()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IItem> GetAll()
        {
            return GetCollection()
                .Find(new BsonDocument())
                .ToList();
        }

        public IItem Get(string pId)
        {
            var filter = new FilterDefinitionBuilder<Item>().Eq(nameof(Item.Name), pId);
            return GetCollection()
                .Find(filter)
                .Limit(1)
                .FirstOrDefault();
        }
    }
}
