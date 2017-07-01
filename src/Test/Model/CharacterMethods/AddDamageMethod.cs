using Moq;
using NUnit.Framework;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddDamageMethod
	{
		private static readonly IRepository<ISkill> SkillRepository = new Mock<IRepository<ISkill>>().Object;

		[Test]
		public void TakesDamage()
		{
			var original = new Character(SkillRepository).SetDamage(10);

			var result = original.AddDamage(5);

			Assert.That(result.Damage, Is.EqualTo(15));
		}

		[Test]
		public void HealsDamage()
		{
			var original = new Character(SkillRepository).SetDamage(10);

			var result = original.AddDamage(-5);

			Assert.That(result.Damage, Is.EqualTo(5));
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = new Character(SkillRepository).SetDamage(10);

			var result = original.AddDamage(-1);

			Assert.That(result, Is.Not.SameAs(original));
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = new Character(SkillRepository).SetDamage(10);
			original.AddDamage(-1);

			Assert.That(original.Damage, Is.EqualTo(10));
		}

		[Test]
		public void HasPendingDamageEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.AddDamage(1);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new DamageTaken(original.Id, 1, 1),
					}));
		}

		[Test]
		public void HasPendingHealedEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var result = original.AddDamage(-1);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new DamageHealed(original.Id, 1, 1),
					}));
		}
	}
}