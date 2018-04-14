using System.Web.Http;
using Pathfinder.Interface.Model;

namespace Pathfinder.Api.Controllers
{
    public class CharacterController : ApiController
    {
		[HttpGet]
		public ICharacter Get(string id)
		{
			return null;
		}
    }
}
