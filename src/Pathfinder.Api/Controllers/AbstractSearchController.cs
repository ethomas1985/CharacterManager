using System.Linq;
using System.Web.Http;
using Pathfinder.Api.Searching;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Controllers {
    public abstract class AbstractSearchController<T> : ApiController, ISearchController<T> where T : INamed
    {
        protected AbstractSearchController()
        {
            FacetManager = new FacetManager<T>();
        }

        protected FacetManager<T> FacetManager { get; }

        protected abstract IQueryable<T> GetQueryable();

        [HttpPost]
        public SearchResults<T> Search(SearchCriteria pCriteria)
        {
            var chips = pCriteria.Chips?.Select(x => x.ToString()) ?? new string[0];
            LogTo.Info("REQUEST|SearchText|{SearchText}|Chips|{Chips}", pCriteria.SearchText, chips);
            var queryable =
                FacetManager.Filter(GetQueryable(), pCriteria.Chips);

            if (!string.IsNullOrWhiteSpace(pCriteria.SearchText))
            {
                queryable = queryable.Where(x => x.Name.Contains(pCriteria.SearchText));
            }

            if (pCriteria.Chips?.Any() ?? false)
            {
                queryable = FacetManager.Filter(queryable, pCriteria.Chips);
            }

            var results = queryable.ToList().OrderBy(x => x.Name).ToList();
            var facets = FacetManager.Build(results, pCriteria.Chips);

            var searchResults = new SearchResults<T>
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

    }
}
