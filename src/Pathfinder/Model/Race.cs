using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Race : IRace
	{
		public Race(
			string pName, 
			string pDescription, 
			Size pSize, 
			int pBaseSpeed, 
			IDictionary<AbilityType, int> pAbilityScores, 
			IEnumerable<ITrait> pTraits, 
			IEnumerable<ILanguage> pLanguages)
		{
			Name = pName;
			Description = pDescription;
			Size = pSize;
			BaseSpeed = pBaseSpeed;
			AbilityScores = pAbilityScores;
			Traits = pTraits;
			Languages = pLanguages;
		}

		public string Name { get; private set; }
		public string Description { get; private set; }
		public Size Size { get; private set; }
		public int BaseSpeed { get; private set; }
		public IDictionary<AbilityType, int> AbilityScores { get; private set; }
		public IEnumerable<ITrait> Traits { get; private set; }
		public IEnumerable<ILanguage> Languages { get; private set; }
	}
}
