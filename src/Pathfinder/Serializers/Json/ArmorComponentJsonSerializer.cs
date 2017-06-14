using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Item;
using Pathfinder.Model.Items;

namespace Pathfinder.Serializers.Json
{
	public class ArmorComponentJsonSerializer : AbstractJsonSerializer<IArmorComponent>
	{
		protected override void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, IArmorComponent pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(IArmorComponent.ArmorBonus), pValue.ArmorBonus);
			WriteProperty(pWriter, pSerializer, nameof(IArmorComponent.ShieldBonus), pValue.ShieldBonus);
			WriteProperty(pWriter, pSerializer, nameof(IArmorComponent.MaximumDexterityBonus), pValue.MaximumDexterityBonus);
			WriteProperty(pWriter, pSerializer, nameof(IArmorComponent.ArmorCheckPenalty), pValue.ArmorCheckPenalty);
			WriteProperty(pWriter, pSerializer, nameof(IArmorComponent.ArcaneSpellFailureChance), pValue.ArcaneSpellFailureChance);
			WriteProperty(pWriter, pSerializer, nameof(IArmorComponent.SpeedModifier), pValue.SpeedModifier);

			pWriter.WriteEndObject();
		}

		protected override IArmorComponent DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject)
		{
			var armorBonus = GetInt(pJobject, nameof(IArmorComponent.ArmorBonus));
			var shieldBonus = GetInt(pJobject, nameof(IArmorComponent.ShieldBonus));
			var maximumDexterityBonus = GetInt(pJobject, nameof(IArmorComponent.MaximumDexterityBonus));
			var armorCheckPenalty = GetInt(pJobject, nameof(IArmorComponent.ArmorCheckPenalty));
			var arcaneSpellFailureChance = GetDecimal(pJobject, nameof(IArmorComponent.ArcaneSpellFailureChance));
			var speed = GetInt(pJobject, nameof(IArmorComponent.SpeedModifier));
			return new ArmorComponent(
				armorBonus, shieldBonus, maximumDexterityBonus, armorCheckPenalty, arcaneSpellFailureChance, speed);
		}
	}
}
