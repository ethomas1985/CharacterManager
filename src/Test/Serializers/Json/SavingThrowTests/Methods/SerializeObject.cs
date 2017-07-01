using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.SavingThrowTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var savingThrow =
				new SavingThrow(
					SavingThrowType.Fortitude,
					new AbilityScore(AbilityType.Constitution, 10), 1, 0, 0, 0);

			Assert.That(
					    () => JsonConvert.SerializeObject(savingThrow),
					    Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var savingThrow =
				new SavingThrow(
							    SavingThrowType.Fortitude,
							    new AbilityScore(AbilityType.Constitution, 10), 1, 0, 0, 0);
			var actual = JsonConvert.SerializeObject(savingThrow);

			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(ISavingThrow.Type)}\":\"{savingThrow.Type.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(ISavingThrow.AbilityModifier)}\":{savingThrow.AbilityModifier},")
					.Append($"\"{nameof(ISavingThrow.Base)}\":{savingThrow.Base},")
					.Append($"\"{nameof(ISavingThrow.Resist)}\":{savingThrow.Resist},")
					.Append($"\"{nameof(ISavingThrow.Misc)}\":{savingThrow.Misc},")
					.Append($"\"{nameof(ISavingThrow.Temporary)}\":{savingThrow.Temporary},")
					.Append($"\"{nameof(ISavingThrow.Score)}\":{savingThrow.Score}")
					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
