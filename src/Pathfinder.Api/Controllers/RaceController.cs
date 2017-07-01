using System.Collections.Generic;
using System.Web.Http;
using Pathfinder.Interface;
using Pathfinder.Interface.Model;

namespace Pathfinder.Api.Controllers
{
	public class RaceController : ApiController
	{
		private readonly IRepository<IRace> _raceRepository = new RepositoryFactory().GetRaceRespository();

		// GET api/<controller>
		public IEnumerable<IRace> Get()
		{
			return _raceRepository.Values;
		}

		// GET api/<controller>/5
		public IRace Get(string pId)
		{
			return _raceRepository[pId];
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