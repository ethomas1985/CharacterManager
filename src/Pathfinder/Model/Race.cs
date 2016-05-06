using System.Collections.Generic;
using System.Linq;
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
			Traits = pTraits.ToList();
			Languages = pLanguages.ToList();
		}

		public string Name { get; }
		public string Description { get; }
		public Size Size { get; }
		public int BaseSpeed { get; }
		public IDictionary<AbilityType, int> AbilityScores { get; }
		public IEnumerable<ITrait> Traits { get; }
		public IEnumerable<ILanguage> Languages { get; }
	}
}
