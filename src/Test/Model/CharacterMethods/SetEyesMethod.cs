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
	public class SetEyesMethod
	{
		private const string EYES = "Octarine";

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

			Assert.Throws<ArgumentNullException>(() => original.SetEyes(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillRepository);
			var result = original.SetEyes(EYES);

			Assert.AreEqual(EYES, result.Eyes);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillRepository);

			var result = original.SetEyes(EYES);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillRepository);
			original.SetEyes(EYES);

			Assert.IsNull(original.Name);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetEyes(EYES);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new EyesSet(original.Id, 1, EYES), 
					}));
		}
	}
}