using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.Mocks;

namespace Pathfinder.Test.Serializers.Json.CharacterClassTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var classLibrary = new MockClassLibrary();
			var characterClass = new CharacterClass(classLibrary.Values.First(), 1, false, null);
			Assert.That(
				() => JsonConvert.SerializeObject(characterClass),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var classLibrary = new MockClassLibrary();
			var testClass = classLibrary.Values.First();
			var characterClass = new CharacterClass(testClass, 1, false, null);
			
			var actual = JsonConvert.SerializeObject(characterClass);

			Assert.That(actual,
				Is.EqualTo(
					$"{{" +
					$"\"{nameof(ICharacterClass.Class)}\":\"{testClass.Name}\"," +
					$"\"{nameof(ICharacterClass.Level)}\":1," +
					$"\"{nameof(ICharacterClass.IsFavored)}\":false," +
					$"\"{nameof(ICharacterClass.BaseAttackBonus)}\":1," +
					$"\"{nameof(ICharacterClass.Fortitude)}\":1," +
					$"\"{nameof(ICharacterClass.Reflex)}\":1," +
					$"\"{nameof(ICharacterClass.Will)}\":1," +
					$"\"{nameof(ICharacterClass.HitPoints)}\":[]" +
					$"}}"));
		}
	}
}
