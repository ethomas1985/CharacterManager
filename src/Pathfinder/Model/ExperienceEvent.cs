using System;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class ExperienceEvent : IExperienceEvent, IEquatable<IExperienceEvent>
	{
		public ExperienceEvent(string pTitle, string pDescription, int pExperiencePoints)
		{
			Assert.ArgumentIsNotEmpty(pTitle, nameof(pTitle));

			Title = pTitle;
			Description = pDescription;
			ExperiencePoints = pExperiencePoints;
		}

		public string Title { get; }
		public string Description { get; }
		public int ExperiencePoints { get; }

		public override bool Equals(object pObject)
		{
			return Equals(pObject as IExperienceEvent);
		}

		public bool Equals(IExperienceEvent pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}

			return 
				ComparisonUtilities.Compare(nameof(IExperienceEvent), Title, pOther.Title, nameof(Title))
				&& ComparisonUtilities.Compare(nameof(IExperienceEvent), Description, pOther.Description, nameof(Description))
				&& ComparisonUtilities.Compare(nameof(IExperienceEvent), ExperiencePoints, pOther.ExperiencePoints, nameof(ExperiencePoints));
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = Title?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ ExperiencePoints;
				return hashCode;
			}
		}
	}
}
