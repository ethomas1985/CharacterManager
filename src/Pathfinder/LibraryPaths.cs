using System.IO;

namespace Pathfinder {
    internal class LibraryPaths
    {
        public LibraryPaths(string pBasePath = null)
        {
            BasePath = pBasePath ?? "../";
        }

        public string BasePath { get; set; }
        private string TraitDirectory { get; } = "Traits";
        private string SkillDirectory { get; } = "Skills";
        private string RaceDirectory { get; } = "Races";
        private string ClassDirectory { get; } = "Classes";
        private string ClassFeatureDirectory { get; } = "ClassFeatures";
        private string SpellDirectory { get; } = "Spells";
        private string FeatDirectory { get; } = "Feats";
        private string ItemDirectory { get; } = "Items";
        private string CharacterDirectory { get; } = "Characters";


        public string TraitLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, TraitDirectory));

        public string SkillLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, SkillDirectory));

        public string RaceLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, RaceDirectory));

        public string ClassLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, ClassDirectory));

        public string ClassFeatureLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, ClassFeatureDirectory));

        public string FeatLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, FeatDirectory));

        public string SpellLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, SpellDirectory));

        public string ItemLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, ItemDirectory));

        public string CharacterLibrary =>
            Path.GetFullPath(Path.Combine(BasePath, CharacterDirectory));
    }
}