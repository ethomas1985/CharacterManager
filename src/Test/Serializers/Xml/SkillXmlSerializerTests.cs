using System;
using System.Xml;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Serializers.Xml;

namespace Pathfinder.Test.Serializers.Xml
{
	[TestFixture]
	public class SkillXmlSerializerTests
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

		private readonly string _xmlString =
			$"<Skill>{Environment.NewLine}" +
			$"  <Name>{UNIT_TESTING}</Name>{Environment.NewLine}" +
			$"  <AbilityType>{AbilityType.Intelligence}</AbilityType>{Environment.NewLine}" +
			$"  <TrainedOnly>{true.ToString().ToLower()}</TrainedOnly>{Environment.NewLine}" +
			$"  <ArmorCheckPenalty>{true.ToString().ToLower()}</ArmorCheckPenalty>{Environment.NewLine}" +
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
		public class SerializeMethod : SkillXmlSerializerTests
		{
			[Test]
			public void Expected()
			{
				var serializer = new SkillXmlSerializer();
				var xml = serializer.Serialize(_skill);

				Assert.AreEqual(_xmlString, xml);
			}
		}

		[TestFixture]
		public class DeserializeMethodTests : SkillXmlSerializerTests
		{
			[Test]
			public void ThrowsForNullString()
			{
				var serializer = new SkillXmlSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(null));
			}

			[Test]
			public void ThrowsForEmptyString()
			{
				var serializer = new SkillXmlSerializer();

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(string.Empty));
			}

			[Test]
			public void NotNull()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.NotNull(result);
			}

			[Test]
			public void BadAbilityScore()
			{
				var serializer = new SkillXmlSerializer();

				Assert.Throws<XmlException>(
					() => serializer.Deserialize(_badXmlString));

			}

			[Test]
			public void SetsTrainedOnly()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.IsTrue(result.TrainedOnly);
			}

			[Test]
			public void SetsArmorCheckPenalty()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.IsTrue(result.ArmorCheckPenalty);
			}

			[Test]
			public void SetsDescription()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Description);
			}

			[Test]
			public void SetsCheck()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Check);
			}

			[Test]
			public void SetsAction()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Action);
			}

			[Test]
			public void SetsTryAgain()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.TryAgain);
			}

			[Test]
			public void SetsSpecial()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Special);
			}

			[Test]
			public void SetsRestriction()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Restriction);
			}

			[Test]
			public void SetsUntrained()
			{
				var serializer = new SkillXmlSerializer();
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(UNIT_TESTING, result.Untrained);
			}
		}
	}
}
