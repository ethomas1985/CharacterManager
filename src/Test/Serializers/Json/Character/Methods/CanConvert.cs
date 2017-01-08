using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Test.Mocks;
using CharacterImpl = Pathfinder.Model.Character;

namespace Pathfinder.Test.Serializers.Json.Character.Methods
{
	[TestFixture]
	public class CanConvert
	{
		[Test]
		public void False()
		{
			var converter = new Pathfinder.Serializers.Json.CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			Assert.IsFalse(converter.CanConvert(typeof(string)));
		}

		[Test]
		public void CanConvertCharacter()
		{
			var converter = new Pathfinder.Serializers.Json.CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			Assert.IsTrue(converter.CanConvert(typeof(CharacterImpl)));
		}

		[Test]
		public void CanConvertICharacter()
		{
			var converter = new Pathfinder.Serializers.Json.CharacterJsonSerializer(
				new MockRaceLibrary(),
				new MockSkillLibrary(),
				new MockClassLibrary());

			Assert.IsTrue(converter.CanConvert(typeof(ICharacter)));
		}
	}
}