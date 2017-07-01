using System;
using System.Collections.Generic;

namespace Pathfinder.Interface
{
	public interface IAggregate
	{
		Guid Id { get; }
		int Version { get; }

		IEnumerable<IEvent> GetPendingEvents();
		void CommitEvents();
		void Load(IEnumerable<IEvent> pHistory);
	}

	public interface IEvent
	{
		Guid Id { get; }
		int Version { get; }
	}
}
