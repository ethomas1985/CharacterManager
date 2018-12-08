using System.IO;
using Newtonsoft.Json;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Library;
using Pathfinder.Repository;
using Pathfinder.Serializers.Xml;
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

        public PathfinderManager CreatePathfinderManager(string pBaseDirectory)
        {
            return new PathfinderManager()
                .RegisterLegacySingletons(pBaseDirectory)
                .RegisterSingletons();
        }

        public static void InitializePersistenceLayer()
        {
            LogTo.Info($"{nameof(InitializePersistenceLayer)}");

            new SpellModelInitializer().Register();
            new SpellComponentInitializer().Register();
        }
    }

    internal static class SingletonRegistryExtensions
    {
        public static PathfinderManager RegisterLegacySingletons(this PathfinderManager pRegistry,
            string pBaseDirectory)
        {
            string filePath = Path.Combine(pBaseDirectory, "libraryPaths.json");

            var libraryPath = File.Exists(filePath)
                ? JsonConvert.DeserializeObject<LibraryPaths>(File.ReadAllText(filePath))
                : new LibraryPaths(pBaseDirectory);

            var classSerializer = new ClassXmlSerializer();
            var classRepository = new ClassRepository(classSerializer, libraryPath.ClassLibrary);
            pRegistry.Register<ILegacyRepository<IClass>>(classRepository);

            var featSerializer = new FeatXmlSerializer();
            var featRepository = new FeatRepository(featSerializer, libraryPath.FeatLibrary);
            pRegistry.Register<ILegacyRepository<IFeat>>(featRepository);

            var featureSerializer = new FeatureXmlSerializer();
            var featureRepository = new FeatureRepository(featureSerializer, libraryPath.ClassFeatureLibrary);
            pRegistry.Register<ILegacyRepository<IFeature>>(featureRepository);

            var skillSerializer = new SkillXmlSerializer();
            var skillRepository = new SkillRepository(skillSerializer, libraryPath.SkillLibrary);
            pRegistry.Register<ILegacyRepository<ISkill>>(skillRepository);

            //var spellSerializer = new SpellXmlSerializer();
            //var spellRepository = new SpellFileSystemRepository(spellSerializer, libraryPath.SpellLibrary);
            //pRegistry.Register<ILegacyRepository<ISpell>>(spellRepository);

            var traitSerializer = new TraitXmlSerializer();
            var traitRepository = new TraitRepository(traitSerializer, libraryPath.TraitLibrary);
            pRegistry.Register<ILegacyRepository<ITrait>>(traitRepository);

            var raceSerializer = new RaceXmlSerializer(traitRepository);
            var raceRepository = new RaceRepository(raceSerializer, libraryPath.RaceLibrary);
            pRegistry.Register<ILegacyRepository<IRace>>(raceRepository);

            var itemSerializer = new ItemXmlSerializer();
            var itemRepository = new ItemRepository(itemSerializer, libraryPath.ItemLibrary);
            pRegistry.Register<ILegacyRepository<IItem>>(itemRepository);

            var characterSerializer = new CharacterXmlSerializer();
            var characterRepository = new CharacterRepository(characterSerializer, libraryPath.CharacterLibrary);
            pRegistry.Register<ILegacyRepository<ICharacter>>(characterRepository);

            return pRegistry;
        }

        public static PathfinderManager RegisterSingletons(this PathfinderManager pRegistry)
        {
            pRegistry.Register<IRepository<ISpell>>(new SpellMongoRepository());

            return pRegistry;
        }
    }
}
