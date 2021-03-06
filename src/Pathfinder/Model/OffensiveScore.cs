﻿using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Enums;
using Pathfinder.Interface.Model;

namespace Pathfinder.Model
{
	internal class OffensiveScore : IOffensiveScore, IEquatable<IOffensiveScore>
	{
		public OffensiveScore(
			OffensiveType pOffensiveType,
			IAbilityScore pAbilityScore,
			int pBaseAttackBonus,
			int pSizeModifier,
			int pTemporaryModifier)
		{
			Type = pOffensiveType;
			Ability = pAbilityScore;
			BaseAttackBonus = pBaseAttackBonus;
			SizeModifier = pSizeModifier;
			TemporaryModifier = pTemporaryModifier;
		}

		public OffensiveType Type { get; }
		private IAbilityScore Ability { get; }

		public int Score
		{
			get
			{
				var score = Values.Sum();

				//Tracer.Message(
				//	pMessage: $"{Type} = {string.Join(" + ", Values)} = {score}");

				return score;
			}
		}

		public int BaseAttackBonus { get; }
		public int AbilityModifier => Ability?.Modifier ?? 0;
		public int SizeModifier { get; }
		public int TemporaryModifier { get; }

		private IEnumerable<int> Values
			=> new List<int>
			{
				BaseAttackBonus,
				AbilityModifier,
				SizeModifier,
				TemporaryModifier
			};

		public override string ToString()
		{
			return $"{Type} = {string.Join(" + ", Values)} = {Score}";
		}

		public override bool Equals(object pObject)
		{
			return Equals(pObject as IOffensiveScore);
		}

		public bool Equals(IOffensiveScore pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			var equal = 
				Type == pOther.Type
				&& AbilityModifier == pOther.AbilityModifier
				&& BaseAttackBonus == pOther.BaseAttackBonus
				&& SizeModifier == pOther.SizeModifier
				&& TemporaryModifier == pOther.TemporaryModifier;

			return equal;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (int) Type;
				hashCode = (hashCode*397) ^ (Ability != null ? Ability.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ BaseAttackBonus;
				hashCode = (hashCode*397) ^ SizeModifier;
				hashCode = (hashCode*397) ^ TemporaryModifier;
				return hashCode;
			}
		}
	}
}