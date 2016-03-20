using System;
using System.Xml;
using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Serializers;

namespace Test.Serializers
{
	[TestFixture]
	public class SkillSerializerTests
	{
		private const string UNIT_TESTING = "Unit Testing";

		private readonly ISkill _skill =
			new Skill(
				UNIT_TESTING,
				AbilityType.Intelligence,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING,
				UNIT_TESTING);

		private readonly string _xmlString =
			$"<Skill>{Environment.NewLine}" +
			$"  <Name>{UNIT_TESTING}</Name>{Environment.NewLine}" +
			$"  <AbilityType>{AbilityType.Intelligence}</AbilityType>{Environment.NewLine}" +
			$"  <KeyAbility>{UNIT_TESTING}</KeyAbility>{Environment.NewLine}" +
			$"  <TrainedOnly>{UNIT_TESTING}</TrainedOnly>{Environment.NewLine}" +
			$"  <ArmorCheckPenalty>{UNIT_TESTING}</ArmorCheckPenalty>{Environment.NewLine}" +
			$"  <Description>{UNIT_TESTING}</Description>{Environment.NewLine}" +
			$"  <Check>{UNIT_TESTING}</Check>{Environment.NewLine}" +
			$"  <Action>{UNIT_TESTING}</Action>{Environment.NewLine}" +
			$"  <TryAgain>{UNIT_TESTING}</TryAgain>{Environment.NewLine}" +
			$"  <Special>{UNIT_TESTING}</Special>{Environment.NewLine}" +
			$"  <Restriction>{UNIT_TESTING}</Restriction>{Environment.NewLine}" +
			$"  <Untrained>{UNIT_TESTING}</Untrained>{Environment.NewLine}" +
			$"</Skill>";

		private readonly string _badXmlString =
			$"<Skill>{Environment.NewLine}" +
			$"  <AbilityType>Not an Ability</AbilityType>{Environment.NewLine}" +
			$"</Skill>";

		[TestFixture]
		public class SerializeMethod : SkillSerializerTests
		{
			[Test]
			public void Expected()
			{
				var serializer = new SkillSerializer();
				var xml = serializer.Serialize(_skill);

				Assert.AreEqual(_xmlString, xml);
			}
		}

		[TestFixture]
		public class DeserializeMethodTests : SkillSerializerTests
		{
			[Test]
			public void ThrowsForNullString()
			{
				var serializer = new SkillSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(null));
			}

			[Test]
			public void ThrowsForEmptyString()
			{
				var serializer = new SkillSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(string.Empty));
			}

			[Test]
			public void NotNull()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.NotNull(result);
			}

			[Test]
			public void BadAbilityScore()
			{
				var serializer = new SkillSerializer();
				var xml = serializer.Serialize(_skill);

				Assert.Throws<XmlException>(
					() => serializer.Deserialize(_badXmlString));

			}

			[Test]
			public void SetsKeyAbility()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.KeyAbility);
			}

			[Test]
			public void SetsTrainedOnly()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.TrainedOnly);
			}

			[Test]
			public void SetsArmorCheckPenalty()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.ArmorCheckPenalty);
			}

			[Test]
			public void SetsDescription()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Description);
			}

			[Test]
			public void SetsCheck()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Check);
			}

			[Test]
			public void SetsAction()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Action);
			}

			[Test]
			public void SetsTryAgain()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.TryAgain);
			}

			[Test]
			public void SetsSpecial()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Special);
			}

			[Test]
			public void SetsRestriction()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Restriction);
			}

			[Test]
			public void SetsUntrained()
			{
				var serializer = new SkillSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Untrained);
			}
		}
	}
}
