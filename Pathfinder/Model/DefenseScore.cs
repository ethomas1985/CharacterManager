using System;
using Pathfinder.Enum;
using Pathfinder.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Pathfinder.Model
{
	internal class DefenseScore : IDefenseScore
	{
		public DefenseScore(
			DefensiveType pDefensiveType, 
			IAbilityScore pDexterity, 
			Func<Size> pGetSize)
		{
			Debug.Assert(AbilityType.Dexterity == pDexterity.Ability);
			Debug.Assert(pGetSize != null);

			Type = pDefensiveType;
			Dexterity = pDexterity;
			GetSize = pGetSize;
		}

		private IAbilityScore Dexterity { get; }
		private Func<Size> GetSize { get; }

		public DefensiveType Type { get; }

		public int Score
		{
			get
			{
				return new List<int> {
					//10,
					//Base,
					//Shield,
					//DexterityModifier,
					//SizeModifier,
					//Natural,
					//Deflect,
					//Dodge,
					//MiscModifier,
					//Temporary
				}.Sum();
			}
		}

		public int Base { get; private set; }

		public int Shield { get; private set; }

		public int Deflect { get; private set; }
		public int DexterityModifier { get { return Dexterity.Modifier; } }
		public int Dodge { get; private set; }
		public int MiscModifier { get; private set; }
		public int Natural { get; private set; }
		public int SizeModifier
		{
			get
			{
				var size = GetSize();
				switch (size)
				{
					case Size.Small:
						return -1;
					case Size.Medium:
						return 0;
					case Size.Large:
						return 1;
					default:
						throw new Exception("Invalid Size.");
				}
			}
		}
		public int Temporary { get; private set; }

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
					case nameof(Shield):
						return Shield;
					case nameof(Deflect):
						return Deflect;
					case nameof(DexterityModifier):
						return DexterityModifier;
					case nameof(Dodge):
						return Dodge;
					case nameof(MiscModifier):
						return MiscModifier;
					case nameof(Natural):
						return Natural;
					case nameof(SizeModifier):
						return SizeModifier;
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
					case nameof(Shield):
						Shield = value;
						break;
					case nameof(Deflect):
						Deflect = value;
						break;
					case nameof(Dodge):
						Dodge = value;
						break;
					case nameof(MiscModifier):
						MiscModifier = value;
						break;
					case nameof(Natural):
						Natural = value;
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