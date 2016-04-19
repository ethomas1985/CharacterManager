namespace Pathfinder.Utilities
{
	internal static class intExtensions
	{
		public static string ToStringWithOrdinal(this int pThis)
		{
			if (pThis <= 0)
				return pThis.ToString();

			switch (pThis % 100)
			{
				case 11:
				case 12:
				case 13:
					return pThis + "th";
			}

			switch (pThis % 10)
			{
				case 1:
					return pThis + "st";
				case 2:
					return pThis + "nd";
				case 3:
					return pThis + "rd";
				default:
					return pThis + "th";
			}
		}

		private static readonly string[] Ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
		private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
		private static readonly string[] Tens = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
		private static readonly string[] ThousandsGroups = { "", " Thousand", " Million", " Billion" };

		private static string FriendlyInteger(this int pThis, string leftDigits = "", int thousands = 0)
		{
			if (pThis == 0)
			{
				return leftDigits;
			}

			var friendlyInt = leftDigits;

			if (friendlyInt.Length > 0)
			{
				friendlyInt += " ";
			}

			if (pThis < 10)
			{
				friendlyInt += Ones[pThis];
			}
			else if (pThis < 20)
			{
				friendlyInt += Teens[pThis - 10];
			}
			else if (pThis < 100)
			{
				friendlyInt += FriendlyInteger(pThis % 10, Tens[pThis / 10 - 2], 0);
			}
			else if (pThis < 1000)
			{
				friendlyInt += FriendlyInteger(pThis % 100, (Ones[pThis / 100] + " Hundred"), 0);
			}
			else
			{
				friendlyInt += FriendlyInteger(pThis % 1000, FriendlyInteger(pThis / 1000, "", thousands + 1), 0);
			}

			return friendlyInt + ThousandsGroups[thousands];
		}

		public static string IntegerToWritten(this int pThis)
		{
			if (pThis == 0)
			{
				return "Zero";
			}
			else if (pThis < 0)
			{
				return "Negative " + IntegerToWritten(-pThis);
			}

			return FriendlyInteger(pThis, "", 0);
		}
	}
}