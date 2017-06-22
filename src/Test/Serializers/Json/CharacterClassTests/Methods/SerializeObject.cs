using System.Collections.Generic;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Interface;
using Pathfinder.Model;
using Pathfinder.Test.ObjectMothers;

namespace Pathfinder.Test.Serializers.Json.CharacterClassTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var testClass = ClassMother.Create();
			var mockClassLibrary = new Mock<ILibrary<IClass>>();
			mockClassLibrary.Setup(foo => foo.Values).Returns(new List<IClass> {testClass});
			var classLibrary =mockClassLibrary.Object;

			var characterClass = new CharacterClass(testClass, 1, false, null);
			Assert.That(
				() => JsonConvert.SerializeObject(characterClass),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var testClass = ClassMother.Create();
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
