using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Pathfinder.Enums;
using Pathfinder.Model;
using Pathfinder.Utilities;

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
					new EnumerableSerializer<MagicSubSchool>(
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

	internal class SetSerializer<T> : IBsonSerializer
	{
		private IBsonSerializer<T> ItemSerializer { get; }

		public SetSerializer(IBsonSerializer<T> pItemSerializer)
		{
			ItemSerializer = pItemSerializer;
		}

		public object Deserialize(BsonDeserializationContext pContext, BsonDeserializationArgs pArgs)
		{
			var asSet = new HashSet<T>();
			var bsonReader = pContext.Reader;

			bsonReader.ReadStartArray();

			while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
			{
				var item = ItemSerializer.Deserialize(pContext);
				asSet.Add(item);
			}

			bsonReader.ReadEndArray();

			return asSet;
		}

		public void Serialize(BsonSerializationContext pContext, BsonSerializationArgs pArgs, object pValue)
		{
			var asSet = pValue as ISet<T> ?? new HashSet<T>();
			pContext.Writer.WriteStartArray();

			foreach(var item in asSet)
			{
				ItemSerializer.Serialize(pContext, pArgs, item);
			}

			pContext.Writer.WriteEndArray();
		}

		public Type ValueType { get; } = typeof(ISet<T>);
	}

	internal class EnumerableSerializer<T> : IBsonSerializer
	{
		private IBsonSerializer<T> ItemSerializer { get; }

		public EnumerableSerializer(IBsonSerializer<T> pItemSerializer)
		{
			ItemSerializer = pItemSerializer;
		}

		public object Deserialize(BsonDeserializationContext pContext, BsonDeserializationArgs pArgs)
		{
			IEnumerable<T> asSet = new List<T>();
			var bsonReader = pContext.Reader;

			bsonReader.ReadStartArray();

			while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
			{
				var item = ItemSerializer.Deserialize(pContext);
				asSet = asSet.Append(item);
			}

			bsonReader.ReadEndArray();

			return asSet;
		}

		public void Serialize(BsonSerializationContext pContext, BsonSerializationArgs pArgs, object pValue)
		{
			IEnumerable<T> asSet = pValue as IEnumerable<T> ?? new List<T>();
			pContext.Writer.WriteStartArray();

			foreach (var item in asSet)
			{
				ItemSerializer.Serialize(pContext, pArgs, item);
			}

			pContext.Writer.WriteEndArray();
		}

		public Type ValueType { get; } = typeof(IEnumerable<T>);
	}
}
