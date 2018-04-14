using NUnit.Framework;
using Pathfinder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Pathfinder.Events.Character;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddClassMethod
	{
		private static readonly Lazy<ILegacyRepository<ISkill>> LazySkillLibrary
			= new Lazy<ILegacyRepository<ISkill>>(() =>
			{
				ISkill race;
				var testSkill = SkillMother.Create();
				var mockRaceLibrary = new Mock<ILegacyRepository<ISkill>>();

				mockRaceLibrary.Setup(foo => foo.Values).Returns(new List<ISkill> { testSkill });
				mockRaceLibrary.Setup(foo => foo[testSkill.Name]).Returns(testSkill);
				mockRaceLibrary
					.Setup(foo => foo.TryGetValue(testSkill.Name, out race))
					.OutCallback((string t, out ISkill r) => r = testSkill)
					.Returns(true);

				return mockRaceLibrary.Object;
			});

		internal static ILegacyRepository<ISkill> SkillRepository => LazySkillLibrary.Value;

		[Test]
		public void NullClass()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<ArgumentNullException>(() => original.AddClass(null));
		}

		[Test]
		public void InvalidLevel()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<Exception>(() => original.AddClass(ClassMother.Chaotic(), -1, false, new List<int>()));
		}

		[Test]
		public void NullHitPoints()
		{
			var original = (ICharacter)new Character(SkillRepository);

			Assert.Throws<Exception>(() => original.AddClass(ClassMother.Chaotic(), 1, false, null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass);

			Assert.IsTrue(result.Classes.Any(x => x.Class.Equals(mockClass)));
		}

		[Test]
		public void Success_Level()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass);

			Assert.AreEqual(1, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.Level);
		}

		[Test]
		public void Success_IsFavored()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass);

			Assert.AreEqual(true, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.IsFavored);
		}

		[Test]
		public void Success_HitPoints()
		{
			var original = ((ICharacter)new Character(SkillRepository))
				.SetConstitution(10);
			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass);

			var expected = mockClass.HitDie.Faces + original.Constitution.Modifier;
			Assert.AreEqual(expected, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.HitPoints.Sum());
		}

		[Test]
		public void Success_MaxSkillRanks()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass);

			var expected = mockClass.SkillAddend + original.Intelligence.Modifier;
			Assert.AreEqual(expected, result.MaxSkillRanks);
		}

		[Test]
		public void Success_Overload()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.IsTrue(result.Classes.Any(x => x.Class.Equals(mockClass)));
		}

		[Test]
		public void Success_Overload_Level()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.AreEqual(10, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.Level);
		}

		[Test]
		public void Success_Overload_IsFavored()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.AreEqual(true, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.IsFavored);
		}

		[Test]
		public void Success_Overload_HitPoints()
		{
			var original = ((ICharacter)new Character(SkillRepository))
				.SetConstitution(10);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.AreEqual(30, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.HitPoints.Sum());
		}

		[Test]
		public void Success_Overload_MaxSkillRanks()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			var expected = mockClass.SkillAddend + original.Intelligence.Modifier;
			Assert.AreEqual(expected, result.MaxSkillRanks);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var result = original.AddClass(ClassMother.Chaotic());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter)new Character(SkillRepository);

			var mockClass = ClassMother.Chaotic();
			original.AddClass(mockClass);

			Assert.IsFalse(original.Classes.Any(x => x.Class.Equals(mockClass)));
		}

		[Test]
		public void HasPendingEvents()
		{
			var original = (ICharacter)new Character(SkillRepository);
			var mockClass = ClassMother.Chaotic();
			var result = original.AddClass(mockClass);

			Assert.That(
				result.GetPendingEvents(),
				Is.EquivalentTo(
					new IEvent[]
					{
						new CharacterCreated(original.Id),
						new ClassAdded(original.Id, 1, new CharacterClass(mockClass, 1, true, new [] { mockClass.HitDie.Faces })),
					}));
		}
	}
}