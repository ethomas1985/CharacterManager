using System.ComponentModel.DataAnnotations;

namespace Pathfinder.Interface.Model
{
	public interface INamed
	{
        [Key]
		string Name { get; }
	}
}
