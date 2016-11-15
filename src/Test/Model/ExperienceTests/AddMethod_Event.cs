using System;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
// ReSharper disable ExpressionIsAlwaysNull

namespace Test.Model.ExperienceTests
{
	[TestFixture]
	public class AddMethod_Event
	{
		[Test]
		public void Fail()
		{
			var original = new Experience();

			IEvent nullEvent = null;
			Assert.Throws<ArgumentNullException>(() => original.Append(nullEvent));
		}

		[Test]
		public void Success()
		{
			var original = new Experience();

			var result = original.Append(new Event("Test", "Test", 10));

			Assert.AreEqual(10, result.Total);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = new Experience();

			var result = original.Append(new Event("Test", "Test", 10));

			Assert.AreNotSame(original, result);
		}

		
		[Test]
		public void Fail_Overload()
		{
			var original = new Experience();

			Assert.Throws<ArgumentNullException>(() => original.Append(null, null, 0));
		}

		[Test]
		public void Success_Overload()
		{
			var original = new Experience();

			var result = original.Append("Test", "Test", 10);

			Assert.AreEqual(10, result.Total);
		}

		[Test]
		public void ReturnsNewInstance_Overload()
		{
			var original = new Experience();

			var result = original.Append(new Event("Test", "Test", 10));

			Assert.AreNotSame(original, result);
		}
	}
}