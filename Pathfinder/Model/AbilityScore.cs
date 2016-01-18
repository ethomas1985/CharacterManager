using System;
using Pathfinder.Enum;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class AbilityScore : IAbilityScore
	{
		public AbilityScore(AbilityType pAbilityType)
		{
			Ability = pAbilityType;
		}

		public AbilityType Ability { get; }

		public int Score
		{
			get
			{
				return Base + Enhanced + Inherent + Temporary - Penalty;
			}
		}

		public int Base { get; private set; }
		public int Enhanced { get; private set; }
		public int Inherent { get; private set; }
		public int Modifier { get; private set; }
		public int Penalty { get; private set; }
		public int Temporary { get; private set; }

		internal int this[string pPropertyName]
		{
			get
			{
				switch (pPropertyName)
				{
					case nameof(Base):
						return Base;
					case nameof(Enhanced):
						return Enhanced;
					case nameof(Inherent):
						return Inherent;
					case nameof(Modifier):
						return Modifier;
					case nameof(Penalty):
						return Penalty;
					case nameof(Temporary):
						return Temporary;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
			set
			{
				switch (pPropertyName)
				{
					case nameof(Base):
						Base = value;
						break;
					case nameof(Enhanced):
						Enhanced = value;
						break;
					case nameof(Inherent):
						Inherent = value;
						break;
					case nameof(Modifier):
						Modifier = value;
						break;
					case nameof(Penalty):
						Penalty = value;
						break;
					case nameof(Temporary):
						Temporary = value;
						break;
					default:
						throw new ArgumentException($"'{pPropertyName}' is not a valid Property.");
				}
			}
		}
	}
}