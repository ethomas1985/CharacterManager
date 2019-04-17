using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Pathfinder.Api.Searching;
using Pathfinder.Enums;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model.Item;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Controllers
{
    public class ItemController : AbstractSearchController<IItem>
    {
        public ItemController(IRepository<IItem> pSpellRepository)
        {
            LogTo.Debug($"{nameof(SpellBookController)}|ctor");

            ItemRepository = pSpellRepository;

            FacetManager
                .Register(nameof(IItem.ItemType), "Item Type",
                          FacetManager<IItem>.CreateStandardFacetFunction(x => x.ItemType), FilterForItemType);
        }

        private IRepository<IItem> ItemRepository { get; }

        // GET: api/Item
        public IEnumerable<IItem> Get()
        {
            return ItemRepository.GetAll();
        }

        // GET: api/Item/5
        public IItem Get(string pName)
        {
            return ItemRepository.Get(pName);
        }

        // POST: api/Item
        public void Post([FromBody] IItem pValue)
        {
            ItemRepository.Insert(pValue);
        }

        // DELETE: api/Item/5
        public void Delete(string pName)
        {
            throw new NotSupportedException();
        }

        protected override IQueryable<IItem> GetQueryable()
        {
            return ItemRepository.GetQueryable();
        }

        private static IQueryable<IItem> FilterForItemType(IQueryable<IItem> pQueryable, SearchChip pSearchChip)
        {
            if (pSearchChip == null || !Enum.TryParse(pSearchChip.Value, out ItemType itemType))
            {
                return pQueryable;
            }

            return pQueryable.Where(x => x.ItemType == itemType);
        }
    }
}
