using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Die : IDie
	{
		public Die(int faces)
		{
			Faces = faces;
		}

		public int Faces { get; }
	}
}
