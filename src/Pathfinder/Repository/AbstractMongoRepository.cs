using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Pathfinder.Utilities;

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
			if (Features.LogQueries)
			{
				return CreateWithLogger();
			}
			return new MongoClient(_url);
		}

		public MongoClient CreateWithLogger()
		{
			var uri = new Uri(_url);
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
		private void LogCommandStartedEvent(CommandStartedEvent pEvent)
		{
			if (ExcludedEvents.Contains(pEvent.CommandName))
			{
				return;
			}
			LogTo.Debug($"{nameof(MongoRepository<TInterface>)}|{typeof(TInterface).Name}|{pEvent.CommandName} - {pEvent.Command.ToJson()}");
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
