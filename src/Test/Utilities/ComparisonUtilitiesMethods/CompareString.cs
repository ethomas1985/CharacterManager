using System.Collections;
using NUnit.Framework;
using Pathfinder.Utilities;

namespace Pathfinder.Test.Utilities.ComparisonUtilitiesMethods
{
	[TestFixture]
	public class CompareString
	{
		[Test]
		[TestCaseSource(typeof(TestCase), nameof(TestCase.Cases))]
		public bool Test(string pControl, string pTest)
		{
			return ComparisonUtilities.Compare(nameof(CompareSets), pControl, pTest, nameof(Test));
		}

		public static class TestCase
		{
			private const string Control = "One";

			public static IEnumerable Cases
			{
				get
				{
					yield return new TestCaseData(null, null).Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | null matches null.");

					yield return new TestCaseData(null, string.Empty).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | null doesn't match empty string.");

					yield return new TestCaseData(null, Control).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | null doesn't match Control.");

					yield return new TestCaseData(Control, Control).Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control matches Control.");

					yield return new TestCaseData(Control, string.Empty).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control matches empty string.");

					yield return new TestCaseData(Control, "Six").Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control doesn't match second string.");

					yield return new TestCaseData(Control, "One").Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control matches equivalent string.");
				}
			}
		}
	}
}
