using System;
using Pathfinder.Interface;
using Pathfinder.Library;
using Pathfinder.Properties;
using Pathfinder.Serializers;

namespace Pathfinder
{
	public  class LibraryFactory
	{
		private  readonly ILibrary<IClass> _classLibrary;
		//private  readonly ILibrary<IFeat> FeatLibrary;
		private  readonly ILibrary<IFeature> _featureLibrary;
		private  readonly ILibrary<IRace> _raceLibrary;
		private  readonly ILibrary<ISkill> _skillLibrary;
		private  readonly ILibrary<ISpell> _spellLibrary;
		private  readonly ILibrary<ITrait> _traitLibrary;

		public LibraryFactory()
		{
			var classSerializer = new ClassXmlSerializer();
			_classLibrary = new ClassLibrary(classSerializer, Settings.Default.ClassLibrary);

			//var _featSerializer = new FeatXmlSerializer();
			//_featLibrary = new FeatLibrary(_featSerializer, Settings.Default.FeatLibrary);

			var featureSerializer = new FeatureXmlSerializer();
			_featureLibrary = new FeatureLibrary(featureSerializer, Settings.Default.ClassFeatureLibrary);

			var skillSerializer = new SkillXmlSerializer();
			_skillLibrary = new SkillLibrary(skillSerializer, Settings.Default.SkillLibrary);

			var spellSerializer = new SpellXmlSerializer();
			_spellLibrary = new SpellLibrary(spellSerializer, Settings.Default.SpellLibrary);

			var traitSerializer = new TraitXmlSerializer();
			_traitLibrary = new TraitLibrary(traitSerializer, Settings.Default.TraitLibrary);
			
			var raceSerializer = new RaceXmlSerializer(_traitLibrary);
			_raceLibrary = new RaceLibrary(raceSerializer, Settings.Default.RaceLibrary);
		}

		public ILibrary<IClass> GetClassLibrary()
		{
			return _classLibrary;
		}

		public ILibrary<IFeat> GetFeatLibrary()
		{
			throw new NotImplementedException();
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
	}
}
