using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Feature : IFeature
	{
		public Feature(string pName, string pBody)
		{
			Name = pName;
			Body = pBody;
		}

		public string Name { get; }
		public string Body { get; }
	}
}
