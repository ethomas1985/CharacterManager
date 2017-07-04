using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pathfinder.Utilities
{
	internal static class stringExtensions
	{
		private static readonly Regex Pattern = new Regex(@"\w+");
		private static readonly Dictionary<string, int> NumberTable =
			new Dictionary<string, int>
			{
				["zero"] = 0,
				["one"] = 1,
				["two"] = 2,
				["three"] = 3,
				["four"] = 4,
				["five"] = 5,
				["six"] = 6,
				["seven"] = 7,
				["eight"] = 8,
				["nine"] = 9,
				["ten"] = 10,
				["eleven"] = 11,
				["twelve"] = 12,
				["thirteen"] = 13,
				["fourteen"] = 14,
				["fifteen"] = 15,
				["sixteen"] = 16,
				["seventeen"] = 17,
				["eighteen"] = 18,
				["nineteen"] = 19,
				["twenty"] = 20,
				["thirty"] = 30,
				["forty"] = 40,
				["fifty"] = 50,
				["sixty"] = 60,
				["seventy"] = 70,
				["eighty"] = 80,
				["ninety"] = 90,
				["hundred"] = 100,
				["thousand"] = 1000,
				["million"] = 1000000,
				["billion"] = 1000000000
			};

		public static int WrittenToInteger(this string pThis)
		{
			var numbers =
					Pattern.Matches(pThis)
						.Cast<Match>()
						.Select(m => m.Value.ToLowerInvariant())
						.Where(v => NumberTable.ContainsKey(v))
						.Select(v => NumberTable[v]);
			var acc = 0;
			var total = 0;
			foreach (var n in numbers)
			{
				if (n >= 1000)
				{
					total += (acc * n);
					acc = 0;
				}
				else if (n >= 100)
				{
					acc *= n;
				}
				else
				{
					acc += n;
				}
			}
			return (total + acc) * (pThis.StartsWith("minus", StringComparison.InvariantCultureIgnoreCase) ? -1 : 1);
		}
		
		public static bool AsBool(this string pThis)
		{
			return bool.TryParse(pThis, out bool value) && value;
		}

		public static int AsInt(this string pThis)
		{
			return int.TryParse(pThis, out int value) ? value : default(int);
		}

		public static decimal AsDecimal(this string pThis)
		{
			decimal value;
			return decimal.TryParse(pThis, out value) ? value : default(decimal);
		}

		public static string SplitCamelCase(this string pThis)
		{
			return
				Regex.Replace(
					Regex.Replace(pThis, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"),
					@"(\p{Ll})(\P{Ll})",
					"$1 $2");
		}

		// Convert the string to Pascal case.
		public static string ToPascalCase(this string pThis)
		{
			if (string.IsNullOrWhiteSpace(pThis))
			{
				return pThis;
			}

			pThis = new CultureInfo("en-US", false).TextInfo.ToTitleCase(pThis.SplitCamelCase());
			var parts = pThis.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
			var result = string.Join(string.Empty, parts);
			return result;
		}

		// Convert the string to camel case.
		public static string ToCamelCase(this string pThis)
		{
			if (string.IsNullOrWhiteSpace(pThis))
			{
				return pThis;
			}

			pThis = pThis.ToPascalCase();
			return pThis.Substring(0, 1).ToLower() + pThis.Substring(1);
		}
	}
}
