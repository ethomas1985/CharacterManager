using Pathfinder.Enum;
using Pathfinder.Interface;
using System;

namespace Pathfinder.Model
{
	internal class Skill : ISkill
	{
		public Skill(IAbilityScore pAbilityScore)
		{
			AbilityScore = pAbilityScore;
		}

		private IAbilityScore AbilityScore { get; set; }

		public AbilityType Ability => AbilityScore.Type;

		public int AbilityModifier => AbilityScore.Modifier;

		public int ArmorClassPenalty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int ClassModifier
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int MiscModifier
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int Ranks
		{
			get; internal set;
		}

		public int TemporaryModifier
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int Total
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Type
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
