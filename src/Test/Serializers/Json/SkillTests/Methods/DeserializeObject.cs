using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;

namespace Pathfinder.Test.Serializers.Json.SkillTests.Methods
{
	[TestFixture]
	public class DeserializeObject
	{
		[Test]
		public void RequiresName()
		{
			Assert.That(
				() => JsonConvert.DeserializeObject<ISkill>("{}"),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(ISkill.Name)}"));
		}

		[Test]
		public void RequiresAbilityType()
		{
			const string name = "Testing Skill";
			var value = $"{{ \"{nameof(ISkill.Name)}\": \"{name}\"}}";
			Assert.That(
				() => JsonConvert.DeserializeObject<ISkill>(value),
				Throws.Exception
					.TypeOf<JsonException>()
					.With.Message.EqualTo($"Missing Required Attribute: {nameof(ISkill.AbilityType)}"));
		}

		[Test]
		public void Expected()
		{
			const string name = "Testing Skill";
			const AbilityType abilityType = AbilityType.Charisma;
			const bool trainedOnly = true;
			const bool armorCheckPenalty = true;
			const string description = "Testing Skill Description";
			const string check = "Testing Skill Check";
			const string action = "Testing Skill Action";
			const string tryAgain = "Testing Skill Try Again";
			const string special = "Testing Skill Special";
			const string restriction = "Testing Skill Restriction";
			const string untrained = "Testing Skill Untrained";

			var value = $"{{" +
						$"\"{nameof(ISkill.Name)}\": \"{name}\"," +
						$"\"{nameof(ISkill.AbilityType)}\": \"{abilityType}\"," +
						$"\"{nameof(ISkill.TrainedOnly)}\": \"{trainedOnly}\"," +
						$"\"{nameof(ISkill.ArmorCheckPenalty)}\": \"{armorCheckPenalty}\"," +
						$"\"{nameof(ISkill.Description)}\": \"{description}\"," +
						$"\"{nameof(ISkill.Check)}\": \"{check}\"," +
						$"\"{nameof(ISkill.Action)}\": \"{action}\"," +
						$"\"{nameof(ISkill.TryAgain)}\": \"{tryAgain}\"," +
						$"\"{nameof(ISkill.Special)}\": \"{special}\"," +
						$"\"{nameof(ISkill.Restriction)}\": \"{restriction}\"," +
						$"\"{nameof(ISkill.Untrained)}\": \"{untrained}\"" +
						$"}}";

			var result = JsonConvert.DeserializeObject<ISkill>(value);
			var expected = new Skill(name, abilityType, true, true, description, check, action, tryAgain, special, restriction, untrained);

			Assert.That(
				result,
				Is.EqualTo(expected));
		}
	}
}
