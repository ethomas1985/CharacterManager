using System.Collections.Generic;

namespace Pathfinder.Utilities
{
	internal static class EnumerableExtensions
	{
		public static IEnumerable<T> Append<T>(this IEnumerable<T> pThis, T pValue)
		{
			foreach (var value in pThis)
			{
				yield return value;
			}
			yield return pValue;
		}
		public static IEnumerable<T> Append<T>(this IEnumerable<T> pThis, IEnumerable<T> pEnumerable)
		{
			foreach (var value in pThis)
			{
				yield return value;
			}

			foreach (var value in pEnumerable)
			{
				yield return value;
			}
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
