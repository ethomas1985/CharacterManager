using System;
using System.Linq;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;

// ReSharper disable ExpressionIsAlwaysNull

namespace Pathfinder.Test.Model.ExperienceTests
{
	[TestFixture]
	public class AddMethod_Experience
	{
		[Test]
		public void Fail()
		{
			var original = new Experience();

			IExperience experience = null;
			Assert.Throws<ArgumentNullException>(() => original.Append(experience));
		}

		[Test]
		public void Success()
		{
			var original = new Experience();

			var experience =
				new Experience()
					.Append(new Event("Test 1", "Test 1", 10))
					.Append(new Event("Test 2", "Test 2", 10))
					.Append(new Event("Test 3", "Test 3", 10));
			var result = original.Append(experience);

			Assert.AreEqual(30, result.Total);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = new Experience();

			var experience =
				new Experience()
					.Append(new Event("Test 1", "Test 1", 10))
					.Append(new Event("Test 2", "Test 2", 10))
					.Append(new Event("Test 3", "Test 3", 10));
			var result = original.Append(experience);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchangedWithEmptyExperience()
		{
			var original = new Experience();

			var experience =
				new Experience()
					.Append(new Event("Test 1", "Test 1", 10))
					.Append(new Event("Test 2", "Test 2", 10))
					.Append(new Event("Test 3", "Test 3", 10));
			var result = original.Append(experience);

			Assert.AreEqual(0, original.Count());
		}
	}
}