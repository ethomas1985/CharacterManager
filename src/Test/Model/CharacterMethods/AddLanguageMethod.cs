using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Linq;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddLanguageMethod
	{
		private readonly Language _language = new Language("Middle Test-ese");
		private static readonly ILegacyRepository<ISkill> SkillRepository = new Mock<ILegacyRepository<ISkill>>().Object;

		[Test]
		public void Null()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.AddLanguage(null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.AddLanguage(_language);

			Assert.IsTrue(result.Languages.Contains(_language));
		}

		[Test]
		public void NoDuplicates()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.AddLanguage(_language).AddLanguage(_language);

			Assert.That(result.Languages.GroupBy(k => k).Count(g => g.Count() > 1), Is.EqualTo(0));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var result = original.AddLanguage(_language);

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(SkillRepository);
			original.AddLanguage(_language);

			Assert.IsFalse(original.Languages.Contains(_language));
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.AddLanguage(new Language("Testing"));

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new LanguageAdded(original.Id, 1, new Language("Testing")),
					}));
		}
	}
}