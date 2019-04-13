using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Pathfinder.Startup {
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

            foreach (var item in asSet)
            {
                ItemSerializer.Serialize(pContext, pArgs, item);
            }

            pContext.Writer.WriteEndArray();
        }

        public Type ValueType { get; } = typeof(ISet<T>);
    }
}
