﻿using System.Collections.Generic;

namespace Pathfinder.Api.Searching {
	public class SearchCriteria
	{
		public string SearchText { get; set; }
		public IEnumerable<SearchChip> Chips { get; set; }
	}
}
