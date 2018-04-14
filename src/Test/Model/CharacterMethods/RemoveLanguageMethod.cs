using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class RemoveLanguageMethod
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
		public void Null()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.RemoveLanguage(null));
		}

		[Test]
		public void Success()
		{
			var language = LanguageMother.MiddleTestese();
			var original = new Character(SkillRepository).AddLanguage(language);
			Assert.IsTrue(original.Languages.Contains(language));

			var result = original.RemoveLanguage(language);

			Assert.IsFalse(result.Languages.Contains(language));
		}

		[Test]
		public void DoesNotRemoveRacialLanguages()
		{
			var race = RaceMother.Create();
			var racialLanguage = LanguageMother.OldTestese();

			var original = new Character(SkillRepository).SetRace(race);

			var result = original.RemoveLanguage(racialLanguage);

			Assert.IsTrue(result.Languages.Contains(racialLanguage));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var language = LanguageMother.MiddleTestese();
			var original = new Character(SkillRepository).AddLanguage(language);
			Assert.IsTrue(original.Languages.Contains(language));

			var result = original.RemoveLanguage(language);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var language = LanguageMother.MiddleTestese();
			var original = new Character(SkillRepository).AddLanguage(language);
			Assert.IsTrue(original.Languages.Contains(language));

			original.RemoveLanguage(language);

			Assert.IsTrue(original.Languages.Contains(language));
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.RemoveLanguage(new Language("Testing"));

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new LanguageRemoved(original.Id, 1, new Language("Testing")),
					}));
		}
	}
}