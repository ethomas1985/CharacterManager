using System.Collections.Generic;
using System.Linq;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Searching
{
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
