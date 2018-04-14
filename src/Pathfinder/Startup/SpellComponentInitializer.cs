using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pathfinder.Enums;
using Pathfinder.Model;

namespace Pathfinder.Startup
{
    internal class SpellComponentInitializer : AbstractModelInitializer<SpellComponent>
    {
        public override void Initializer(BsonClassMap<SpellComponent> pClassMap)
        {
            pClassMap.MapProperty(x => x.ComponentType)
                .SetSerializer(new EnumSerializer<ComponentType>(BsonType.String));
            pClassMap.MapProperty(x => x.Description);
            pClassMap.MapCreator(x => new SpellComponent(x.ComponentType, x.Description));
        }
    }
}
