using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Serializers.Json;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace Pathfinder.Api
{
    public static class WebApiConfig
    {
        private static T InitializeContainer<T>(T pContainer) where T : class, IDependencyContainer
        {
            return PathfinderConfiguration.Instance
                .InitializeContainer(pContainer, Path.GetFullPath("."));
        }

        public static void Register(HttpConfiguration pConfig)
        {
            pConfig.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            pConfig.MessageHandlers.Add(new LogRequestAndResponseHandler());

            var cors = new EnableCorsAttribute("http://localhost:8888", "*", "*");
            pConfig.EnableCors(cors);

            var dependencyContainer = InitializeContainer(new DependencyContainer());
            dependencyContainer.Container.RegisterWebApiControllers(pConfig);
            RegisterJsonConverters(dependencyContainer, pConfig);
            pConfig.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(dependencyContainer.Container);

            pConfig.Formatters.Add(new BsonMediaTypeFormatter());

            pConfig.MapHttpAttributeRoutes();
            pConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
        }

        private static void RegisterJsonConverters(IDependencyContainer pContainer, HttpConfiguration pConfig)
        {
            var classLibrary = pContainer.Get<ILegacyRepository<IClass>>();
            var raceLibrary = pContainer.Get<ILegacyRepository<IRace>>();
            var skillLibrary = pContainer.Get<ILegacyRepository<ISkill>>();
            pConfig.Formatters.JsonFormatter.SerializerSettings.Converters =
                new List<JsonConverter>
                {
                    new StringEnumConverter {CamelCaseText = true},
                    new AbilityScoreJsonSerializer(),
                    new AbilityTypeJsonSerializer(),
                    new ArmorComponentJsonSerializer(),
                    new CharacterClassJsonSerializer(classLibrary),
                    new CharacterJsonSerializer(raceLibrary, skillLibrary),
                    new ClassJsonSerializer(),
                    new ClassLevelJsonSerializer(),
                    new CurrencyJsonSerializer(),
                    new DefenseScoreJsonSerializer(),
                    new DiceJsonSerializer(),
                    new DieJsonSerializer(),
                    new EventJsonSerializer(),
                    new ExperienceJsonSerializer(),
                    new FeatJsonSerializer(),
                    new FeatureJsonSerializer(),
                    new InventoryJsonSerializer(),
                    new InventoryItemJsonSerializer(),
                    new ItemJsonSerializer(),
                    new LanguageJsonSerializer(),
                    new OffensiveScoreJsonSerializer(),
                    new PurseJsonSerializer(),
                    new RaceJsonSerializer(),
                    new SavingThrowJsonSerializer(),
                    new SkillJsonSerializer(),
                    new SkillScoreJsonSerializer(),
                    new SpellJsonSerializer(),
                    new SubFeatureJsonSerializer(),
                    new SpellComponentJsonSerializer(),
                    new TraitJsonSerializer(),
                    new WeaponComponentJsonSerializer(),
                    new WeaponSpecialJsonSerializer()
                };
        }
    }

    public class DependencyContainer : IDependencyContainer
    {
        public SimpleInjector.Container Container { get; }

        public DependencyContainer()
        {
            Container = new SimpleInjector.Container();
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        public IDependencyContainer Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            Container.Register<TInterface, TClass>(Lifestyle.Scoped);

            return this;
        }

        public IDependencyContainer RegisterInstance<TInterface, TClass>(TClass pSingleton)
            where TInterface : class
            where TClass : class, TInterface
        {
            Container.RegisterInstance<TInterface>(pSingleton);

            return this;
        }

        public T Get<T>() where T : class
        {
            return Container.GetInstance<T>();
        }
    }
}
