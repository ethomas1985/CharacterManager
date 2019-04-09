using System.Web.Http;
using Microsoft.AspNet.OData;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Controllers.OData
{
    [EnableQuery]
    public class SpellsController : ODataController
    {
        public SpellsController(IRepository<ISpell> pSpellRepository)
        {
            LogTo.Debug($"{nameof(SpellsController)}|ctor|ODATA");
            SpellsRepository = pSpellRepository;
                //PathfinderConfiguration.Instance
                //    .InitializeContainer(new BasicDependencyContainer(), Path.GetFullPath("."))
                //    .Get<IRepository<ISpell>>();
        }

        private IRepository<ISpell> SpellsRepository { get; }

        [Queryable]
        public IHttpActionResult Get()
        {
            LogTo.Debug($"{nameof(SpellsController)}|Get");
            return Ok(SpellsRepository.GetQueryable());
        }
    }
}
