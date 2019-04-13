using MongoDB.Bson.Serialization;
using Pathfinder.Model.Currency;

namespace Pathfinder.Startup
{
    internal class AbstractCurrencyModelInitializer : AbstractModelInitializer<AbstractCurrency>
    {
        public override void Initializer(BsonClassMap<AbstractCurrency> pClassMap)
        {
            pClassMap.MapProperty(x => x.Denomination);
            pClassMap.MapProperty(x => x.Value);
        }
    }

    internal class CopperModelInitializer : AbstractModelInitializer<Copper>
    {
        public override void Initializer(BsonClassMap<Copper> pClassMap)
        {
            pClassMap.MapCreator(
                x => new Copper(x.Value));
        }
    }

    internal class SilverModelInitializer : AbstractModelInitializer<Silver>
    {
        public override void Initializer(BsonClassMap<Silver> pClassMap)
        {
            pClassMap.MapCreator(
                x => new Silver(x.Value));
        }
    }

    internal class GoldModelInitializer : AbstractModelInitializer<Gold>
    {
        public override void Initializer(BsonClassMap<Gold> pClassMap)
        {
            pClassMap.MapCreator(
                x => new Gold(x.Value));
        }
    }

    internal class PlatinumModelInitializer : AbstractModelInitializer<Platinum>
    {
        public override void Initializer(BsonClassMap<Platinum> pClassMap)
        {
            pClassMap.MapCreator(
                x => new Platinum(x.Value));
        }
    }
}
