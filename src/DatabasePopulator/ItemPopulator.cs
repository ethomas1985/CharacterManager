using System;
using System.Linq;
using NUnit.Framework;
using Pathfinder;
using Pathfinder.Library;
using Pathfinder.Repository;
using Pathfinder.Serializers.Xml;
using Pathfinder.Test.Serializers.Json;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace DatabasePopulator
{
    [TestFixture, Explicit]
    public class ItemPopulator
    {
        private const string RESOURCES_DIRECTORY = @"C:\Users\thean\Documents\GitHub\CharacterManager\resources\Items\";
        //C:\Users\thean\Documents\GitHub\CharacterManager\resources\Items\Acid.xml

        [SetUp]
        public void TestSetUp()
        {
            PathfinderConfiguration.InitializePersistenceLayer();
            SetupTestFixtureForJsonSerializers.SetupJsonConverters();
            LogTo.ChangeLogLevel("Debug");
        }

        [Test, Explicit]
        public void UpdateMongoDatabase()
        {
            var itemXmlSerializer = new ItemXmlSerializer();
            var legacyItemRepository = new ItemRepository(itemXmlSerializer, RESOURCES_DIRECTORY);
            var items = legacyItemRepository.Values.OrderBy(x => x.Name).ToList();

            var itemMongoRepository = new ItemMongoRepository(
                new MongoSettings(
                    new Uri("mongodb://localhost:27017"), "pathfinder"));

            LogTo.Info("{Class}.{Method}|Loaded {Count} Spells from file system.",
                       nameof(ItemPopulator), nameof(UpdateMongoDatabase), items.Count);

            foreach (var item in items)
            {
                LogTo.Info("{Class}.{Method}|Looking at \"{Name}\"",
                           nameof(ItemPopulator), nameof(UpdateMongoDatabase), item.Name);
                var storedSpell = itemMongoRepository.Get(item.Name);
                if (storedSpell == null)
                {
                    LogTo.Info("{Class}.{Method}|Saving spell \"{Name}\"",
                               nameof(ItemPopulator), nameof(UpdateMongoDatabase), item.Name);
                    itemMongoRepository.Insert(item);
                }
                else
                {
                    LogTo.Info("{Class}.{Method}|Updating spell \"{Name}\"",
                               nameof(ItemPopulator), nameof(UpdateMongoDatabase), item.Name);
                    itemMongoRepository.Update(item);
                }
            }

            Assert.That(itemMongoRepository.GetAll().Count(), Is.EqualTo(585));
        }
    }
}
