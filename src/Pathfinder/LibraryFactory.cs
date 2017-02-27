using Newtonsoft.Json;
using Pathfinder.Interface;
using Pathfinder.Interface.Item;
using Pathfinder.Library;
using Pathfinder.Serializers.Xml;
using System.IO;
using System.Web;

namespace Pathfinder
{
	public class LibraryFactory
	{
		private LibraryPath _libraryPath;
		private  readonly ILibrary<IClass> _classLibrary;
		private  readonly ILibrary<IFeat> _featLibrary;
		private  readonly ILibrary<IFeature> _featureLibrary;
		private  readonly ILibrary<IRace> _raceLibrary;
		private  readonly ILibrary<ISkill> _skillLibrary;
		private  readonly ILibrary<ISpell> _spellLibrary;
		private  readonly ILibrary<ITrait> _traitLibrary;
		private  readonly ILibrary<IItem> _itemLibrary;
		private  readonly ILibrary<ICharacter> _characterLibrary;

		public LibraryFactory()
		{
			Initialize();

			var classSerializer = new ClassXmlSerializer();
			_classLibrary = new ClassLibrary(classSerializer, ClassLibrary);

			var featSerializer = new FeatXmlSerializer();
			_featLibrary = new FeatLibrary(featSerializer, FeatLibrary);

			var featureSerializer = new FeatureXmlSerializer();
			_featureLibrary = new FeatureLibrary(featureSerializer, ClassFeatureLibrary);

			var skillSerializer = new SkillXmlSerializer();
			_skillLibrary = new SkillLibrary(skillSerializer, SkillLibrary);

			var spellSerializer = new SpellXmlSerializer();
			_spellLibrary = new SpellLibrary(spellSerializer, SpellLibrary);

			var traitSerializer = new TraitXmlSerializer();
			_traitLibrary = new TraitLibrary(traitSerializer, TraitLibrary);

			var raceSerializer = new RaceXmlSerializer(_traitLibrary);
			_raceLibrary = new RaceLibrary(raceSerializer, RaceLibrary);

			var itemSerializer = new ItemXmlSerializer();
			_itemLibrary = new ItemLibrary(itemSerializer, ItemLibrary);

			var characterSerializer = new CharacterXmlSerializer();
			_characterLibrary = new CharacterLibrary(characterSerializer, CharacterLibrary);
		}

		private void Initialize()
		{
			string filePath = Path.Combine(HttpRuntime.BinDirectory,"libraryPaths.json");

			_libraryPath =
				File.Exists(filePath)
					? JsonConvert.DeserializeObject<LibraryPath>(File.ReadAllText(filePath))
					: new LibraryPath();
		}

		public string TraitLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.TraitLibrary));
		public string SkillLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.SkillLibrary));
		public string RaceLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.RaceLibrary));
		public string ClassLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.ClassLibrary));
		public string ClassFeatureLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.ClassFeatureLibrary));
		public string FeatLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.FeatLibrary));
		public string SpellLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.SpellLibrary));
		public string ItemLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.ItemLibrary));
		public string CharacterLibrary =>
			Path.GetFullPath(Path.Combine(HttpRuntime.BinDirectory, _libraryPath.CharacterLibrary));

		public ILibrary<IClass> GetClassLibrary()
		{
			return _classLibrary;
		}

		public ILibrary<IFeat> GetFeatLibrary()
		{
			return _featLibrary;
		}

		public ILibrary<IFeature> GetFeatureLibrary()
		{
			return _featureLibrary;
		}

		public ILibrary<IRace> GetRaceLibrary()
		{
			return _raceLibrary;
		}

		public ILibrary<ISkill> GetSkillLibrary()
		{
			return _skillLibrary;
		}

		public ILibrary<ISpell> GetSpellLibrary()
		{
			return _spellLibrary;
		}

		public ILibrary<ITrait> GetTraitLibrary()
		{
			return _traitLibrary;
		}

		public ILibrary<IItem> GetItemLibrary()
		{
			return _itemLibrary;
		}

		public ILibrary<ICharacter> GetCharacterLibrary()
		{
			return _characterLibrary;
		}
	}

	internal class LibraryPath
	{
		public string TraitLibrary { get; set; } = "../Trait/";
		public string SkillLibrary { get; set; } = "../Skills/";
		public string RaceLibrary { get; set; } = "../Races/";
		public string ClassLibrary { get; set; } = "../Classes/";
		public string ClassFeatureLibrary { get; set; } = "../ClassFeatures/";
		public string SpellLibrary { get; set; } = "../Spells/";
		public string FeatLibrary { get; set; } = "../Feats/";
		public string ItemLibrary { get; set; } = "../Items/";
		public string CharacterLibrary { get; set; } = "../Characters/";
	}
}
