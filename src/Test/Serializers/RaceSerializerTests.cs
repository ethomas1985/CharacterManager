using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Interface;
using Pathfinder.Library;
using Pathfinder.Serializers;
using Pathfinder.Model;
using Pathfinder.Properties;

namespace Test.Serializers
{
	[TestFixture]
	public class RaceSerializerTests
	{
		const string RACE_NAME = "Race";
		const string DESCRIPTION = "Description";
		const int BASE_SPEED = 15;

		private readonly Race _race =
			new Race(
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
					new Trait("Slow and Steady", null, null)
				},
				new List<ILanguage>
				{
					new Language("English")
				});

		readonly string _xmlString =
			//$"<?xml version=\"1.0\" encoding=\"utf-8\" ?>{Environment.NewLine}"+
			$"<Race>{Environment.NewLine}"+
			$"  <Name>{RACE_NAME}</Name>{Environment.NewLine}"+
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
		public class SerializeMethod : RaceSerializerTests
		{
			[Test]
			public void Expected()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var xml = serializer.Serialize(_race);

				Assert.AreEqual(_xmlString, xml);
			}
		}

		[TestFixture]
		public class DeserializeMethod : RaceSerializerTests
		{
			[Test]
			public void ThrowsForNullString()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(null));
			}

			[Test]
			public void ThrowsForEmptyString()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);

				Assert.Throws<ArgumentNullException>(() => serializer.Deserialize(string.Empty));
			}

			[Test]
			public void NotNull()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.NotNull(result);
			}

			[Test]
			public void SetName()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(RACE_NAME, result.Name);
			}

			[Test]
			public void SetDescription()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(DESCRIPTION, result.Description);
			}

			[Test]
			public void SetSize()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(Size.Gargantuan, result.Size);
			}

			[Test]
			public void SetBaseSpeed()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(BASE_SPEED, result.BaseSpeed);
			}

			[Test]
			public void SetAbilityScores()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(6, result.AbilityScores.Count);
			}

			[Test]
			public void SetAbilityStrength()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Strength]);
			}

			[Test]
			public void SetAbilityDexterity()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Dexterity]);
			}

			[Test]
			public void SetAbilityConstitution()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Constitution]);
			}

			[Test]
			public void SetAbilityIntelligence()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Intelligence]);
			}

			[Test]
			public void SetAbilityWisdom()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Wisdom]);
			}

			[Test]
			public void SetAbilityCharisma()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(2, result.AbilityScores[AbilityType.Charisma]);
			}

			[Test]
			public void SetTraits()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(1, result.Traits.Count());
			}

			[Test]
			public void SetLanguages()
			{
				var traitLibrary = new TraitLibrary(new TraitSerializer(), Settings.Default.TraitLibrary);
				var serializer = new RaceSerializer(traitLibrary);
				var result = serializer.Deserialize(_xmlString);

				Assert.AreEqual(1, result.Languages.Count());
			}
		}
	}
}
