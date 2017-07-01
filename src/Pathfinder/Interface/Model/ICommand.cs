using System;

namespace Pathfinder.Interface.Model
{
	public interface ICommand
	{
		Guid Id { get; }
		int OriginalVersion { get; }
	}
}
