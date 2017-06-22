using System;
using System.Collections.Generic;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Serializers.Json;
using Pathfinder.Test.ObjectMothers;

// ReSharper disable LocalizableElement

namespace Pathfinder.Test.Serializers.Json
{
	[SetUpFixture]
	public class SetupTestFixtureForJsonSerializers
	{
		private static readonly Lazy<ILibrary<IClass>> LazyClassLibrary
			= new Lazy<ILibrary<IClass>>(() =>
			{
				var testClass = ClassMother.Create();
				var mockClassLibrary = new Mock<ILibrary<IClass>>();

				mockClassLibrary.Setup(foo => foo.Values).Returns(new List<IClass> { testClass });
				mockClassLibrary.Setup(foo => foo[It.IsAny<string>()]).Returns(testClass);

				return mockClassLibrary.Object;
			});

		public static ILibrary<IClass> ClassLibrary => LazyClassLibrary.Value;

		private static readonly Lazy<ILibrary<IRace>> LazyRaceLibrary
			= new Lazy<ILibrary<IRace>>(() =>
				{
					IRace race;
					var testRace = RaceMother.Create();
					var mockRaceLibrary = new Mock<ILibrary<IRace>>();

					mockRaceLibrary.Setup(foo => foo.Values).Returns(new List<IRace> { testRace });
					mockRaceLibrary.Setup(foo => foo[It.IsAny<string>()]).Returns(testRace);
					mockRaceLibrary
						.Setup(foo => foo.TryGetValue(It.IsAny<string>(), out race))
						.OutCallback((string t, out IRace r) => r = testRace)
						.Returns(true);

					return mockRaceLibrary.Object;
				});

		public static ILibrary<IRace> RaceLibrary => LazyRaceLibrary.Value;

		private static readonly Lazy<ILibrary<ISkill>> LazySkillLibrary
			= new Lazy<ILibrary<ISkill>>(() =>
				{
					ISkill race;
					var testSkill = SkillMother.Create();
					var mockSkillLibrary = new Mock<ILibrary<ISkill>>();

					var values = new List<ISkill> { testSkill };
					
					mockSkillLibrary.Setup(moq => moq.GetEnumerator()).Returns(() => values.GetEnumerator());
					mockSkillLibrary.Setup(moq => moq.Values).Returns(values);
					mockSkillLibrary.Setup(moq => moq[It.IsAny<string>()]).Returns(testSkill);
					mockSkillLibrary
						.Setup(moq => moq.TryGetValue(It.IsAny<string>(), out race))
						.OutCallback((string t, out ISkill r) => r = testSkill)
						.Returns(true);

					return mockSkillLibrary.Object;
				});

		public static ILibrary<ISkill> SkillLibrary => LazySkillLibrary.Value;

		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			Console.WriteLine($"EXECUTING {nameof(SetupTestFixtureForJsonSerializers)}.{nameof(RunBeforeAnyTests)}");

			JsonConvert.DefaultSettings = GetJsonSerializerSettings;

			JsonSerializerSettings GetJsonSerializerSettings()
			{
				return new JsonSerializerSettings
				{
					Converters =
						new List<JsonConverter>
						{
							new StringEnumConverter { CamelCaseText = true },
							new BooleanJsonConverter(),
							new AbilityScoreJsonSerializer(),
							new AbilityTypeJsonSerializer(),
							new ArmorComponentJsonSerializer(),
							new CharacterClassJsonSerializer(ClassLibrary),
							new CharacterJsonSerializer(RaceLibrary, SkillLibrary),
							new ClassJsonSerializer(),
							new ClassLevelJsonSerializer(),
							new CurrencyJsonSerializer(),
							new DefenseScoreJsonSerializer(),
							new DiceJsonSerializer(),
							new DieJsonSerializer(),
							new EventJsonSerializer(),
							new ExperienceJsonSerializer(),
							new FeatJsonSerializer(),
							new FeatureJsonSerializer(),
							new InventoryJsonSerializer(),
							new ItemJsonSerializer(),
							new LanguageJsonSerializer(),
							new OffensiveScoreJsonSerializer(),
							new PurseJsonSerializer(),
							new RaceJsonSerializer(),
							new SavingThrowJsonSerializer(),
							new SkillJsonSerializer(),
							new SkillScoreJsonSerializer(),
							new SpellJsonSerializer(),
							new SubFeatureJsonSerializer(),
							new SpellComponentJsonSerializer(),
							new TraitJsonSerializer(),
							new WeaponComponentJsonSerializer(),
							new WeaponSpecialJsonSerializer(),
						},
					//StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
					//Formatting = Formatting.Indented,
				};
			}
		}
	}
}
