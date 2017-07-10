using System;
using Pathfinder.Interface.Model;
using Pathfinder.Interface.Model.Item;

namespace Pathfinder.Commands.Character
{
	public class RemoveItemFromInventory : ICommand
	{
		public RemoveItemFromInventory(Guid pId, int pOriginalVersion, IItem pItem)
		{
			Id = pId;
			OriginalVersion = pOriginalVersion;
			Item = pItem;
		}

		public Guid Id { get; }
		public int OriginalVersion { get; }
		public IItem Item { get; }
	}
}