using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.SpellComponentTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var spellComponent = CreateTestingSpell();
			Assert.That(
				() => JsonConvert.SerializeObject(spellComponent),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var spellComponent = CreateTestingSpell();
			var actual = JsonConvert.SerializeObject(spellComponent);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(ISpellComponent.ComponentType)}\":\"{spellComponent.ComponentType.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(ISpellComponent.Description)}\":\"{spellComponent.Description}\"")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}

		private static SpellComponent CreateTestingSpell()
		{
			return new SpellComponent(ComponentType.Material, "Testing Material");
		}
	}
}
