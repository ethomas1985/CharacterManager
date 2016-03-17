using System.Collections.Generic;
using Pathfinder.Interface;

namespace Pathfinder.Model
{
	internal class Trait : ITrait
	{
		public Trait(
			string pName, 
			string pText,
			IDictionary<string, int> pPropertyModifiers)
		{
			Name = pName;
			Text = pText;
			PropertyModifiers = pPropertyModifiers;
		}

		public string Name { get; }
		public bool Active { get; set; }
		public string Text { get; }
		public IDictionary<string, int> PropertyModifiers { get; }
	}
}
