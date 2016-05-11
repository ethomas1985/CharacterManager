using System;

namespace Pathfinder.Utilities
{
	public static class decimalExtensions
	{
		public static string ToStringWithRounding(this decimal pThis)
		{
			return decimal.Round(pThis, 2, MidpointRounding.AwayFromZero).ToString("F");
		}
	}
}
