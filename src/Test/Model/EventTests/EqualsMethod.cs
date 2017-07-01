using NUnit.Framework;
using Pathfinder.Model;

namespace Pathfinder.Test.Model.EventTests
{
	[TestFixture]
	public class EqualsMethod
	{
		[Test]
		public void NotNull()
		{
			var original = new ExperienceEvent("Test", "Test", 10);

			Assert.IsFalse(original.Equals(null));
		}

		[Test]
		public void EqualsSelf()
		{
			var original = new ExperienceEvent("Test", "Test", 10);

			Assert.IsTrue(original.Equals(original));
		}

		[Test]
		public void AreEqual()
		{
			var first = new ExperienceEvent("Test", "Test", 10);
			var second = new ExperienceEvent("Test", "Test", 10);

			Assert.IsTrue(first.Equals(second));
		}

		[Test]
		public void DifferentPoints()
		{
			var first = new ExperienceEvent("Test", "Test", 10);
			var second = new ExperienceEvent("Test", "Test", 5);

			Assert.IsFalse(first.Equals(second));
		}

		[Test]
		public void DifferentDescription()
		{
			var first = new ExperienceEvent("Test", "First Test", 10);
			var second = new ExperienceEvent("Test", "Second Test", 10);

			Assert.IsFalse(first.Equals(second));
		}

		[Test]
		public void DifferentName()
		{
			var first = new ExperienceEvent("First Test", "Test", 10);
			var second = new ExperienceEvent("Second Test", "Test", 10);

			Assert.IsFalse(first.Equals(second));
		}
	}
}
