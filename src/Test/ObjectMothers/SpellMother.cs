using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Test.ObjectMothers
{
    public class SpellMother
    {
        public static ISpell AcidArrow()
        {
            return new Spell(
                pName: "Acid Arrow",
                pSchool: MagicSchool.Conjuration,
                pSubSchools: new[]
                {
                    MagicSubSchool.Creation
                },
                pMagicDescriptors: new HashSet<MagicDescriptor>
                {
                    MagicDescriptor.Acid
                },
                pSavingThrow: null,
                pDescription: new[]
                {
                    "An arrow of acid springs from your hand and speeds to its target. You must succeed on a ranged touch attack to hit your target. The arrow deals 2d4 points of acid damage with no splash damage. For every three caster levels you possess, the acid, unless neutralized, lasts for another round (to a maximum of 6 additional rounds at 18th level), dealing another 2d4 points of damage in each round."
                },
                pHasSpellResistance: false,
                pSpellResistance: null,
                pCastingTime: "1 standard action",
                pRange: "long (400 ft. + 40 ft./level)",
                pLevelRequirements: new Dictionary<string, int>()
                {
                    ["sorcerer"] = 2,
                    ["wizard"] = 2
                },
                pDuration: "1 round + 1 round per three levels",
                pComponents: new HashSet<ISpellComponent>
                {
                    new SpellComponent(ComponentType.Verbal, string.Empty),
                    new SpellComponent(ComponentType.Somatic, string.Empty),
                    new SpellComponent(ComponentType.Material, "Rhubarb leaf and an adder's stomach"),
                    new SpellComponent(ComponentType.Focus, "A dart"),
                }
            );
        }
    }
}
