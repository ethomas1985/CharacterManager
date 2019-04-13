using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Model.Currency;
using Pathfinder.Model.Items;

namespace Pathfinder.Startup
{
    internal class ItemModelInitializer : AbstractModelInitializer<Item>
    {
        public override void Initializer(BsonClassMap<Item> pClassMap)
        {
            pClassMap.MapProperty(x => x.Name);
            pClassMap.MapProperty(x => x.ItemType)
                .SetSerializer(new EnumSerializer<ItemType>(BsonType.String));
            pClassMap.MapProperty(x => x.Category);
            pClassMap.MapProperty(x => x.Cost);
            pClassMap.MapProperty(x => x.Weight);
            pClassMap.MapProperty(x => x.Description);
            pClassMap.MapProperty(x => x.WeaponComponent);
            pClassMap.MapProperty(x => x.ArmorComponent);

            pClassMap.MapCreator(
                x => new Item(x.Name, x.ItemType, x.Category, x.Cost, x.Weight, x.Description, x.WeaponComponent,
                              x.ArmorComponent));
        }
    }

    internal class PurseModelInitializer : AbstractModelInitializer<Purse>
    {
        public override void Initializer(BsonClassMap<Purse> pClassMap)
        {
            pClassMap.MapProperty(x => x.Copper);
            pClassMap.MapProperty(x => x.Silver);
            pClassMap.MapProperty(x => x.Gold);
            pClassMap.MapProperty(x => x.Platinum);

            pClassMap.MapCreator(
                x => new Purse(x.Copper, x.Silver, x.Gold, x.Platinum));
        }
    }

    internal class WeaponComponentModelInitializer : AbstractModelInitializer<WeaponComponent>
    {
        public override void Initializer(BsonClassMap<WeaponComponent> pClassMap)
        {
            pClassMap.MapProperty(x => x.Proficiency)
                .SetSerializer(new EnumSerializer<Proficiency>(BsonType.String));
            pClassMap.MapProperty(x => x.WeaponType)
                .SetSerializer(new EnumSerializer<WeaponType>(BsonType.String));
            pClassMap.MapProperty(x => x.Encumbrance)
                .SetSerializer(new EnumSerializer<Encumbrance>(BsonType.String));
            pClassMap.MapProperty(x => x.Size)
                .SetSerializer(new EnumSerializer<WeaponSize>(BsonType.String));
            pClassMap.MapProperty(x => x.DamageType)
                .SetSerializer(new EnumSerializer<DamageType>(BsonType.String));
            pClassMap.MapProperty(x => x.BaseWeaponDamage)
                .SetSerializer(new GenericEnumerableSerializer<IDice>());
            pClassMap.MapProperty(x => x.CriticalThreat);
            pClassMap.MapProperty(x => x.CriticalMultiplier);
            pClassMap.MapProperty(x => x.Range);
            pClassMap.MapProperty(x => x.Specials)
                .SetSerializer(new GenericEnumerableSerializer<IWeaponSpecial>());


            pClassMap.MapCreator(
                x => new WeaponComponent(
                    x.Proficiency, x.WeaponType, x.Encumbrance, x.Size, x.DamageType,
                    x.BaseWeaponDamage, x.CriticalThreat, x.CriticalMultiplier, x.Range, x.Specials));
        }
    }

    internal class DiceModelInitializer : AbstractModelInitializer<Dice>
    {
        public override void Initializer(BsonClassMap<Dice> pClassMap)
        {
            pClassMap.MapProperty(x => x.DieCount);
            pClassMap.MapProperty(x => x.Die);

            pClassMap.MapCreator(
                x => new Dice(x.DieCount, x.Die));
        }
    }

    internal class WeaponSpecialModelInitializer : AbstractModelInitializer<WeaponSpecial>
    {
        public override void Initializer(BsonClassMap<WeaponSpecial> pClassMap)
        {
            pClassMap.MapProperty(x => x.Name);
            pClassMap.MapProperty(x => x.Description);

            pClassMap.MapCreator(
                x => new WeaponSpecial(x.Name, x.Description));
        }
    }

    internal class ArmorComponentModelInitializer : AbstractModelInitializer<ArmorComponent>
    {
        public override void Initializer(BsonClassMap<ArmorComponent> pClassMap)
        {
            pClassMap.MapProperty(x => x.ArmorBonus);
            pClassMap.MapProperty(x => x.ShieldBonus);
            pClassMap.MapProperty(x => x.MaximumDexterityBonus);
            pClassMap.MapProperty(x => x.ArmorCheckPenalty);
            pClassMap.MapProperty(x => x.ArcaneSpellFailureChance);
            pClassMap.MapProperty(x => x.SpeedModifier);

            pClassMap.MapCreator(
                x => new ArmorComponent(
                    x.ArmorBonus, x.ShieldBonus, x.MaximumDexterityBonus,
                    x.ArmorCheckPenalty, x.ArcaneSpellFailureChance, x.SpeedModifier));
        }
    }
}
