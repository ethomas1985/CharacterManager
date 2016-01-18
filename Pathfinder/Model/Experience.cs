using Pathfinder.Interface;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Pathfinder.Model
{
	internal class Experience : List<IEvent>, IExperience
	{
		public int Total
		{
			get
			{
				return this.Sum(x => x.ExperiencePoints);
			}
		}
	}
}