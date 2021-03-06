﻿using Newtonsoft.Json;

namespace Pathfinder.Api.Searching {
	public class Bucket
	{
		public Bucket(string pValue, int pCount, bool pSelected = false)
		{
			Value = pValue;
			Count = pCount;
			Selected = pSelected;
		}

		public string Value { get; set; }
		public int Count { get; set; }
		public bool Selected { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this, Formatting.None);
		}
	}
}
