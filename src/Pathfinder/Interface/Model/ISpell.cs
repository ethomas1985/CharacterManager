using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Interface.Model
{
    public interface ISpell : INamed
    {
        MagicSchool School { get; }
        IEnumerable<MagicSubSchool> SubSchools { get; }
        ISet<MagicDescriptor> MagicDescriptors { get; }
        string SavingThrow { get; }
        string Description { get; }
        bool HasSpellResistance { get; }
        string SpellResistance { get; }
        string CastingTime { get; }
        string Range { get; }

        IDictionary<string, int> LevelRequirements { get; }

        string Duration { get; }
        ISet<ISpellComponent> Components { get; }
    }
}
