using System.Collections.Generic;
using Pathfinder.Enums;

namespace Pathfinder.Model
{
	public class RacialEffect : AbstractEffect
	{
		public RacialEffect(
			string pName,
			EffectType pType,
			string pText,
			IDictionary<string, int> pPropertyModifiers)
			: base(pName, pType, true, pText, pPropertyModifiers)
		{ }

		public override bool Active => true;
	}
}