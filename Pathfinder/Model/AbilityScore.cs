using System;
using Pathfinder.Enum;
using Pathfinder.Interface;
using System.Collections.Generic;
using System.Linq;

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
				return
					new List<int> {
						Base,
						Enhanced,
						Inherent,
						Temporary,
						Penalty * -1
					}.Sum();
			}
		}

		public int Modifier
		{
			get
			{
				int scoreMinusTen = Score - 10;
				decimal half = scoreMinusTen / 2.0M;
				return Math.Max(-5, (int) Math.Floor(half));
			}
		}

		public int Base { get; internal set; }
		public int Enhanced { get; internal set; }
		public int Inherent { get; internal set; }
		public int Penalty { get; internal set; }
		public int Temporary { get; internal set; }

		internal int this[string pPropertyName]
		{
			get
			{
				switch (pPropertyName)
				{
					case nameof(Score):
						return Score;
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

		public override string ToString()
		{
			return $"{Ability}[{Score}][{Modifier}]";
		}
	}
}