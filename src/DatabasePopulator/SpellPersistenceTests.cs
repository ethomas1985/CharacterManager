using System;
using System.Linq;
using NUnit.Framework;
using Pathfinder;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Library;
using Pathfinder.Repository;
using Pathfinder.Test.Serializers.Json;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace DatabasePopulator
{
    [TestFixture, Explicit]
    public class SpellPersistenceTests
    {
        private const string RESOURCES_DIRECTORY =
            @"C:\Users\thean\Documents\GitHub\PathfinderReferenceDocumentScaper\parsedSite\spells\";
            //@"C:\Users\thean\Documents\GitHub\CharacterManager\resources\Spells\";

        [SetUp]
        public void TestSetUp()
        {
            PathfinderConfiguration.InitializePersistenceLayer();
            SetupTestFixtureForJsonSerializers.SetupJsonConverters();
            LogTo.ChangeLogLevel("Debug");
        }

        [Test]
        public void TestInsert()
        {
            var spellSerializer = new SpellJsonSerializer();
            var spellRepository = new SpellFileSystemRepository(spellSerializer, RESOURCES_DIRECTORY, "*.json");
            Assert.That(spellRepository.Values, Is.Not.Empty);

            var spell = spellRepository.Values.First();

            var spellStore = new SpellMongoRepository(
                new MongoSettings(
                    new Uri("mongodb://localhost:27017"), "pathfinder"));

            LogTo.Info("{Class}.{Method}|Loaded Spell \"{Name}\" from file system.",
                       nameof(SpellPersistenceTests), nameof(TestInsert), spell.Name);

            LogTo.Info("{Class}.{Method}|Looking at \"{spell.Name}\"",
                       nameof(SpellPersistenceTests), nameof(TestInsert), spell.Name);
            var storedSpell = spellStore.Get(spell.Name);
            if (storedSpell == null)
            {
                LogTo.Info("{Class}.{Method}|Saving spell \"{spell.Name}\"",
                           nameof(SpellPersistenceTests), nameof(TestInsert), spell.Name);
                spellStore.Insert(spell);
            }

            Assert.That(spellStore.Get(spell.Name), Is.Not.Null);
        }

        [Test, Explicit]
        public void UpdateMongoDatabase()
        {
            var spellSerializer = new SpellJsonSerializer();
            var spellRepository = new SpellFileSystemRepository(spellSerializer, RESOURCES_DIRECTORY, "*.json");
            var spells = spellRepository.Values.OrderBy(x => x.Name).ToList();

            var spellStore = new SpellMongoRepository(
                new MongoSettings(
                    new Uri("mongodb://localhost:27017"), "pathfinder"));

            LogTo.Info("{Class}.{Method}|Loaded {Count} Spells from file system.",
                       nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spells.Count);

            foreach (var spell in spells)
            {
                LogTo.Info("{Class}.{Method}|Looking at \"{Name}\"",
                           nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spell.Name);
                var storedSpell = spellStore.Get(spell.Name);
                if (storedSpell == null)
                {
                    LogTo.Info("{Class}.{Method}|Saving spell \"{Name}\"",
                               nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spell.Name);
                    spellStore.Insert(spell);
                }
                else
                {
                    LogTo.Info("{Class}.{Method}|Updating spell \"{Name}\"",
                               nameof(SpellPersistenceTests), nameof(UpdateMongoDatabase), spell.Name);
                    spellStore.Update(spell);
                }
            }

            Assert.That(spellStore.GetAll().Count(), Is.EqualTo(552));
        }
    }

    internal class SpellJsonSerializer : ISerializer<ISpell, string>
    {
        public ISpell Deserialize(string pValue)
        {
            try
            {
                return JsonConvert.DeserializeObject<ISpell>(pValue);
            }
            catch (Exception e)
            {
                LogTo.Exception(e);
                throw new Exception($"Exception deserializing value \"{pValue}\"", e);
            }
        }

        public string Serialize(ISpell pObject)
        {
            return JsonConvert.SerializeObject(pObject);
        }
    }
}
