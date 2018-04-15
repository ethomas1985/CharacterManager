using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Serializers.Json;

namespace Pathfinder.Api
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;

            var manager =
                PathfinderConfiguration.Instance
                    .CreatePathfinderManager(HttpRuntime.BinDirectory);

            var classLibrary = manager.Get<ILegacyRepository<IClass>>();
            var raceLibrary = manager.Get<ILegacyRepository<IRace>>();
            var skillLibrary = manager.Get<ILegacyRepository<ISkill>>();

            jsonFormatter.SerializerSettings.Converters =
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
                    new WeaponSpecialJsonSerializer(),
                };
        }
    }
}
