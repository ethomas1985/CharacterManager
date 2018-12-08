using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;

namespace Pathfinder.Library
{
    [Obsolete("This was dumb.")]
    internal class SpellFileSystemRepository : AbstractFilesystemRepository<ISpell>
    {
        private readonly Lazy<SpellByClassIndex> _spellByClassIndex =
            new Lazy<SpellByClassIndex>(() => new SpellByClassIndex());

        private readonly Lazy<SpellByLevelByClassIndex> _spellByLevelByClassIndex =
            new Lazy<SpellByLevelByClassIndex>(() => new SpellByLevelByClassIndex());

        internal SpellFileSystemRepository(ISerializer<ISpell, string> pSerializer, string pLibraryDirectory, string pFileType = XML)
            : base(pSerializer, pLibraryDirectory, pFileType) { }

        private SpellByClassIndex SpellByClassIndex => _spellByClassIndex.Value;
        private SpellByLevelByClassIndex SpellByLevelByClassIndex => _spellByLevelByClassIndex.Value;

        /// <summary>
        /// Returns the Level Index for the given Class's Spell Library.
        /// </summary>
        /// <param name="pClass"></param>
        /// <returns></returns>
        internal SpellByLevelIndex this[IClass pClass]
        {
            get
            {
                SpellByLevelIndex value;
                if (SpellByLevelByClassIndex.TryGetValue(pClass.Name, out value))
                {
                    return value;
                }

                throw new KeyNotFoundException($"Key := \"{pClass.Name}\"");
            }
        }

        protected override void LoadFile(ISerializer<ISpell, string> pSerializer, string pFile)
        {
            var xml = File.ReadAllText(pFile);
            var spell = pSerializer.Deserialize(xml);

            Library.TryAdd(spell.Name, spell);

            foreach (var keyValue in spell.LevelRequirements)
            {
                var classRequirement = keyValue.Key;
                var levelRequirement = keyValue.Value;

                AddToClassIndex(classRequirement, spell);
                AddToClassLevelIndex(classRequirement, levelRequirement, spell);
            }
        }

        private void AddToClassIndex(string pClassRequirement, ISpell pSpell)
        {
            var classIndex = new SpellIndex();
            SpellByClassIndex[pClassRequirement] = classIndex;

            classIndex.AddOrUpdate(pSpell.Name, pSpell, (key, existingSpell) => pSpell);
        }

        private void AddToClassLevelIndex(string pClassRequirement, int pLevelRequirement, ISpell pSpell)
        {
            var newClassIndex = new SpellByLevelIndex();
            var classIndex =
                SpellByLevelByClassIndex.AddOrUpdate(pClassRequirement, newClassIndex, (key, existing) => existing);

            var newLevelIndex = new SpellIndex();
            var levelIndex = classIndex.AddOrUpdate(pLevelRequirement, newLevelIndex, (key, existing) => existing);

            levelIndex[pSpell.Name] = pSpell;
        }
    }

    internal class SpellIndex : ConcurrentDictionary<string, ISpell> { }

    internal class SpellByLevelIndex : ConcurrentDictionary<int, SpellIndex> { }

    internal class SpellByLevelByClassIndex : ConcurrentDictionary<string, SpellByLevelIndex> { }

    internal class SpellByClassIndex : ConcurrentDictionary<string, SpellIndex> { }
}
