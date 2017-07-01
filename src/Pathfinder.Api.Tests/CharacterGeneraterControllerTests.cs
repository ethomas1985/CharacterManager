using Moq;
using NUnit.Framework;
using Pathfinder.Api.Controllers;
using Pathfinder.Api.Models;
using Pathfinder.Interface;
using Pathfinder.Model;
using System;
using System.Collections.Generic;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Test;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Api.Tests
{
	[TestFixture]
	public class CharacterGeneraterControllerTests
	{
		private static readonly Lazy<IRepository<IClass>> LazyClassLibrary
			= new Lazy<IRepository<IClass>>(() =>
			{
				IClass iClass;
				var testClass = ClassMother.Level1Neutral();
				var mockClassLibrary = new Mock<IRepository<IClass>>();

				mockClassLibrary.Setup(foo => foo.Values).Returns(new List<IClass> { testClass });
				mockClassLibrary.Setup(foo => foo[testClass.Name]).Returns(testClass);
				mockClassLibrary
					.Setup(foo => foo.TryGetValue(testClass.Name, out iClass))
					.OutCallback((string t, out IClass r) => r = testClass)
					.Returns(true);
				return mockClassLibrary.Object;
			});

		internal static IRepository<IClass> ClassRepository => LazyClassLibrary.Value;

		private static readonly Lazy<IRepository<IRace>> LazyRaceLibrary
			= new Lazy<IRepository<IRace>>(() =>
			{
				IRace race;
				var testRace = RaceMother.Create();
				var mockRaceLibrary = new Mock<IRepository<IRace>>();

				mockRaceLibrary.Setup(foo => foo.Values).Returns(new List<IRace> { testRace });
				mockRaceLibrary.Setup(foo => foo[testRace.Name]).Returns(testRace);
				mockRaceLibrary
					.Setup(foo => foo.TryGetValue(testRace.Name, out race))
					.OutCallback((string t, out IRace r) => r = testRace)
					.Returns(true);
					
				return mockRaceLibrary.Object;
			});

		internal static IRepository<IRace> RaceRepository => LazyRaceLibrary.Value;

		private static readonly Lazy<IRepository<ISkill>> LazySkillLibrary
			= new Lazy<IRepository<ISkill>>(() =>
			{
				ISkill race;
				var testSkill =SkillMother.Create();
				var mockRaceLibrary = new Mock<IRepository<ISkill>>();

				mockRaceLibrary.Setup(foo => foo.Values).Returns(new List<ISkill> { testSkill });
				mockRaceLibrary.Setup(foo => foo[testSkill.Name]).Returns(testSkill);
				mockRaceLibrary
					.Setup(foo => foo.TryGetValue(testSkill.Name, out race))
					.OutCallback((string t, out ISkill r) => r = testSkill)
					.Returns(true);
					
				return mockRaceLibrary.Object;
			});

		internal static IRepository<ISkill> SkillRepository => LazySkillLibrary.Value;

		protected static CharacterGeneratorController createCharacterGeneratorController()
		{
			var mockCharacterLibrary = new Mock<IRepository<ICharacter>>();
			
			var mockFeatLibrary = new Mock<IRepository<IFeat>>();
			var mockItemLibrary = new Mock<IRepository<IItem>>();
			var mockSkillLibrary = new Mock<IRepository<ISkill>>();

			var charGen =
				new CharacterGeneratorController(
					mockCharacterLibrary.Object,
					RaceRepository,
					mockSkillLibrary.Object,
					ClassRepository,
					mockFeatLibrary.Object,
					mockItemLibrary.Object);
			return charGen;
		}
	}
}
