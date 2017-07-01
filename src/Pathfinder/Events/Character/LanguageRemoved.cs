using System;
using Pathfinder.Interface.Model;

namespace Pathfinder.Events.Character
{
	internal class LanguageRemoved : AbstractEvent, IEquatable<LanguageRemoved>
	{
		public LanguageRemoved(Guid pId, int pVersion, ILanguage pLanguage)
			: base(pId, pVersion)
		{
			Language = pLanguage;
		}

		public ILanguage Language { get; }

		public override string ToString()
		{
			return $"Character [{Id}] | Removed the '{Language}' {nameof(Language).ToLower()} | Version {Version}";
		}

		public override bool Equals(object pOther)
		{
			return Equals(pOther as LanguageRemoved);
		}

		public bool Equals(LanguageRemoved pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}

			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return base.Equals(pOther)
				   && string.Equals(Language, pOther.Language);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode() * 397) ^ (Language != null ? Language.GetHashCode() : 0);
			}
		}
	}
}