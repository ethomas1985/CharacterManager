using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Model.Items;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public class WeaponComponentJsonSerializer : AbstractJsonSerializer<IWeaponComponent>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IWeaponComponent pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.Proficiency), pValue.Proficiency);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.WeaponType), pValue.WeaponType);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.Encumbrance), pValue.Encumbrance);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.Size), pValue.Size);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.DamageType), pValue.DamageType);

			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.BaseWeaponDamage), pValue.BaseWeaponDamage);

			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.CriticalThreat), pValue.CriticalThreat);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.CriticalMultiplier), pValue.CriticalMultiplier);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.Range), pValue.Range);
			WriteProperty(pWriter, pSerializer, nameof(IWeaponComponent.Specials), pValue.Specials);

			pWriter.WriteEndObject();
		}

		protected override IWeaponComponent DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var proficiencyString = GetString(pJobject, nameof(IWeaponComponent.Proficiency)).ToPascalCase();
			if (!Enum.TryParse(proficiencyString, out Proficiency proficiency))
			{
				proficiency = Proficiency.None;
			}

			var weaponTypeString = GetString(pJobject, nameof(IWeaponComponent.WeaponType)).ToPascalCase();
			if (!Enum.TryParse(weaponTypeString, out WeaponType weaponType))
			{
				weaponType = WeaponType.None;
			}

			var encumbranceString = GetString(pJobject, nameof(IWeaponComponent.Encumbrance)).ToPascalCase();
			if (!Enum.TryParse(encumbranceString, out Encumbrance encumbrance))
			{
				encumbrance = Encumbrance.None;
			}

			var weaponSizeString = GetString(pJobject, nameof(IWeaponComponent.Size)).ToPascalCase();
			if (!Enum.TryParse(weaponSizeString, out WeaponSize weaponSize))
			{
				weaponSize = WeaponSize.Medium;
			}

			var damageTypeString = GetString(pJobject, nameof(IWeaponComponent.DamageType)).ToPascalCase();
			if (!Enum.TryParse(damageTypeString, out DamageType damageType))
			{
				damageType = DamageType.None;
			}

			var baseWeaponDamage = GetValuesFromArray<IDice>(pSerializer, pJobject, nameof(IWeaponComponent.BaseWeaponDamage));
			var criticalThreat = GetInt(pJobject, nameof(IWeaponComponent.CriticalThreat));
			var criticalMultiplier = GetInt(pJobject, nameof(IWeaponComponent.CriticalMultiplier));
			var range = GetInt(pJobject, nameof(IWeaponComponent.Range));

			var weaponSpecials = GetValuesFromArray<IWeaponSpecial>(pSerializer, pJobject, nameof(IWeaponComponent.Specials));

			return new WeaponComponent(
				proficiency,
				weaponType,
				encumbrance,
				weaponSize,
				damageType,
				baseWeaponDamage,
				criticalThreat,
				criticalMultiplier,
				range,
				weaponSpecials);
		}
	}
}
