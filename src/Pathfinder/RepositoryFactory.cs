using Newtonsoft.Json;
using Pathfinder.Interface;
using Pathfinder.Library;
using Pathfinder.Serializers.Xml;
using System.IO;
using System.Web;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder
{
	public class RepositoryFactory
	{
		private LibraryPath _libraryPath;
		private  readonly IRepository<IClass> _classRepository;
		private  readonly IRepository<IFeat> _featRepository;
		private  readonly IRepository<IFeature> _featureRepository;
		private  readonly IRepository<IRace> _raceRepository;
		private  readonly IRepository<ISkill> _skillRepository;
		private  readonly IRepository<ISpell> _spellRepository;
		private  readonly IRepository<ITrait> _traitRepository;
		private  readonly IRepository<IItem> _itemRepository;
		private  readonly IRepository<ICharacter> _characterRepository;

		public RepositoryFactory()
		{
			Initialize();

			var classSerializer = new ClassXmlSerializer();
			_classRepository = new ClassRepository(classSerializer, ClassLibrary);

			var featSerializer = new FeatXmlSerializer();
			_featRepository = new FeatRepository(featSerializer, FeatLibrary);

			var featureSerializer = new FeatureXmlSerializer();
			_featureRepository = new FeatureRepository(featureSerializer, ClassFeatureLibrary);

			var skillSerializer = new SkillXmlSerializer();
			_skillRepository = new SkillRepository(skillSerializer, SkillLibrary);

			var spellSerializer = new SpellXmlSerializer();
			_spellRepository = new SpellRepository(spellSerializer, SpellLibrary);

			var traitSerializer = new TraitXmlSerializer();
			_traitRepository = new TraitRepository(traitSerializer, TraitLibrary);

			var raceSerializer = new RaceXmlSerializer(_traitRepository);
			_raceRepository = new RaceRepository(raceSerializer, RaceLibrary);

			var itemSerializer = new ItemXmlSerializer();
			_itemRepository = new ItemRepository(itemSerializer, ItemLibrary);

			var characterSerializer = new CharacterXmlSerializer();
			_characterRepository = new CharacterRepository(characterSerializer, CharacterLibrary);
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

		public IRepository<IClass> GetClassRespository()
		{
			return _classRepository;
		}

		public IRepository<IFeat> GetFeatRespository()
		{
			return _featRepository;
		}

		public IRepository<IFeature> GetFeatureRespository()
		{
			return _featureRepository;
		}

		public IRepository<IRace> GetRaceRespository()
		{
			return _raceRepository;
		}

		public IRepository<ISkill> GetSkillRespository()
		{
			return _skillRepository;
		}

		public IRepository<ISpell> GetSpellRespository()
		{
			return _spellRepository;
		}

		public IRepository<ITrait> GetTraitRespository()
		{
			return _traitRepository;
		}

		public IRepository<IItem> GetItemRespository()
		{
			return _itemRepository;
		}

		public IRepository<ICharacter> GetCharacterRespository()
		{
			return _characterRepository;
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
