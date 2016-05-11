using System.Collections.Generic;
using Pathfinder.Enums;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Race : IRace
	{
		public Race(
			string pName,
			string pAdjective,
			string pDescription,
			Size pSize,
			int pBaseSpeed,
			IDictionary<AbilityType, int> pAbilityScores,
			IEnumerable<ITrait> pTraits,
			IEnumerable<ILanguage> pLanguages)
		{
			Name = pName;
			Adjective = pAdjective;
			Description = pDescription;
			Size = pSize;
			BaseSpeed = pBaseSpeed;
			AbilityScores = pAbilityScores;
			Traits = pTraits;
			Languages = pLanguages;
		}

		public string Name { get; }
		public string Adjective { get; }
		public string Description { get; }
		public Size Size { get; }
		public int BaseSpeed { get; }
		public IDictionary<AbilityType, int> AbilityScores { get; }
		public IEnumerable<ITrait> Traits { get; }
		public IEnumerable<ILanguage> Languages { get; }

		public bool TryGetAbilityScore(AbilityType pAbilityType, out int pValue)
		{
			return AbilityScores.TryGetValue(pAbilityType, out pValue);
		}
	}
}
