using System.IO;
using Newtonsoft.Json;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Library;
using Pathfinder.Repository;
using Pathfinder.Serializers.Xml;

namespace Pathfinder.Containers
{
    internal static class DependencyContainerExtensions
    {
        public static T RegisterLegacySingletons<T>(this T pRegistry, string pBaseDirectory)
            where T : IDependencyContainer
        {
            string filePath = Path.Combine(pBaseDirectory, "libraryPaths.json");

            var libraryPath = File.Exists(filePath)
                ? JsonConvert.DeserializeObject<LibraryPaths>(File.ReadAllText(filePath))
                : new LibraryPaths(pBaseDirectory);

            var classSerializer = new ClassXmlSerializer();
            var classRepository = new ClassRepository(classSerializer, libraryPath.ClassLibrary);
            pRegistry.RegisterInstance<ILegacyRepository<IClass>, ClassRepository>(classRepository);

            var featSerializer = new FeatXmlSerializer();
            var featRepository = new FeatRepository(featSerializer, libraryPath.FeatLibrary);
            pRegistry.RegisterInstance<ILegacyRepository<IFeat>, FeatRepository>(featRepository);

            var featureSerializer = new FeatureXmlSerializer();
            var featureRepository = new FeatureRepository(featureSerializer, libraryPath.ClassFeatureLibrary);
            pRegistry.RegisterInstance<ILegacyRepository<IFeature>, FeatureRepository>(featureRepository);

            var skillSerializer = new SkillXmlSerializer();
            var skillRepository = new SkillRepository(skillSerializer, libraryPath.SkillLibrary);
            pRegistry.RegisterInstance<ILegacyRepository<ISkill>, SkillRepository>(skillRepository);

            var traitSerializer = new TraitXmlSerializer();
            var traitRepository = new TraitRepository(traitSerializer, libraryPath.TraitLibrary);
            pRegistry.RegisterInstance<ILegacyRepository<ITrait>, TraitRepository>(traitRepository);

            var raceSerializer = new RaceXmlSerializer(traitRepository);
            var raceRepository = new RaceRepository(raceSerializer, libraryPath.RaceLibrary);
            pRegistry.RegisterInstance<ILegacyRepository<IRace>, RaceRepository>(raceRepository);

            var characterSerializer = new CharacterXmlSerializer();
            var characterRepository = new CharacterRepository(characterSerializer, libraryPath.CharacterLibrary);
            pRegistry.RegisterInstance<ILegacyRepository<ICharacter>, CharacterRepository>(characterRepository);

            return pRegistry;
        }

        public static T RegisterSingletons<T>(this T pRegistry) where T : IDependencyContainer
        {

            pRegistry.Register<IRepository<ISpell>, SpellMongoRepository>();
            pRegistry.Register<IRepository<IItem>, ItemMongoRepository>();

            return pRegistry;
        }
    }
}
