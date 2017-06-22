using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Assert = NUnit.Framework.Assert;
using Pathfinder.Utilities;

namespace Pathfinder.Test.Serializers.Json.SkillTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		private static ILibrary<ISkill> SkillLibrary => SetupTestFixtureForJsonSerializers.SkillLibrary;

		[Test]
		public void Success()
		{
			var skill = SkillLibrary.Values.First();

			Assert.That(
					    () => JsonConvert.SerializeObject(skill),
					    Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var skill = SkillLibrary.Values.First();
			var actual = JsonConvert.SerializeObject(skill);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(ISkill.Name)}\":\"{skill.Name}\",")
					.Append($"\"{nameof(ISkill.AbilityType)}\":\"{skill.AbilityType.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(ISkill.TrainedOnly)}\":{skill.TrainedOnly.ToString().ToCamelCase()},")
					.Append($"\"{nameof(ISkill.ArmorCheckPenalty)}\":{skill.ArmorCheckPenalty.ToString().ToCamelCase()},")
					.Append($"\"{nameof(ISkill.Description)}\":\"{skill.Description}\",")
					.Append($"\"{nameof(ISkill.Check)}\":\"{skill.Check}\",")
					.Append($"\"{nameof(ISkill.Action)}\":\"{skill.Action}\",")
					.Append($"\"{nameof(ISkill.TryAgain)}\":\"{skill.TryAgain}\",")
					.Append($"\"{nameof(ISkill.Special)}\":\"{skill.Special}\",")
					.Append($"\"{nameof(ISkill.Restriction)}\":\"{skill.Restriction}\",")
					.Append($"\"{nameof(ISkill.Untrained)}\":\"{skill.Untrained}\"")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
