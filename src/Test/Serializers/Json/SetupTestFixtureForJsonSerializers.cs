using System;
using System.Collections.Generic;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Serializers.Json;
using Pathfinder.Test.Mocks;
using Pathfinder.Test.ObjectMothers;

// ReSharper disable LocalizableElement

namespace Pathfinder.Test.Serializers.Json
{
	[SetUpFixture]
	public class SetupTestFixtureForJsonSerializers
	{
		private static readonly Lazy<ILegacyRepository<IClass>> LazyClassLibrary
			= new Lazy<ILegacyRepository<IClass>>(MockHelper.GetClassRepository);

		public static ILegacyRepository<IClass> ClassRepository => LazyClassLibrary.Value;

		private static readonly Lazy<ILegacyRepository<IRace>> LazyRaceLibrary
			= new Lazy<ILegacyRepository<IRace>>(MockHelper.GetRaceRepository);

		public static ILegacyRepository<IRace> RaceRepository => LazyRaceLibrary.Value;

		private static readonly Lazy<ILegacyRepository<ISkill>> LazySkillLibrary
			= new Lazy<ILegacyRepository<ISkill>>(MockHelper.GetSkillRepository);

		public static ILegacyRepository<ISkill> SkillRepository => LazySkillLibrary.Value;

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
							new AbilityScoreJsonSerializer(),
							new AbilityTypeJsonSerializer(),
							new ArmorComponentJsonSerializer(),
							new CharacterClassJsonSerializer(ClassRepository),
							new CharacterJsonSerializer(RaceRepository, SkillRepository),
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
