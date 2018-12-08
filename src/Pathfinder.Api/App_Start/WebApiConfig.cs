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
using Pathfinder.Enums;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Repository;
using Pathfinder.Serializers.Json;
using Unity;

namespace Pathfinder.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration pConfig)
        {
            pConfig.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            RegisterJsonConverters(pConfig);

            pConfig.MessageHandlers.Add(new LogRequestAndResponseHandler());

            var cors = new EnableCorsAttribute("http://localhost:8888", "*", "*");
            pConfig.EnableCors(cors);
            // Web API configuration and services

            var container = new UnityContainer();
            container.RegisterType<IRepository<ISpell>, SpellMongoRepository>(new HierarchicalLifetimeManager());
            pConfig.DependencyResolver = new UnityResolver(container);

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

        private static void RegisterJsonConverters(HttpConfiguration pConfig)
        {
            var manager =
                PathfinderConfiguration.Instance
                    .CreatePathfinderManager(Path.GetFullPath("."));

            var classLibrary = manager.Get<ILegacyRepository<IClass>>();
            var raceLibrary = manager.Get<ILegacyRepository<IRace>>();
            var skillLibrary = manager.Get<ILegacyRepository<ISkill>>();
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

    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer Container { get; }

        public UnityResolver(IUnityContainer container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
