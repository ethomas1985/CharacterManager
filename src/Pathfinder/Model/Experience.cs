using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder.Model
{
	internal class Experience : IExperience, IEquatable<IExperience>
	{
		private readonly IEnumerable<IEvent> _events;

		public Experience()
		{
			_events = new List<IEvent>();
		}

		private Experience(IEnumerable<IEvent> pEvents)
		{
			_events = pEvents;
		}

		public IEnumerator<IEvent> GetEnumerator()
		{
			return _events.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable) _events).GetEnumerator();
		}

		public IExperience Append(IEvent pItem)
		{
			Assert.ArgumentNotNull(pItem, nameof(pItem));

			return new Experience(_events.Append(pItem));
		}

		public IExperience Append(string pTitle, string pDescription, int pExperiencePoints)
		{
			return Append(new Event(pTitle, pDescription, pExperiencePoints));
		}

		public IExperience Append(IExperience pExperience)
		{
			Assert.ArgumentNotNull(pExperience, nameof(pExperience));

			return new Experience(_events.Append(pExperience));
		}

		public IExperience Remove(IEvent pEvent)
		{
			return new Experience(_events.Where(x => !x.Equals(pEvent)));
		}

		public int Total => this.Sum(x => x.ExperiencePoints);

		public override bool Equals(object pOther)
		{
			return Equals(pOther as IExperience);
		}

		public bool Equals(IExperience pOther)
		{
			if (ReferenceEquals(null, pOther))
			{
				return false;
			}
			if (ReferenceEquals(this, pOther))
			{
				return true;
			}
			return ComparisonUtilities.CompareEnumerables(nameof(IExperience), this, pOther, nameof(IEnumerable<IEvent>));
		}

		public override int GetHashCode()
		{
			return _events?.GetHashCode() ?? 0;
		}
	}
}
