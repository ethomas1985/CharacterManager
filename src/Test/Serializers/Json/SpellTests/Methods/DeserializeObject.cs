﻿using System.Collections.Generic;
using System.Linq;
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
    public class DeserializeObject
    {
        [Test]
        public void RequiresName()
        {
            Assert.That(
                () => JsonConvert.DeserializeObject<ISpell>("{}"),
                Throws.Exception
                    .TypeOf<JsonException>()
                    .With.Message.EqualTo($"Missing Required Attribute: {nameof(ISpell.Name)}"));
        }

        [Test]
        public void Expected()
        {
            LogTo.ChangeLogLevel("Verbose");

            const string name = "Testing Spell";
            const MagicSchool magicSchool = MagicSchool.Abjuration;
            const MagicSubSchool magicSubSchool = MagicSubSchool.Charm;
            var magicDescriptors = new HashSet<MagicDescriptor>
            {
                MagicDescriptor.Acid
            };
            const string savingThrow = "Testing Saving Throw";
            const bool hasSpellResistance = true;
            const string spellResistance = "Testing Spell Resistance";
            const string castingTime = "Testing Casting Time";
            const string range = "Testing Range";
            var levelRequirements = new Dictionary<string, int>
            {
                ["Testing Something"] = 1
            };
            var description = new List<string> { "Testing Description" };
            const string duration = "Testing Duration";
            var spellComponents = new HashSet<ISpellComponent>
            {
                new SpellComponent(ComponentType.Material, "Testing Material")
            };

            var magicDescriptorStrings = magicDescriptors.Select(x => $"\"{x}\"");
            var levelRequirementStrings = levelRequirements.Select(x => $"\"{x.Key}\":{x.Value}");
            var spellComponentStrings =
                spellComponents
                    .Select(x => $"{{" +
                                 $"\"{nameof(ISpellComponent.ComponentType)}\":\"{x.ComponentType}\"," +
                                 $"\"{nameof(ISpellComponent.Description)}\":\"{x.Description}\"" +
                                 $"}}");
            var result = JsonConvert.DeserializeObject<ISpell>(
                $"{{" +
                $"\"{nameof(ISpell.Name)}\":\"{name}\"," +
                $"\"{nameof(ISpell.School)}\":\"{magicSchool}\"," +
                $"\"{nameof(ISpell.SubSchools)}\":[\"{magicSubSchool}\"]," +
                $"\"{nameof(ISpell.MagicDescriptors)}\":[" +
                $"{string.Join(",", magicDescriptorStrings)}" +
                $"]," +
                $"\"{nameof(ISpell.SavingThrow)}\":\"{savingThrow}\"," +
                $"\"{nameof(ISpell.Description)}\":[\"{string.Join("\", \"", description)}\"]," +
                $"\"{nameof(ISpell.HasSpellResistance)}\":\"{hasSpellResistance}\"," +
                $"\"{nameof(ISpell.SpellResistance)}\":\"{spellResistance}\"," +
                $"\"{nameof(ISpell.CastingTime)}\":\"{castingTime}\"," +
                $"\"{nameof(ISpell.Range)}\":\"{range}\"," +
                $"\"{nameof(ISpell.LevelRequirements)}\":{{" +
                $"{string.Join(",", levelRequirementStrings)}" +
                $"}}," +
                $"\"{nameof(ISpell.Duration)}\":\"{duration}\"," +
                $"\"{nameof(ISpell.Components)}\":[" +
                $"{string.Join(",", spellComponentStrings)}" +
                $"]" +
                "}");

            var expected =
                new Spell(name, magicSchool, new[] { magicSubSchool }, magicDescriptors, savingThrow, description, hasSpellResistance, spellResistance, castingTime, range, levelRequirements, duration, spellComponents);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
