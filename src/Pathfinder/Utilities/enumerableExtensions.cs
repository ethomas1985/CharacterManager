﻿using System.Collections.Generic;

namespace Pathfinder.Utilities
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Append<T>(this IEnumerable<T> pThis, T pValue)
		{
			foreach (var value in pThis)
			{
				yield return value;
			}
			yield return pValue;
		}

		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> pThis, T pValue)
		{
			yield return pValue;
			foreach (var value in pThis)
			{
				yield return value;
			}
		}
	}
}
