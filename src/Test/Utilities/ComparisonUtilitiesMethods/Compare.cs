using System.Collections;
using NUnit.Framework;
using Pathfinder.Utilities;

namespace Pathfinder.Test.Utilities.ComparisonUtilitiesMethods
{
	[TestFixture]
	public class Compare
	{
		[Test]
		[TestCaseSource(typeof(TestCase), nameof(TestCase.Cases))]
		public bool Test(int pControl, int pTest)
		{
			return ComparisonUtilities.Compare(nameof(CompareSets), pControl, pTest, nameof(Test));
		}

		public static class TestCase
		{
			private const int Control = 1;

			public static IEnumerable Cases
			{
				get
				{
					yield return new TestCaseData(Control, Control).Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control matches Control.");

					yield return new TestCaseData(Control, 0).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control doesn't match 0.");

					yield return new TestCaseData(Control, 6).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control doesn't match second int.");

					yield return new TestCaseData(Control, 1).Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control matches equivalent int.");
				}
			}
		}
	}
}
