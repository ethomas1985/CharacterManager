using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Pathfinder.Utilities;

namespace Pathfinder.Test.Utilities.ComparisonUtilitiesMethods
{
	[TestFixture]
	public class CompareSets
	{
		[Test]
		[TestCaseSource(typeof(TestCase), nameof(TestCase.Cases))]
		public bool Test(ISet<string> pControl, ISet<string> pTest)
		{
			return ComparisonUtilities.CompareSets(nameof(CompareSets), pControl, pTest, nameof(Test));
		}

		public static class TestCase
		{
			private static readonly HashSet<string> ControlList
			= new HashSet<string>
			{
				"One",
				"Two",
				"Three",
			};

			public static IEnumerable Cases
			{
				get
				{
					yield return new TestCaseData(null, null).Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | null matches null.");

					yield return new TestCaseData(null, new HashSet<string>()).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | null doesn't match empty List.");

					yield return new TestCaseData(null, ControlList).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | null doesn't match Control.");

					yield return new TestCaseData(ControlList, ControlList).Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control matches Control.");

					yield return new TestCaseData(ControlList, new HashSet<string>()).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control doesn't match empty list.");

					yield return new TestCaseData(ControlList, new HashSet<string> { "Four", "Five", "Six" }).Returns(false)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control doesn't match Second List.");

					yield return new TestCaseData(ControlList, new HashSet<string> { "One", "Two", "Three" }).Returns(true)
						.SetName($"{nameof(CompareEnumerables.Test)} | Control matches equivalent List.");
				}
			}
		}
	}
}
