namespace Pathfinder.Api.Controllers {
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
	}
}
