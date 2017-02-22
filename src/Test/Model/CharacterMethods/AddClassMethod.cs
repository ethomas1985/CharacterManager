using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Test.Model.CharacterMethods
{
	[TestFixture]
	public class AddClassMethod
	{
		private const int SKILL_ADDEND = 5;
		private const int HIT_DIE_FACES = 6;

		private static MockClass CreateMockClass()
		{
			return new MockClass
				   {
					   Name = "Mock Class",
					   Alignments = new HashSet<Alignment>
									{
										Alignment.ChaoticGood,
										Alignment.ChaoticNeutral,
										Alignment.ChaoticEvil
									},
					   HitDie = new Die(HIT_DIE_FACES),
					   SkillAddend = SKILL_ADDEND
				   };
		}

		[Test]
		public void NullClass()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<ArgumentNullException>(() => original.AddClass(null));
		}

		[Test]
		public void InvalidLevel()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<Exception>(() => original.AddClass(CreateMockClass(), -1, false, new List<int>()));
		}

		[Test]
		public void NullHitPoints()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			Assert.Throws<Exception>(() => original.AddClass(CreateMockClass(), 1, false, null));
		}

		[Test]
		public void Success()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass);

			Assert.IsTrue(result.Classes.Any(x => x.Class.Equals(mockClass)));
		}

		[Test]
		public void Success_Level()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass);

			Assert.AreEqual(1, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.Level);
		}

		[Test]
		public void Success_IsFavored()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass);

			Assert.AreEqual(true, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.IsFavored);
		}

		[Test]
		public void Success_HitPoints()
		{
			var original = ((ICharacter) new Character(new MockSkillLibrary()))
				.SetConstitution(10);
			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass);

			var expected = mockClass.HitDie.Faces + original.Constitution.Modifier;
			Assert.AreEqual(expected, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.HitPoints.Sum());
		}

		[Test]
		public void Success_MaxSkillRanks()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass);

			var expected = SKILL_ADDEND + original.Intelligence.Modifier;
			Assert.AreEqual(expected, result.MaxSkillRanks);
		}

		[Test]
		public void Success_Overload()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.IsTrue(result.Classes.Any(x => x.Class.Equals(mockClass)));
		}

		[Test]
		public void Success_Overload_Level()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.AreEqual(10, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.Level);
		}

		[Test]
		public void Success_Overload_IsFavored()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.AreEqual(true, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.IsFavored);
		}

		[Test]
		public void Success_Overload_HitPoints()
		{
			var original = ((ICharacter) new Character(new MockSkillLibrary()))
				.SetConstitution(10);

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			Assert.AreEqual(30, result.Classes.FirstOrDefault(x => x.Class.Equals(mockClass))?.HitPoints.Sum());
		}

		[Test]
		public void Success_Overload_MaxSkillRanks()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			var result = original.AddClass(mockClass, 10, true, new List<int> { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 });

			var expected = SKILL_ADDEND + original.Intelligence.Modifier;
			Assert.AreEqual(expected, result.MaxSkillRanks);
		}

		[Test]
		public void ReturnsNewInstance()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var result = original.AddClass(CreateMockClass());

			Assert.AreNotSame(original, result);
		}

		[Test]
		public void OriginalUnchanged()
		{
			var original = (ICharacter) new Character(new MockSkillLibrary());

			var mockClass = CreateMockClass();
			original.AddClass(mockClass);

			Assert.IsFalse(original.Classes.Any(x => x.Class.Equals(mockClass)));
		}
	}
}