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
    public class SpellBookController : ApiController, ISearchController<ISpell>
    {
        public SpellBookController(IRepository<ISpell> pSpellRepository)
        {
            LogTo.Debug($"{nameof(SpellBookController)}|ctor");

            SpellsRepository = pSpellRepository;
                //PathfinderConfiguration.Instance
                //    .CreatePathfinderManager(Path.GetFullPath("."))
                //    .Get<IRepository<ISpell>>();

            FacetManager = new FacetManager<ISpell>()
                .Register(nameof(ISpell.School), "Magic School", CreateFacetForSchool, FilterForMagicSchool)
                .Register("Class", "Available to", CreateFacetForClass, FilterForClass);
        }

        private IRepository<ISpell> SpellsRepository { get; }

        private FacetManager<ISpell> FacetManager { get; }

        [HttpPost]
        public SearchResults<ISpell> Search([FromBody] SearchCriteria pCriteria)
        {
            var chips = pCriteria.Chips?.Select(x => x.ToString()) ?? new string[0];
            LogTo.Info("REQUEST|SearchText|{SearchText}|Chips|{Chips}", pCriteria.SearchText, chips);
            var queryable =
                FacetManager.Filter(
                    SpellsRepository.GetQueryable(),
                    pCriteria.Chips);

            if (!string.IsNullOrWhiteSpace(pCriteria.SearchText))
            {
                queryable = queryable.Where(x => x.Name.Contains(pCriteria.SearchText));
            }

            if (pCriteria.Chips?.Any() ?? false)
            {
                queryable = FacetManager.Filter(queryable, pCriteria.Chips);
            }

            var results = queryable.OrderBy(x => x.Name).ToList();
            var facets = FacetManager.Build(results, pCriteria.Chips);

            var searchResults = new SearchResults<ISpell>
            {
                SearchText = pCriteria.SearchText,
                Facets = facets,
                Results = results.Take(20),
                Count = results.Count
            };

            chips = searchResults.Facets?.Where(x => x.Buckets.Any(y => y.Selected))
                    .SelectMany(x => x.Buckets.Where(y => y.Selected).Select(y => $"{x.Name}: {y.Value}")) ??
                new string[0];
            LogTo.Info("RESPONSE|SearchText|{SearchText}|Chips|{Chips}|Results|{Results}", searchResults.SearchText,
                       chips, results.Count);
            return searchResults;
        }

        private static IEnumerable<Bucket> CreateFacetForSchool(IEnumerable<ISpell> pResults)
        {
            return pResults
                .GroupBy(x => x.School)
                .Select(g => new Bucket(g.Key.ToString(), g.Count()))
                .ToList();
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
