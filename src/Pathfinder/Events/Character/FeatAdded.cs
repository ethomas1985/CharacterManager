using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character
{
	internal class FeatAdded : AbstractEvent, IEquatable<FeatAdded>
	{
		public FeatAdded(Guid pId, int pVersion, IFeat pFeat, string pSpecialization = null)
			: base(pId, pVersion)
		{
			Feat = pFeat;
			Specialization = pSpecialization;
		}
		public IFeat Feat { get; }
		public string Specialization { get; }

		public override string ToString()
		{
			var featText = string.IsNullOrWhiteSpace(Specialization) ? Feat.ToString() : $"{Feat} - {Specialization}";
			return $"Character [{Id}] | {nameof(Feat)} '{featText}' Added | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as FeatAdded);
		}

		public bool Equals(FeatAdded pOther)
		{
			return base.Equals(pOther)
				&& Equals(Feat, pOther.Feat)
				&& Equals(Specialization, pOther.Specialization);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (base.GetHashCode() * 397)
					^ (Feat != null ? Feat.GetHashCode() : 0)
					^ (Specialization != null ? Specialization.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}