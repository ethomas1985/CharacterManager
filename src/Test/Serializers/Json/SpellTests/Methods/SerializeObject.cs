using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using Pathfinder.Enums;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;
using Pathfinder.Model;
using Pathfinder.Utilities;
using Assert = NUnit.Framework.Assert;

namespace Pathfinder.Test.Serializers.Json.SpellTests.Methods
{
	[TestFixture]
	public class SerializeObject
	{
		[Test]
		public void Success()
		{
			var spell = CreateTestingSpell();
			Assert.That(
				() => JsonConvert.SerializeObject(spell),
				Throws.Nothing);
		}

		[Test]
		public void Expected()
		{
			var spell = CreateTestingSpell();
			var actual = JsonConvert.SerializeObject(spell);

			var magicDescriptorStrings = spell.MagicDescriptors.Select(x => $"\"{x.ToString().ToCamelCase()}\"");
			var levelRequirementStrings = spell.LevelRequirements.Select(x => $"\"{x.Key}\":{x.Value}");
			var spellComponentStrings = spell.Components.Select(x => $"{{" +
																	 $"\"{nameof(ISpellComponent.ComponentType)}\":\"{x.ComponentType.ToString().ToCamelCase()}\"," +
																	 $"\"{nameof(ISpellComponent.Description)}\":\"{x.Description}\"" +
																	 $"}}");
			var expected =
				new StringBuilder("{")
					.Append($"\"{nameof(ISpell.Name)}\":\"{spell.Name}\",")
					.Append($"\"{nameof(ISpell.School)}\":\"{spell.School.ToString().ToCamelCase()}\",")
					.Append($"\"{nameof(ISpell.SubSchools)}\":[\"{string.Join("\", \"",spell.SubSchools.ToString().ToCamelCase())}\"],")
					
					.Append($"\"{nameof(ISpell.MagicDescriptors)}\":[")
					.Append($"{string.Join(",", magicDescriptorStrings)}")
					.Append($"],")
					
					.Append($"\"{nameof(ISpell.SavingThrow)}\":\"{spell.SavingThrow}\",")
					.Append($"\"{nameof(ISpell.Description)}\":\"{spell.Description}\",")
					.Append($"\"{nameof(ISpell.HasSpellResistance)}\":{spell.HasSpellResistance.ToString().ToCamelCase()},")
					.Append($"\"{nameof(ISpell.SpellResistance)}\":\"{spell.SpellResistance}\",")
					.Append($"\"{nameof(ISpell.CastingTime)}\":\"{spell.CastingTime}\",")
					.Append($"\"{nameof(ISpell.Range)}\":\"{spell.Range}\",")
					
					.Append($"\"{nameof(ISpell.LevelRequirements)}\":{{")
					.Append($"{string.Join(",", levelRequirementStrings)}")
					.Append($"}},")

					.Append($"\"{nameof(ISpell.Duration)}\":\"{spell.Duration}\",")
					
					.Append($"\"{nameof(ISpell.Components)}\":[")
					.Append($"{string.Join(",", spellComponentStrings)}")
					.Append($"]")

					.Append("}")
					.ToString();

			Assert.That(actual, Is.EqualTo(expected));
		}

		private static Spell CreateTestingSpell()
		{
			var spell =
				new Spell(
						  "Testing Spell",
						  MagicSchool.Abjuration,
						  new [] { MagicSubSchool.Charm },
						  new HashSet<MagicDescriptor>
						  {
							  MagicDescriptor.Acid
						  },
						  "Testing Saving Throw",
						  "Testing Description",
						  true,
						  "Testing Spell Resisitance",
						  "Testing Casting Time",
						  "Testing Range",
						  new Dictionary<string, int>
						  {
							  ["Testing Something"] = 1
						  },
						  "Testing Duration",
						  new HashSet<ISpellComponent>
						  {
							  new SpellComponent(ComponentType.Material, "Testing Material")
						  });
			return spell;
		}
	}
}
