using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Library;
using Pathfinder.Model;
using Pathfinder.Serializers.Xml;

namespace Pathfinder.Test.Serializers.Xml
{
	[TestFixture]
	public abstract class RaceXmlSerializerTests
	{
		const string RACE_NAME = "Race";
		const string DESCRIPTION = "Description";
		const int BASE_SPEED = 15;

		private readonly Race _race =
			new Race(
				RACE_NAME,
				RACE_NAME,
				DESCRIPTION,
				Size.Gargantuan,
				BASE_SPEED,
				new Dictionary<AbilityType, int>
				{
					[AbilityType.Strength] = 2,
					[AbilityType.Dexterity] = 2,
					[AbilityType.Constitution] = 2,
					[AbilityType.Intelligence] = 2,
					[AbilityType.Wisdom] = 2,
					[AbilityType.Charisma] = 2
				},
				new List<ITrait>
				{
					new Trait("Slow and Steady", null, false, null)
				},
				new List<ILanguage>
				{
					new Language("English")
				});

		readonly string _xmlString =
			//$"<?xml version=\"1.0\" encoding=\"utf-8\" ?>{Environment.NewLine}"+
			$"<Race>{Environment.NewLine}"+
			$"  <Name>{RACE_NAME}</Name>{Environment.NewLine}"+
			$"  <Adjective>{RACE_NAME}</Adjective>{Environment.NewLine}" +
			$"  <Description>{DESCRIPTION}</Description>{Environment.NewLine}"+
			$"  <Size>{Size.Gargantuan}</Size>{Environment.NewLine}"+
			$"  <BaseSpeed>{BASE_SPEED}</BaseSpeed>{Environment.NewLine}"+
			$"  <AbilityScores>{Environment.NewLine}"+
			$"    <{AbilityType.Strength}>2</{AbilityType.Strength}>{Environment.NewLine}"+
			$"    <{AbilityType.Dexterity}>2</{AbilityType.Dexterity}>{Environment.NewLine}"+
			$"    <{AbilityType.Constitution}>2</{AbilityType.Constitution}>{Environment.NewLine}"+
			$"    <{AbilityType.Intelligence}>2</{AbilityType.Intelligence}>{Environment.NewLine}"+
			$"    <{AbilityType.Wisdom}>2</{AbilityType.Wisdom}>{Environment.NewLine}"+
			$"    <{AbilityType.Charisma}>2</{AbilityType.Charisma}>{Environment.NewLine}"+
			$"  </AbilityScores>{Environment.NewLine}"+
			$"  <Traits>{Environment.NewLine}"+
			$"    <Trait>Slow and Steady</Trait>{Environment.NewLine}"+
			$"  </Traits>{Environment.NewLine}"+
			$"  <Languages>{Environment.NewLine}"+
			$"    <Language>English</Language>{Environment.NewLine}"+
			$"  </Languages>{Environment.NewLine}"+
			 "</Race>";

		[TestFixture]
		public class SerializeMethod : RaceXmlSerializerTests
		{
			[Test]
			public void Expected()
			{
				var traitLibrary = new TraitRepository(new TraitXmlSerializer(), "../../../../resources/Traits/");
				var serializer = new RaceXmlSerializer(traitLibrary);
				var xml = serializer.Serialize(_race);

				Assert.AreEqual(_xmlString, xml);
			}
		}

		[TestFixture]
		public class DeserializeMethod : RaceXmlSerializerTests
		{
			private readonly TraitRepository _traitRepository =
				new TraitRepository(
					new TraitXmlSerializer(),
					Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, "../../../../resources/Traits/")));

			[Test]
			public void ThrowsForNullString()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(null));
			}

			[Test]
			public void ThrowsForEmptyString()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(string.Empty));
			}

			[Test]
			public void NotNull()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.NotNull(result);
			}

			[Test]
			public void SetName()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(RACE_NAME, result.Name);
			}

			[Test]
			public void SetDescription()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(DESCRIPTION, result.Description);
			}

			[Test]
			public void SetSize()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(Size.Gargantuan, result.Size);
			}

			[Test]
			public void SetBaseSpeed()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(BASE_SPEED, result.BaseSpeed);
			}

			[Test]
			public void SetAbilityScores()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(6, result.AbilityScores.Count);
			}

			[Test]
			public void SetAbilityStrength()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Strength]);
			}

			[Test]
			public void SetAbilityDexterity()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Dexterity]);
			}

			[Test]
			public void SetAbilityConstitution()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Constitution]);
			}

			[Test]
			public void SetAbilityIntelligence()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Intelligence]);
			}

			[Test]
			public void SetAbilityWisdom()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Wisdom]);
			}

			[Test]
			public void SetAbilityCharisma()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Charisma]);
			}

			[Test]
			public void SetTraits()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(1, result.Traits.Count());
			}

			[Test]
			public void SetLanguages()
			{
				var serializer = new RaceXmlSerializer(_traitRepository);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(1, result.Languages.Count());
			}
		}
	}
}
