using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Serializers.Json;
using Pathfinder.Test.Mocks;
// ReSharper disable LocalizableElement

namespace Pathfinder.Test.Serializers.Json
{
	[SetUpFixture]
	public class SetupTestFixtureForJsonSerializers
	{
		internal static ILibrary<IClass> ClassLibrary { get; } = new MockClassLibrary();

		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			Console.WriteLine($"EXECUTING {nameof(SetupTestFixtureForJsonSerializers)}.{nameof(RunBeforeAnyTests)}");

			JsonConvert.DefaultSettings = GetJsonSerializerSettings;

			JsonSerializerSettings GetJsonSerializerSettings()
			{
				ClassLibrary.Store(CreateTestingClass());

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
							new CharacterJsonSerializer(new MockRaceLibrary(), new MockSkillLibrary()),
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


		public static IClass CreateTestingClass()
		{
			const string className = "Mock Class";
			var classLevel = new ClassLevel(1, new List<int> { 1 }, 1, 1, 1, null);

			return new Class(
				className,
				new HashSet<Alignment> { Alignment.Neutral },
				new Die(6),
				0,
				new HashSet<string>(),
				new IClassLevel[] { classLevel },
				new List<string>());
		}
	}
}
