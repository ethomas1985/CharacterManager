using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
// ReSharper disable ArgumentsStyleNamedExpression
// ReSharper disable ExplicitCallerInfoArgument

namespace Pathfinder.Utilities
{
	internal class ComparisonUtilities
	{
		public static bool CompareDictionaries<TKey, TValue>(
			string pClass,
			IDictionary<TKey, IEnumerable<TValue>> pThis,
			IDictionary<TKey, IEnumerable<TValue>> pOther,
			string pFieldName,
			[CallerMemberName] string pCallerName = null)
		{
			if (ReferenceEquals(null, pThis) || ReferenceEquals(null, pOther))
			{
				return ReferenceEquals(null, pThis) && ReferenceEquals(null, pOther);
			}

			if (pThis.Count != pOther.Count)
			{
				Tracer.Message(
					$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}.Count " +
					$"=/= {nameof(pOther)}.{pFieldName}.Count",
					pCallerName: pCallerName);
				return false;
			}

			bool compare = true;
			foreach (var key in pThis.Keys)
			{
				var theseValues = pThis[key];
				if (!pOther.TryGetValue(key, out var otherValues))
				{
					Tracer.Message(
						$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}.Keys: [{string.Join(", ", pThis.Keys)}] " +
						$"=/= {nameof(pOther)}.{pFieldName}.Keys: [{string.Join(", ", pOther.Keys)}]",
						pCallerName: pCallerName);
					return false;
				}

				compare &= CompareEnumerables(pClass, theseValues, otherValues, $"{pFieldName}[{key}]");
			}
			return compare;
		}

		public static bool CompareDictionaries<TKey, TValue>(
			string pClass,
			IDictionary<TKey, TValue> pThis,
			IDictionary<TKey, TValue> pOther,
			string pFieldName,
			[CallerMemberName] string pCallerName = null)
		{
			if (ReferenceEquals(null, pThis) || ReferenceEquals(null, pOther))
			{
				return ReferenceEquals(null, pThis) && ReferenceEquals(null, pOther);
			}

			if (pThis.Count != pOther.Count)
			{
				Tracer.Message(
					$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}.Count " +
					$"=/= {nameof(pOther)}.{pFieldName}.Count",
					pCallerName: pCallerName);
				return false;
			}

			bool compare = true;
			foreach (var key in pThis.Keys)
			{
				var theseValues = pThis[key];
				if (!pOther.TryGetValue(key, out var otherValues))
				{
					Tracer.Message(
						$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}.Keys: [{string.Join(", ", pThis.Keys)}] " +
						$"=/= {nameof(pOther)}.{pFieldName}.Keys: [{string.Join(", ", pOther.Keys)}]",
						pCallerName: pCallerName);
					return false;
				}

				compare &= Compare(pClass, theseValues, otherValues, $"{pFieldName}[{key}]");
			}
			return compare;
		}

		public static bool CompareEnumerables<T>(string pClass, IEnumerable<T> pThis, IEnumerable<T> pOther, string pFieldName, [CallerMemberName] string pCallerName = null)
		{
			if (ReferenceEquals(null, pThis) || ReferenceEquals(null, pOther))
			{
				return ReferenceEquals(null, pThis) && ReferenceEquals(null, pOther);
			}

			var thisArray = pThis as T[] ?? pThis.ToArray();
			var otherArray = pOther as T[] ?? pOther.ToArray();
			if (thisArray.SequenceEqual(otherArray))
			{
				return true;
			}

			Tracer.Message(
				$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}: [{string.Join(", ", thisArray)}] =/= {nameof(pOther)}.{pFieldName}: [{string.Join(", ", otherArray)}]",
				pCallerName: pCallerName);
			return false;
		}

		public static bool CompareSets<T>(string pClass, ISet<T> pThis, ISet<T> pOther, string pFieldName, [CallerMemberName] string pCallerName = null)
		{
			if (ReferenceEquals(null, pThis) || ReferenceEquals(null, pOther))
			{
				return ReferenceEquals(null, pThis) && ReferenceEquals(null, pOther);
			}

			if (pThis.SetEquals(pOther))
			{
				return true;
			}

			Tracer.Message(
				$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}: [{string.Join(", ", pThis)}] =/= {nameof(pOther)}.{pFieldName}: [{string.Join(", ", pOther)}]",
				pCallerName: pCallerName);
			return false;
		}

		public static bool Compare(string pClass, string pThis, string pOther, string pFieldName, [CallerMemberName] string pCallerName = null)
		{
			if (string.Equals(pThis, pOther, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}

			Tracer.Message(
				$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}: {pThis} =/= {nameof(pOther)}.{pFieldName}: {pOther}",
				pCallerName: pCallerName);
			return false;
		}

		public static bool Compare<T>(string pClass, T pThis, T pOther, string pFieldName, [CallerMemberName] string pCallerName = null)
		{
			var isThisNull = pThis == null;
			var isOtherNull = pOther == null;
			if (isThisNull && !isOtherNull)
			{
				return false;
			}
			if (ReferenceEquals(pThis, pOther))
			{
				return true;
			}

			if (Equals(pThis, pOther))
			{
				return true;
			}

			Tracer.Message(
				$"{pClass}\t|\t{nameof(pThis)}.{pFieldName}: {pThis} =/= {nameof(pOther)}.{pFieldName}: {pOther}",
				pCallerName: pCallerName);
			return false;
		}
	}
}
