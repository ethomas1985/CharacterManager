using System;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Event : IEvent, IEquatable<IEvent>
	{
		public Event(string pTitle, string pDescription, int pExperiencePoints)
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
			return Equals(pObject as IEvent);
		}

		public bool Equals(IEvent pOther)
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
				ComparisonUtilities.Compare(nameof(IEvent), Title, pOther.Title, nameof(Title))
				&& ComparisonUtilities.Compare(nameof(IEvent), Description, pOther.Description, nameof(Description))
				&& ComparisonUtilities.Compare(nameof(IEvent), ExperiencePoints, pOther.ExperiencePoints, nameof(ExperiencePoints));
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
