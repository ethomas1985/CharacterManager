using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pathfinder.Utilities;

namespace Pathfinder.Serializers.Json
{
	public abstract class AbstractJsonSerializer<T> : JsonConverter where T : class
	{
		public override bool CanConvert(Type pObjectType)
		{
			var canConvert = typeof(T).IsAssignableFrom(pObjectType);
			//Tracer.Message($"{GetType().FullName} : {nameof(AbstractJsonSerializer<T>)} || {pObjectType.GetType().FullName} || {canConvert}");
			return canConvert;
		}

		public override void WriteJson(JsonWriter pWriter, object pValue, JsonSerializer pSerializer)
		{
			Assert.ArgumentNotNull(pWriter, nameof(pWriter));

			var valueAsT = pValue as T;
			Assert.IsTrue(valueAsT != null, $"{nameof(pValue)} was not an instance of {typeof(T).Name}");

			Assert.ArgumentNotNull(pSerializer, nameof(pSerializer));

			SerializeToJson(pWriter, pSerializer, valueAsT);
		}

		protected abstract void SerializeToJson(JsonWriter pWriter, JsonSerializer pSerializer, T pValue);

		protected static void WriteProperty(JsonWriter pWriter, JsonSerializer pSerializer, string pName, object pValue)
		{
			if (pValue == null)
			{
				return;
			}

			pWriter.WritePropertyName(pName);
			pSerializer.Serialize(pWriter, pValue);
		}

		protected static void WriteSimpleArray<T2>(
			JsonWriter pWriter,
			JsonSerializer pSerializer,
			string pPropertyName,
			IEnumerable<T2> pValues)
		{
			if (pValues == null)
			{
				return;
			}
			pWriter.WritePropertyName(pPropertyName);

			pWriter.WriteStartArray();
			WriteValues(pWriter, pSerializer, pValues);
			pWriter.WriteEndArray();
		}

		protected static void WriteArrayProperty<T2>(
			JsonWriter pWriter,
			JsonSerializer pSerializer,
			string pPropertyName,
			IEnumerable<T2> pValues)
		{
			pWriter.WritePropertyName(pPropertyName);
			pSerializer.Serialize(pWriter, pValues);
		}

		protected static void WriteSimpleDictionary<TKey, TValue>(
			JsonWriter pWriter,
			JsonSerializer pSerializer,
			string pPropertyName,
			IDictionary<TKey, TValue> pDictionary,
			Func<TKey, string> pKeySelector = null,
			Func<TValue, object> pValueSelector = null)
		{
			if (pDictionary == null)
			{
				return;
			}
			pWriter.WritePropertyName(pPropertyName);

			var keySelector = pKeySelector ?? (key => key.ToString());
			var valueSelector = pValueSelector ?? (value => value);

			pWriter.WriteStartObject();
			foreach (var keypair in pDictionary)
			{
				WriteProperty(pWriter, pSerializer, keySelector(keypair.Key), valueSelector(keypair.Value));
			}
			pWriter.WriteEndObject();
		}

		protected static void WriteValues<T2>(JsonWriter pWriter, JsonSerializer pSerializer, params T2[] pValues)
		{
			if (!pValues.Any())
			{
				return;
			}

			foreach (var value in pValues)
			{
				pSerializer.Serialize(pWriter, value);
			}
		}

		public override object ReadJson(JsonReader pReader, Type pObjectType, object pExistingValue, JsonSerializer pSerializer)
		{
			var jObject = JObject.Load(pReader);
			return DeserializeFromJson(pSerializer, jObject);
		}

		protected abstract T DeserializeFromJson(JsonSerializer pSerializer, JObject pJobject);

		protected static bool GetBoolean(JToken pJToken, string pField)
		{
			return bool.TryParse(GetString(pJToken, pField), out bool value) && value;
		}

		protected static string GetString(JToken pJToken, string pField)
		{
			return pJToken.SelectToken(pField)?.ToString();
		}

		protected static int GetInt(JToken pJToken, string pField)
		{
			return GetString(pJToken, pField).AsInt();
		}

		protected static decimal GetDecimal(JToken pJToken, string pField)
		{
			return GetString(pJToken, pField).AsDecimal();
		}

		protected static IDictionary<TKey, TValue> GetValuesFromHashObject<TKey, TValue>(
			JsonSerializer pSerializer, JToken pJobject, string pAttributeName)
		{
			var selectToken = pJobject
				.SelectToken(pAttributeName);
			if (selectToken == null)
			{
				return new Dictionary<TKey, TValue>();
			}

			return pSerializer.Deserialize<Dictionary<TKey, TValue>>(selectToken.CreateReader());
		}

		protected static IEnumerable<T2> GetValuesFromArray<T2>(JsonSerializer pSerializer, JToken pJobject, string pAttributeName = null)
		{
			var selectToken = pJobject
				.SelectToken(pAttributeName);
			if (selectToken == null)
			{
				return new T2[0];
			}
			return pSerializer.Deserialize<List<T2>>(selectToken.CreateReader());
		}
	}
}