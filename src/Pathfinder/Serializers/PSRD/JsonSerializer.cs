using System.Linq;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface;

namespace Pathfinder.Serializers.PSRD
{
	public abstract class JsonSerializer<TModel, TSerialized> : ISerializer<TModel, TSerialized>
	{
		protected static string GetString(JObject jObject, string pField)
		{
			return (string) jObject[pField];
		}

		protected static string GetStringFor(JObject jObject, string pField, string pValue)
		{
			var section = jObject["sections"].Children().Where(x => x[pField] != null && ((string) x[pField]).Equals(pValue));
			return section.Select(x => (string) x["body"]).FirstOrDefault();
		}

		protected static bool GetBoolean(JObject jObject, string pField)
		{
			bool value;
			return bool.TryParse((string) jObject[pField], out value) && value;
		}

		public abstract TModel Deserialize(TSerialized pValue);
		public abstract TSerialized Serialize(TModel pObject);
	}
}