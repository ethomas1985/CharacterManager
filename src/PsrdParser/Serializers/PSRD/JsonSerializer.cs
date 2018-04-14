using System.Linq;
using Newtonsoft.Json.Linq;
using Pathfinder.Interface.Infrastructure;

namespace PsrdParser.Serializers.PSRD
{
	public abstract class JsonSerializer<TModel, TSerialized> : ISerializer<TModel, TSerialized>
	{
		protected static string getString(JObject pJObject, string pField)
		{
			return (string) pJObject[pField];
		}

		protected static string getStringFor(JObject pJObject, string pField, string pValue)
		{
			var sections = pJObject["sections"];
			var children = sections.Children();

			var section = children.Where(x => x[pField] != null && ((string) x[pField]).Equals(pValue));
			return section.Select(x => (string) x["body"]).FirstOrDefault();
		}

		protected static bool getBoolean(JObject pJObject, string pField)
		{
			bool value;
			return bool.TryParse((string) pJObject[pField], out value) && value;
		}

		public abstract TModel Deserialize(TSerialized pValue);
		public abstract TSerialized Serialize(TModel pObject);
	}
}
