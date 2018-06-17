﻿namespace Pathfinder.Api.Controllers
{
	public class SearchChip
	{
		public string Name { get; set; }
		public string Value { get; set; }

		public override string ToString()
		{
			return $"{Name}:{Value}";
		}
	}
}
