using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using Pathfinder.Enums;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Controllers
{
    public class SpellBookController : ApiController
    {
        public SpellBookController()
        {
            LogTo.Debug($"{nameof(SpellBookController)}|ctor");

            SpellsRepository =
                PathfinderConfiguration.Instance
                    .CreatePathfinderManager(Path.GetFullPath("."))
                    .Get<IRepository<ISpell>>();

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
            IEnumerable<Facet> facets = FacetManager.Build(results, pCriteria.Chips);

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

    public delegate IEnumerable<Bucket> BucketCollectionFactory<in T>(IEnumerable<T> pCollection);

    public delegate IQueryable<T> ApplyFacetFilter<T>(IQueryable<T> pQueryable, SearchChip pFacet);

    public class FacetManager<T>
    {
        private delegate Facet FacetFactory(IEnumerable<T> pCollection);

        private Dictionary<string, string> NameToIdMap { get; } = new Dictionary<string, string>();

        private List<FacetFactory> Factories { get; } = new List<FacetFactory>();

        private Dictionary<string, ApplyFacetFilter<T>> Filters { get; } =
            new Dictionary<string, ApplyFacetFilter<T>>();

        public FacetManager<T> Register(string pName, BucketCollectionFactory<T> pBucketCollectionFactory,
            ApplyFacetFilter<T> pFilter)
        {
            return Register(pName, pName, pBucketCollectionFactory, pFilter);
        }

        public FacetManager<T> Register(string pId, string pName, BucketCollectionFactory<T> pFactory,
            ApplyFacetFilter<T> pFilter)
        {
            Factories.Add(x => new Facet(pId, pName, pFactory(x)));
            NameToIdMap[pName] = pId;
            Filters[pId] = pFilter;

            return this;
        }

        public IEnumerable<Facet> Build(IEnumerable<T> pCollection, IEnumerable<SearchChip> pSearchChips)
        {
            var groupedSearchChips = pSearchChips?.ToLookup(k => k.Name);
            var collection = pCollection.ToList();

            return Factories.Select(generateNewFacet);

            Facet generateNewFacet(FacetFactory pFactory)
            {
                var newFacet = pFactory(collection);

                if (groupedSearchChips != null && groupedSearchChips.Contains(newFacet.Name))
                {
                    var oldBuckets = new HashSet<string>(groupedSearchChips[newFacet.Name].Select(k => k.Value));
                    foreach (var bucket in newFacet.Buckets)
                    {
                        bucket.Selected = oldBuckets.Contains(bucket.Value);
                    }
                }

                return newFacet;
            }
        }

        public IQueryable<T> Filter(IQueryable<T> pQueryable, IEnumerable<SearchChip> pSearchChips)
        {
            return pSearchChips != null
                ? pSearchChips.Aggregate(pQueryable, filterOnFacet)
                : pQueryable;

            IQueryable<T> filterOnFacet(IQueryable<T> queryable, SearchChip searchChip)
            {
                var success = NameToIdMap.TryGetValue(searchChip.Name, out var id);
                if (!success)
                {
                    LogTo.Warning(
                        $"{nameof(FacetManager<T>)}|{typeof(T)}.Name|{searchChip.Name}|name not registered");

                    return queryable;
                }

                success = Filters.TryGetValue(NameToIdMap[searchChip.Name], out var filter);
                if (!success)
                {
                    LogTo.Warning(
                        $"{nameof(FacetManager<T>)}|{typeof(T)}.Name|{searchChip.Name}|filter not registered");

                    return queryable;
                }

                LogTo.Debug($"{nameof(FacetManager<T>)}|{typeof(T)}.Name|{searchChip.Name}");
                return filter(queryable, searchChip);
            }
        }
    }
}
