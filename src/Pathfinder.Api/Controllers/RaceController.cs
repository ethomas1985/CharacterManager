using System.Collections.Generic;
using System.Web.Http;
using Pathfinder.Interface;

namespace Pathfinder.Api.Controllers
{
	public class RaceController : ApiController
	{
		private readonly ILibrary<IRace> _raceLibrary = new LibraryFactory().GetRaceLibrary();

		// GET api/<controller>
		public IEnumerable<IRace> Get()
		{
			return _raceLibrary.Values;
		}

		// GET api/<controller>/5
		public IRace Get(string pId)
		{
			return _raceLibrary[pId];
		}

		//// POST api/<controller>
		//public void Post([FromBody]IRace pValue)
		//{
		//	_raceLibrary.Store(pValue);
		//}

		//// DELETE api/<controller>/5
		//public void Delete(int pId)
		//{
		//}
	}
}