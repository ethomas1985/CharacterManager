using NUnit.Framework;
using Pathfinder.Enum;
using Pathfinder.Model.Factories;

namespace Test.Factories
{
	[TestFixture]
	public class CharacterBuilderTests
	{
		[TestFixture]
		public class UseCase : CharacterBuilderTests
		{
			[Test]
			public void BuildSotallytober()
			{
				var builder =
					new CharacterBuilder(Race.Elf)
						//.SetName("Sotallytober")
						//.SetClass(new Ranger(1, 10))
						//.SetAlignment(Alignment.NeutralGood)
						//.SetGender(Gender.Male)
						//.SetAge(30)
						//.SetHeight(6.0M)
						//.SetWeight()
						//.SetHair("")
						//.SetEyes("")
						//.SetHomeland()
						.SetBaseStrength(13)
						.SetBaseDexterity(14)
						.SetBaseConstitution(14)
						.SetBaseIntelligence(13)
						.SetBaseWisdom(11)
						.SetBaseCharisma(10)
						.SetStartingGold();

				var character = builder.Build();
				Assert.NotNull(character);
			}
		}
	}
}