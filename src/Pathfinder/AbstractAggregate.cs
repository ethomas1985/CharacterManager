using System;
using System.Collections.Generic;
using System.Diagnostics;
using Pathfinder.Interface;
using Pathfinder.Utilities;

namespace Pathfinder
{
	public abstract class AbstractAggregate : IAggregate
	{
		public Guid Id { get; protected set;}
		public int Version { get; protected set;}

		private List<IEvent> PendingEvents { get; } = new List<IEvent>();

		public IEnumerable<IEvent> GetPendingEvents()
		{
			return PendingEvents;
		}

		public void CommitEvents()
		{
			PendingEvents.Clear();
		}

		public void Load(IEnumerable<IEvent> pHistory)
		{
			foreach (var e in pHistory)
			{
				ApplyChange(e, false);
			}
		}

		protected void ApplyChange(IEvent pEvent, bool pIsNew)
		{
			Apply(pEvent);
			if (pIsNew)
			{
				PendingEvents.Add(pEvent);
			}
			Tracer.Message(pEvent.ToString());
		}

		protected abstract void Apply(IEvent pEvent);
	}
}
