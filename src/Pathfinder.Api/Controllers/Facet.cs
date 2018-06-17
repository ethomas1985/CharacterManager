using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pathfinder.Api.Controllers {
	public class Facet
	{
		public Facet(string pId, string pName, IEnumerable<Bucket> pBuckets)
		{
			Id = pId;
			Name = pName;
			Buckets = pBuckets;
		}

		/// <summary>
		/// Unique identifier for lookup.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Value intended to be displayed.
		/// </summary>
		public string Name { get; set; }

		public IEnumerable<Bucket> Buckets { get; set; }
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.None);
		}
	}
}
