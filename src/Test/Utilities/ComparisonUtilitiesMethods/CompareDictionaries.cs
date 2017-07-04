using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Pathfinder.Utilities;

namespace Pathfinder.Test.Utilities.ComparisonUtilitiesMethods
{
	[TestFixture]
	public class CompareDictionaries
	{
		[Test]
		[TestCaseSource(typeof(TestCase), nameof(TestCase.Cases))]
		public bool TestWithSingleValues(IDictionary<string, string> pControl, IDictionary<string, string> pTest)
		{
			return ComparisonUtilities.CompareDictionaries(nameof(CompareDictionaries), pControl, pTest, nameof(Test));
		}

		public static class TestCase
		{
			private static readonly Dictionary<string, string> ControlList
			= new Dictionary<string, string>
			{
				["One"] = "One",
				["Two"] = "Two",
				["Three"] = "Three",
			};

			public static IEnumerable Cases
			{
				get
				{
					yield return new TestCaseData(null, null).Returns(true)
						.SetName($"{nameof(Test)} | null matches null.");

					yield return new TestCaseData(null, new Dictionary<string, string>()).Returns(false)
						.SetName($"{nameof(Test)} | null doesn't match empty List.");

					yield return new TestCaseData(null, ControlList).Returns(false)
						.SetName($"{nameof(Test)} | null doesn't match Control.");

					yield return new TestCaseData(ControlList, ControlList).Returns(true)
						.SetName($"{nameof(Test)} | Control matches Control.");

					yield return new TestCaseData(ControlList, new Dictionary<string, string>()).Returns(false)
						.SetName($"{nameof(Test)} | Control doesn't match empty list.");

					yield return
						new TestCaseData(
							ControlList,
							new Dictionary<string, string>
							{
								["Four"] = "Four",
								["Five"] = "Five",
								["Six"] = "Six"
							})
						.Returns(false)
						.SetName($"{nameof(Test)} | Control doesn't match Second List.");

					yield return
						new TestCaseData(
							ControlList,
							new Dictionary<string, string>
							{
								["One"] = "One",
								["Two"] = "Two",
								["Three"] = "Three"
							})
						.Returns(true)
						.SetName($"{nameof(Test)} | Control matches equivalent List.");
				}
			}
		}

		[Test]
		[TestCaseSource(typeof(EnumerableTestCase), nameof(EnumerableTestCase.Cases))]
		public bool TestWithEnumerableValues(IDictionary<string, IEnumerable<string>> pControl, IDictionary<string, IEnumerable<string>> pTest)
		{
			return ComparisonUtilities.CompareDictionaries(nameof(CompareDictionaries), pControl, pTest, nameof(TestWithEnumerableValues));
		}

		public static class EnumerableTestCase
		{
			private static readonly Dictionary<string, IEnumerable<string>> ControlList
			= new Dictionary<string, IEnumerable<string>>
			{
				["One"] = new List<string> { "One" },
				["Two"] = new List<string> { "Two" },
				["Three"] = new List<string> { "Three" },
			};

			public static IEnumerable Cases
			{
				get
				{
					yield return new TestCaseData(null, null).Returns(true)
						.SetName($"{nameof(TestWithEnumerableValues)} | null matches null.");

					yield return new TestCaseData(null, new Dictionary<string, IEnumerable<string>>()).Returns(false)
						.SetName($"{nameof(TestWithEnumerableValues)} | null doesn't match empty Dictionary.");

					yield return new TestCaseData(null, ControlList).Returns(false)
						.SetName($"{nameof(TestWithEnumerableValues)} | null doesn't match Control.");

					yield return new TestCaseData(ControlList, ControlList).Returns(true)
						.SetName($"{nameof(TestWithEnumerableValues)} | Control matches Control.");

					yield return new TestCaseData(ControlList, new Dictionary<string, IEnumerable<string>>()).Returns(false)
						.SetName($"{nameof(TestWithEnumerableValues)} | Control doesn't match empty Dictionary.");

					yield return new TestCaseData(
							ControlList,
							new Dictionary<string, IEnumerable<string>>
							{
								["Four"] = new List<string> { "Four" },
								["Five"] = new List<string> { "Five" },
								["Six"] = new List<string> { "Six" }
							}).Returns(false)
						.SetName($"{nameof(TestWithEnumerableValues)} | Control doesn't match Second Dictionary.");

					yield return new TestCaseData(
							ControlList,
							new Dictionary<string, IEnumerable<string>>
							{
								["One"] = new List<string> { "One" },
								["Two"] = new List<string> { "Two" },
								["Three"] = new List<string> { "Three" }
							}).Returns(true)
						.SetName($"{nameof(TestWithEnumerableValues)} | Control matches equivalent Dictionary.");
				}
			}
		}
	}
}
