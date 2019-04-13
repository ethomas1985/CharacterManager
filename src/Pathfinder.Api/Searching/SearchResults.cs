using System.Collections.Generic;

namespace Pathfinder.Api.Searching {
	public class SearchResults<T>
	{
		public string SearchText { get; set; }
		public IEnumerable<Facet> Facets { get; set; }
		public IEnumerable<T> Results { get; set; }
		public int Count { get; set; }
	}
}
