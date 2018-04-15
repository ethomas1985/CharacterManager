using System.Collections.Generic;

namespace Pathfinder.Api.Controllers {
	public class SearchCriteria
	{
		public string SearchText { get; set; }
		public IEnumerable<Facet> Facets { get; set; }
	}
}