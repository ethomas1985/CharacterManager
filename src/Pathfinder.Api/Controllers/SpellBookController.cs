using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Pathfinder.Api.Searching;
using Pathfinder.Enums;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Controllers
{
    public class SpellBookController : AbstractSearchController<ISpell>
    {
        public SpellBookController(IRepository<ISpell> pSpellRepository)
        {
            LogTo.Debug($"{nameof(SpellBookController)}|ctor");

            SpellsRepository = pSpellRepository;

            FacetManager
                .Register(nameof(ISpell.School), "Magic School",
                          FacetManager<ISpell>.CreateStandardFacetFunction(x => x.School), FilterForMagicSchool)
                .Register("Class", "Available to", CreateFacetForClass, FilterForClass);
        }

        private IRepository<ISpell> SpellsRepository { get; }

        protected override IQueryable<ISpell> GetQueryable()
        {
            return SpellsRepository.GetQueryable();
        }

        private IQueryable<ISpell> FilterForMagicSchool(IQueryable<ISpell> pQueryable, SearchChip pSearchChip)
        {
            if (pSearchChip == null)
            {
                return pQueryable;
            }

            if (!Enum.TryParse(pSearchChip.Value, out MagicSchool outValue))
            {
                return pQueryable;
            }

            return pQueryable.Where(x => x.School == outValue);
        }

        private static IEnumerable<Bucket> CreateFacetForClass(IEnumerable<ISpell> pResults)
        {
            var results = pResults as List<ISpell> ?? pResults.ToList();
            var classes = results.SelectMany(x => x.LevelRequirements.Keys).Distinct();
            return classes
                .Select(x => new Bucket(x, results.Count(y => y.LevelRequirements.ContainsKey(x))))
                .ToList();
        }

        private IQueryable<ISpell> FilterForClass(IQueryable<ISpell> pQueryable, SearchChip pSearchChip)
        {
            if (pSearchChip == null)
            {
                return pQueryable;
            }

            var className = pSearchChip.Value;

            return pQueryable.Where(x => x.LevelRequirements.ContainsKey(className));
        }
    }
}
