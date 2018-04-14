using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Utilities;

namespace Pathfinder.Api.Controllers
{
	public class SpellBookController : ApiController
	{
		public SpellBookController()
		{
			SpellsRepository =
				PathfinderConfiguration.Instance
					.CreatePathfinderManager(HttpRuntime.BinDirectory)
					.Get<IRepository<ISpell>>();
		}

		public IRepository<ISpell> SpellsRepository { get; }

		[HttpPost]
		public SearchResults<ISpell> Search([FromBody] SearchCriteria pCriteria)
		{
			LogTo.Info("REQUEST|SearchText|{SearchText}", pCriteria.SearchText);
			var results = SpellsRepository
				.GetQueryable()
				.Where(x => x.Name.Contains(pCriteria.SearchText))
				.ToList();
			IEnumerable<Facet> facets = GetFacets(results);

			var searchResults = new SearchResults<ISpell>
			{
				SearchText = pCriteria.SearchText,
				Facets = facets,
				Results = results.Take(20),
				Count = results.Count
			};

			LogTo.Info("RESPONSE|SearchText|{SearchText}|Results|{Results}", searchResults.SearchText, results.Count);
			return searchResults;
		}


		private IEnumerable<Facet> GetFacets(IEnumerable<ISpell> pResults)
		{
			var results = pResults.ToList();
			var facets = new List<Facet>
			{
				createFacetForSchool(),
				createFacetForClass(),
				createFacetForLevel()
			};
			return facets;

			Facet createFacetForSchool()
			{
				return new Facet(
					nameof(ISpell.School),
					results.GroupBy(x => x.School).Select(g => new Bucket(g.Key.ToString(), g.Count())));
			}

			Facet createFacetForClass()
			{
				var classes = results.SelectMany(x => x.LevelRequirements.Keys).Distinct();
				var buckets = classes.Select(x => new Bucket(x, results.Count(y => y.LevelRequirements.ContainsKey(x))));

				return new Facet("Available to", buckets);
			}

			Facet createFacetForLevel()
			{
				var classes = results.SelectMany(x => x.LevelRequirements.Values).Distinct();
				var buckets = classes.Select(x => new Bucket($"{x}", results.Count(y => y.LevelRequirements.Values.Contains(x))));

				return new Facet("Available to", buckets);
			}
		}
	}

	public class SearchCriteria
	{
		public string SearchText { get; set; }
		public IEnumerable<Facet> Facets { get; set; }
	}

	public class SearchResults<T>
	{
		public string SearchText { get; set; }
		public IEnumerable<Facet> Facets { get; set; }
		public IEnumerable<T> Results { get; set; }
		public int Count { get; set; }
	}

	public struct Facet
	{
		public Facet(string pName, IEnumerable<Bucket> pBuckets)
		{
			Name = pName;
			Buckets = pBuckets;
		}

		public string Name { get; }

		public IEnumerable<Bucket> Buckets { get; }
	}

	public struct Bucket
	{
		public Bucket(string pValue, int pCount, bool pSelected = false)
		{
			Value = pValue;
			Count = pCount;
			Selected = pSelected;
		}

		public string Value { get; }
		public int Count { get; }
		public bool Selected { get; }
	}
}
