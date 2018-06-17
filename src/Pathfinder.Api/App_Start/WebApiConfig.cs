using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Serializers.Json;

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

            // Web API routes
            pConfig.MapHttpAttributeRoutes();

            pConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
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
}
