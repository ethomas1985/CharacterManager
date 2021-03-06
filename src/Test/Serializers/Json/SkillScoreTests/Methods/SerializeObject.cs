﻿using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.SkillScoreTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		private static ILegacyRepository<ISkill> SkillRepository => SetupTestFixtureForJsonSerializers.SkillRepository;

		[Test]
		public void Success()
		{
			var skill = SkillRepository.Values.First();
			var skillScore =
				new SkillScore(skill, new AbilityScore(skill.AbilityType, 10), 1, 1, 1, 1, 1);

			Assert.That(
				() => JsonConvert.SerializeObject(skillScore),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var skill = SkillRepository.Values.First();
			var skillScore =
				new SkillScore(skill, new AbilityScore(skill.AbilityType, 10), 1, 1, 1, 1, 1);
			var actual = JsonConvert.SerializeObject(skillScore);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(ISkillScore.Skill)}\":\"{skillScore.Skill.Name}\",")
					.Append($"\"{nameof(ISkillScore.Ability)}\":\"{skillScore.Ability.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(ISkillScore.AbilityModifier)}\":{skillScore.AbilityModifier},")
					.Append($"\"{nameof(ISkillScore.Ranks)}\":{skillScore.Ranks},")
					.Append($"\"{nameof(ISkillScore.ClassModifier)}\":{skillScore.ClassModifier},")
					.Append($"\"{nameof(ISkillScore.MiscModifier)}\":{skillScore.MiscModifier},")
					.Append($"\"{nameof(ISkillScore.ArmorClassPenalty)}\":{skillScore.ArmorClassPenalty}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
