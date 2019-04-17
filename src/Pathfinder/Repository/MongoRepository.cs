using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Pathfinder.Utilities;

namespace Pathfinder.Repository
{
    internal class MongoRepository<TModel> where TModel: class
    {
        private IMongoSettings Settings { get; }
        protected string CollectionName { get; }

        public MongoRepository(IMongoSettings pSettings, string pCollectionName)
        {
            Settings = pSettings;
            CollectionName = pCollectionName;
        }

        [Obsolete("Use IMongoSettings")]
        public MongoRepository(string pMongoUrl, string pDatabaseName, string pCollectionName)
            : this(new MongoSettings(new Uri(pMongoUrl), pDatabaseName), pCollectionName)
        { }

        public MongoClient Create()
        {
            if (Features.LogQueries)
            {
                return CreateWithLogger();
            }
            return new MongoClient(Uri);
        }

        public MongoClient CreateWithLogger()
        {
            var uri = new Uri(Uri);
            return new MongoClient(new MongoClientSettings()
            {
                Server = new MongoServerAddress(uri.Host, uri.Port),
                ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(LogCommandStartedEvent);
                }
            });
        }

        private static HashSet<string> ExcludedEvents => new HashSet<string> { "isMaster", "buildInfo", "getLastError" };

        protected string Uri => Settings.Server.ToString();

        public string DatabaseName => Settings.Database;

        private void LogCommandStartedEvent(CommandStartedEvent pEvent)
        {
            if (ExcludedEvents.Contains(pEvent.CommandName))
            {
                return;
            }
            LogTo.Debug($"{nameof(MongoRepository<TModel>)}|{typeof(TModel).Name}|{pEvent.CommandName} - {pEvent.Command.ToJson()}");
        }


        public IMongoDatabase GetDatabase()
        {
            return Create().GetDatabase(DatabaseName);
        }

        public IMongoCollection<TModel> GetCollection()
        {
            return GetDatabase().GetCollection<TModel>(CollectionName);
        }
    }
}
