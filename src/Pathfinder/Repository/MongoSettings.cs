using System;

namespace Pathfinder.Repository
{
    public interface IMongoSettings {
        Uri Server { get; }
        string Database { get; }
    }

    internal class MongoSettings : IMongoSettings
    {
        public MongoSettings(Uri pServer, string pDatabase)
        {
            Server = pServer;
            Database = pDatabase;
        }
        public Uri Server { get; }
        public string Database { get;}
    }
}
