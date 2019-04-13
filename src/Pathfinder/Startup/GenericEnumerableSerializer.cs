using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Pathfinder.Utilities;

namespace Pathfinder.Startup
{
    internal class GenericEnumerableSerializer<T> : IBsonSerializer
    {
        private readonly Lazy<IBsonSerializer<T>> _lazySerializer =
            new Lazy<IBsonSerializer<T>>(() => BsonSerializer.SerializerRegistry.GetSerializer<T>());

        private readonly IBsonSerializer<T> _itemBsonSerializer;

        public GenericEnumerableSerializer()
        {
            _itemBsonSerializer = null;
        }

        public GenericEnumerableSerializer(IBsonSerializer<T> pItemSerializer)
        {
            _itemBsonSerializer = pItemSerializer;
        }

        private IBsonSerializer<T> ItemBsonSerializer => _itemBsonSerializer ?? _lazySerializer.Value;

        public object Deserialize(BsonDeserializationContext pContext, BsonDeserializationArgs pArgs)
        {
            IEnumerable<T> asSet = new List<T>();
            var bsonReader = pContext.Reader;

            bsonReader.ReadStartArray();

            while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
            {
                var item = ItemBsonSerializer.Deserialize(pContext);
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
                ItemBsonSerializer.Serialize(pContext, pArgs, item);
            }

            pContext.Writer.WriteEndArray();
        }

        public Type ValueType { get; } = typeof(IEnumerable<T>);
    }
}
