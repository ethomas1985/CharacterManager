using System.Collections.Generic;
using Moq;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Mocks
{
	public class MockHelper
	{
		public static ILegacyRepository<ISkill> GetSkillRepository()
		{
			var testSkill = SkillMother.Create();
			return GetRepositoryMock(testSkill);
		}

		public static ILegacyRepository<IRace> GetRaceRepository()
		{
			var testRace = RaceMother.Create();
			return GetRepositoryMock(testRace);
		}

		public static ILegacyRepository<IClass> GetClassRepository()
		{
			var testClass = ClassMother.Level1Neutral();
			return GetRepositoryMock(testClass);
		}

		private static ILegacyRepository<T> GetRepositoryMock<T>(T testInstance)
		{
			T outValue;
			var mock = new Mock<ILegacyRepository<T>>();

			mock.Setup(foo => foo.GetEnumerator())
				.Returns(new List<T> { testInstance }.GetEnumerator());

			mock.Setup(foo => foo.Values)
				.Returns(new List<T> { testInstance });

			mock
				.Setup(foo => foo.GetAll())
				.Returns(new List<T> { testInstance });

			mock.Setup(foo => foo[It.IsAny<string>()])
				.Returns(testInstance);

			mock.Setup(foo => foo.GetAll())
				.Returns(new List<T> { testInstance });

			mock
				.Setup(foo => foo.TryGetValue(It.IsAny<string>(), out outValue))
				.OutCallback((string t, out T r) => r = testInstance)
				.Returns(true);

			return mock.Object;
		}
	}
}
