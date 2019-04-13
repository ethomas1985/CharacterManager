using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pathfinder.Enums;
using Pathfinder.Model;

namespace Pathfinder.Startup
{
    internal class SpellModelInitializer : AbstractModelInitializer<Spell>
    {
        public override void Initializer(BsonClassMap<Spell> pClassMap)
        {
            //pClassMap.SetDiscriminator(nameof(Spell));
            pClassMap.MapProperty(x => x.Name);
            pClassMap.MapProperty(x => x.School)
                .SetSerializer(new EnumSerializer<MagicSchool>(BsonType.String));
            pClassMap.MapProperty(x => x.SubSchools)
                .SetSerializer(
                    new GenericEnumerableSerializer<MagicSubSchool>(
                        new EnumSerializer<MagicSubSchool>(BsonType.String)));
            pClassMap.MapProperty(x => x.MagicDescriptors)
                .SetSerializer(
                    new SetSerializer<MagicDescriptor>(
                        new EnumSerializer<MagicDescriptor>(BsonType.String)));
            pClassMap.MapProperty(x => x.SavingThrow);
            pClassMap.MapProperty(x => x.Description);
            pClassMap.MapProperty(x => x.HasSpellResistance);
            pClassMap.MapProperty(x => x.SpellResistance);
            pClassMap.MapProperty(x => x.CastingTime);
            pClassMap.MapProperty(x => x.Range);
            pClassMap.MapProperty(x => x.LevelRequirements);
            pClassMap.MapProperty(x => x.Duration);
            pClassMap.MapProperty(x => x.Components);

            pClassMap.MapCreator(
                x => new Spell(x.Name, x.School, x.SubSchools, x.MagicDescriptors, x.SavingThrow,
                               x.Description, x.HasSpellResistance, x.SpellResistance, x.CastingTime,
                               x.Range, x.LevelRequirements, x.Duration, x.Components));
        }
    }
}
