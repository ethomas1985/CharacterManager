using System;
using Pathfinder.Repository;
using Pathfinder.Startup;
using Pathfinder.Utilities;

namespace Pathfinder
{
    public class PathfinderConfiguration
    {
        private static PathfinderConfiguration _instance;

        public static PathfinderConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PathfinderConfiguration();
                }

                return _instance;
            }
        }

        private PathfinderConfiguration()
        {
            LogTo.Info("STARTUP!");
            InitializePersistenceLayer();
        }

        public T InitializeContainer<T>(T pContainer, string pBaseDirectory) where T: class, IDependencyContainer
        {
            return pContainer
                .RegisterInstance<IMongoSettings, MongoSettings>(
                    new MongoSettings(new Uri("mongodb://localhost:27017"), "pathfinder"))
                .RegisterLegacySingletons(pBaseDirectory)
                .RegisterSingletons() as T;
        }

        public static void InitializePersistenceLayer()
        {
            LogTo.Info($"{nameof(InitializePersistenceLayer)}");

            new SpellModelInitializer().Register();
            new SpellComponentInitializer().Register();
        }
    }
}
