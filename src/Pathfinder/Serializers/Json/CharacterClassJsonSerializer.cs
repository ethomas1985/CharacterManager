using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Serializers.Json
{
	public class CharacterClassJsonSerializer : AbstractJsonSerializer<ICharacterClass>
	{
		public CharacterClassJsonSerializer(IRepository<IClass> pClassRepository)
		{
			ClassRepository = pClassRepository;
		}

		protected override void SerializeToJson(
			JsonWriter pWriter,
			JsonSerializer pSerializer,
			ICharacterClass pValue)
		{
			pWriter.WriteStartObject();

			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.Class), pValue.Class.Name);
			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.Level), pValue.Level);
			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.IsFavored), pValue.IsFavored);

			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.BaseAttackBonus), pValue.BaseAttackBonus);
			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.Fortitude), pValue.Fortitude);
			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.Reflex), pValue.Reflex);
			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.Will), pValue.Will);

			WriteProperty(pWriter, pSerializer, nameof(ICharacterClass.HitPoints), pValue.HitPoints);

			pWriter.WriteEndObject();
		}

		protected override ICharacterClass DeserializeFromJson(
			JsonSerializer pSerializer, JObject pJobject)
		{
			var className = GetString(pJobject, nameof(ICharacterClass.Class));
			if (string.IsNullOrEmpty(className))
			{
				throw new JsonException($"Missing Required Attribute: {nameof(ICharacterClass.Class)}");
			}
			var @class = ClassRepository[className];

			var level = GetInt(pJobject, nameof(ICharacterClass.Level));
			var isFavored = GetBoolean(pJobject, nameof(ICharacterClass.IsFavored));

			var hitPointTokens = pJobject.SelectToken(nameof(ICharacterClass.HitPoints));
			var hitPoints = hitPointTokens?.Values<int>() ?? new List<int>();

			return new CharacterClass(@class, level, isFavored, hitPoints);
		}

		public IRepository<IClass> ClassRepository { get; }
	}
}
