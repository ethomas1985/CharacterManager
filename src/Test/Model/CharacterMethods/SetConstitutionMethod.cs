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
	public class SetConstitutionMethod
	{
		private static IRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<IRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void InvalidValue()
		{
			var original = (ICharacter) new Character(SkillRepository);

			Assert.Throws<Exception>(() => original.SetConstitution(-1));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(SkillRepository);
			var result = original.SetConstitution(10);

			Assert.AreEqual(10, result.Constitution.Base);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(SkillRepository);

			var result = original.SetConstitution(10);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(SkillRepository);
			original.SetConstitution(10);

			Assert.AreEqual(0, original.Constitution.Base);
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetConstitution(10);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ConstitutionSet(original.Id, 1, 10, 0, 0), 
					}));
		}
	}
}