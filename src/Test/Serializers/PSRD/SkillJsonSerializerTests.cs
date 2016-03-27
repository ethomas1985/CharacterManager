using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Serializers.PSRD;
using Test.Serializers.PSRD.TestData;

namespace Test.Serializers.PSRD
{
	[TestFixture]
	public class SkillJsonSerializerTests
	{
		private const string UNIT_TESTING = "Unit Testing";

		private readonly ISkill _skill =
			new Skill(
				UNIT_TESTING,
				AbilityType.Intelligence,
				true,
				true,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING);

		private readonly string _jsonString = TestResources.TestSkill;

		private readonly string _badJsonString =
			$"{{{Environment.NewLine}" +
			$"	\"attribute\": \"Not an Ability\", {Environment.NewLine}" +
			$"}}{Environment.NewLine}";

		[TestFixture]
		public class SerializeMethod : SkillJsonSerializerTests
		{
			[Test]
			public void Expected()
			{
				var serializer = new SkillJsonSerializer();
				Assert.Throws<NotImplementedException>(
					() => serializer.Serialize(_skill));

				//Assert.AreEqual(_jsonString, xml);
			}
		}

		[TestFixture]
		public class DeserializeMethodTests : SkillJsonSerializerTests
		{
			public Regex NormalizeSpace { get; } = new Regex(@"\s+", RegexOptions.Compiled);

			[Test]
			public void ThrowsForNullString()
			{
				var serializer = new SkillJsonSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(null));
			}

			[Test]
			public void ThrowsForEmptyString()
			{
				var serializer = new SkillJsonSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(string.Empty));
			}

			[Test]
			public void NotNull()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.NotNull(result);
			}

			[Test]
			public void BadAbilityScore()
			{
				var serializer = new SkillJsonSerializer();

				Assert.Throws<JsonException>(
					() => serializer.Deserialize(_badJsonString));

			}

			[Test]
			public void SetsTrainedOnly()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.IsTrue(result.TrainedOnly);
			}

			[Test]
			public void SetsArmorCheckPenalty()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.IsTrue(result.ArmorCheckPenalty);
			}

			[Test]
			public void SetsDescription()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.AreEqual("This is the Description Field", result.Description);
			}

			[Test]
			public void SetsCheck()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				var expected =
					NormalizeSpace.Replace(TestResources.Expected_Checks, string.Empty);

				var actual =
					NormalizeSpace.Replace(result.Check, string.Empty);

				Assert.AreEqual(expected, actual);
			}

			[Test]
			public void SetsAction()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.AreEqual("This is the Action field.", result.Action);
			}

			[Test]
			public void SetsTryAgain()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.AreEqual("This is the Try Again Field", result.TryAgain);
			}

			[Test]
			public void SetsSpecial()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.AreEqual("This is the Special field", result.Special);
			}

			[Test]
			public void SetsRestriction()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.AreEqual("This is the Restriction field", result.Restriction);
			}

			[Test]
			public void SetsUntrained()
			{
				var serializer = new SkillJsonSerializer();
				var result = serializer.Deserialize(_jsonString);

				Assert.AreEqual("This is the Untrained field", result.Untrained);
			}
		}
	}
}
