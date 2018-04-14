using NUnit.Framework;
using Pathfinder.Model;
using System;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class SetAgeMethod
	{
		private static ILegacyRepository<ISkill> SkillRepository
		{
			get
			{
				var mockSkillLibrary = new Mock<ILegacyRepository<ISkill>>();

				return mockSkillLibrary.Object;
			}
		}

		[Test]
		public void Negative()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.That(() => original.SetAge(-1), Throws.Exception.TypeOf<Exception>());
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var result = original.SetAge(30);

			Assert.That(result.Age, Is.EqualTo(30));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var result = original.SetAge(30);

			Assert.That(result, Is.Not.SameAs(original));
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(SkillRepository);
			original.SetAge(30);

			Assert.That(original.Age, Is.Not.EqualTo(30));
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.SetAge(30);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new AgeSet(original.Id, 1, 30)
					}));
		}
	}
}