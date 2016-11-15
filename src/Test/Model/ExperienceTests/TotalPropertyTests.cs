using System.Collections;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Test.Model.ExperienceTests
{
	[TestFixture]
	public class TotalPropertyTests
	{
		[Test]
		[TestCaseSource(typeof(ExperienceTestsCase), nameof(ExperienceTestsCase.Cases))]
		public int Getter(IExperience pInput)
		{
			return pInput.Total;
		}
	}

	public static class ExperienceTestsCase
	{
		public static IEnumerable Cases
		{
			get
			{
				IExperience xp = new Experience();
				yield return new TestCaseData(xp).Returns(0).SetName("0 Experience");

				xp = xp.Append("Test 1", "Test 1", 10);
				yield return new TestCaseData(xp).Returns(10).SetName("10 Experience");

				xp = xp.Append("Test 2", "Test 3", 100);
				yield return new TestCaseData(xp).Returns(110).SetName("110 Experience");
			}
		}
	}
}
