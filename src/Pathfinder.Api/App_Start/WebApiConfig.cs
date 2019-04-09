using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Dependencies;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pathfinder.Api.Controllers;
using Pathfinder.Api.Controllers.OData;
using Pathfinder.Enums;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Serializers.Json;
using Pathfinder.Utilities;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace Pathfinder.Api
{
    public static class WebApiConfig
    {
        private static T InitializeContainer<T>(T pContainer) where T: class, IDependencyContainer
        {
            return PathfinderConfiguration.Instance
                .InitializeContainer(pContainer, Path.GetFullPath("."));
        }

        public static void Register(HttpConfiguration pConfig)
        {
            // SWITCH TO simple.injector....
            var dependencyContainer = InitializeContainer(new DependencyContainer());
            dependencyContainer.Container.RegisterWebApiControllers(pConfig);

            pConfig.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            RegisterJsonConverters(dependencyContainer, pConfig);

            pConfig.MessageHandlers.Add(new LogRequestAndResponseHandler());

            var cors = new EnableCorsAttribute("http://localhost:8888", "*", "*");
            pConfig.EnableCors(cors);
            // Web API configuration and services

            pConfig.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(dependencyContainer.Container);

            // Web API routes
            pConfig.MapHttpAttributeRoutes();

            pConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            pConfig.MapODataServiceRoute("ODataRoute", "odata", GetEdmModel(),
                                         new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataModelBuilder()
            {
                Namespace = "Pathfinder",
                ContainerName = "Models"
            };
            builder.AddEnumTypeAndValues<MagicSchool>()
                .AddEnumTypeAndValues<MagicSubSchool>()
                .AddEnumTypeAndValues<MagicDescriptor>()
                .AddEnumTypeAndValues<ComponentType>();

            //var kvpType = typeof(KeyValuePair<string, int>);
            //var kvpEdmType = builder.AddComplexType(kvpType);
            //kvpEdmType.AddProperty(kvpType.GetProperty("Key"));
            //kvpEdmType.AddProperty(kvpType.GetProperty("Value"));

            var spellEntityConfig = builder.EntitySet<ISpell>("Spells").EntityType;
            spellEntityConfig.Name = "Spell";
            spellEntityConfig.HasKey(x => x.Name);
            spellEntityConfig.EnumProperty(x => x.School);
            spellEntityConfig.CollectionProperty(x => x.SubSchools);
            spellEntityConfig.CollectionProperty(x => x.MagicDescriptors);
            spellEntityConfig.Property(x => x.SavingThrow);
            spellEntityConfig.CollectionProperty(x => x.Description);
            spellEntityConfig.Property(x => x.HasSpellResistance);
            spellEntityConfig.Property(x => x.SpellResistance);
            spellEntityConfig.Property(x => x.CastingTime);
            spellEntityConfig.Property(x => x.Range);
            spellEntityConfig.ComplexProperty(x => x.LevelRequirements);
            spellEntityConfig.Property(x => x.Duration);
            spellEntityConfig.CollectionProperty(x => x.Components);

            var spellComponentEntityTypeConfig = builder.ComplexType<ISpellComponent>();
            spellComponentEntityTypeConfig.EnumProperty(x => x.ComponentType);
            spellComponentEntityTypeConfig.Property(x => x.Description);

            return builder.GetEdmModel();
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
        public Container Container { get; }

        public DependencyContainer()
        {
            Container = new Container();
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
