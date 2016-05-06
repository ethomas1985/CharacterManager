using System.Collections.Generic;
using System.Web.Http;
using Pathfinder.Api.Properties;
using Pathfinder.Interface;

namespace Pathfinder.Api.Controllers
{
    public class CharacterController : ApiController
    {
	    public ICharacter New(IRace pRace)
	    {
		    return new CharacterFactory(Settings.Default.CharacterLibraryDirectory).Create(pRace);

	    }

        // GET: api/Character
        public IEnumerable<ICharacter> Get()
        {
            return new List<ICharacter>();
        }

        // GET: api/Character/5
        public ICharacter Get(string pName)
        {
            return new CharacterFactory(Settings.Default.CharacterLibraryDirectory).Get(pName);
        }

        // POST: api/Character
        public void Post([FromBody]ICharacter pValue)
        {
        }

        // PUT: api/Character/5
        public void Put(int pId, [FromBody]ICharacter pValue)
        {
        }

        // DELETE: api/Character/5
        public void Delete(int pId)
        {
        }
    }
}
