using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetHeightMethod
	{
		private const string HEIGHT = "Over 9000";

		private static IRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<IRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void Null()
		{
			var original = (ICharacter) new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.SetHeight(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillRepository);
			var result = original.SetHeight(HEIGHT);

			Assert.AreEqual(HEIGHT, result.Height);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillRepository);

			var result = original.SetHeight(HEIGHT);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillRepository);
			original.SetHeight(HEIGHT);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetHeight(HEIGHT);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new HeightSet(original.Id, 1, HEIGHT), 
					}));
		}
	}
}