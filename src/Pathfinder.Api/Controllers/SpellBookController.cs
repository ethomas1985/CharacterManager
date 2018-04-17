using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
					.CreatePathfinderManager(Path.GetFullPath("."))
					.Get<IRepository<ISpell>>();

			FacetManager = new FacetManager<ISpell>()
				.Register(nameof(ISpell.School), CreateFacetForSchool, FilterForMagicSchool)
				.Register("Class", "Available to", CreateFacetForClass, FilterForClass)
				.Register("Level", "Available at", CreateFacetForLevel, FilterForLevel);
		}

		public IRepository<ISpell> SpellsRepository { get; }

		private FacetManager<ISpell> FacetManager { get; }

		[HttpPost]
		public SearchResults<ISpell> Search([FromBody] SearchCriteria pCriteria)
		{
			LogTo.Info("REQUEST|SearchText|{SearchText}", pCriteria.SearchText);
			var queryable =
				FacetManager.Filter(
					SpellsRepository.GetQueryable(),
					pCriteria.Facets);

			var results = queryable
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

			var chips = pCriteria.Facets?.Where(x => x.Buckets.Any(y => y.Selected)).Select(x => $"{x.Name}:'{x.Buckets.First(y=>y.Selected).Value}'")
				?? new string[0];
			LogTo.Info("RESPONSE|SearchText|{SearchText}|Chips|{Chips}|Results|{Results}", searchResults.SearchText, chips, results.Count);
			return searchResults;
		}

		private IEnumerable<Facet> GetFacets(IEnumerable<ISpell> pResults)
		{
			return FacetManager.Build(pResults);
		}

		private static IEnumerable<Bucket> CreateFacetForSchool(IEnumerable<ISpell> pResults)
		{
			return pResults
				.GroupBy(x => x.School)
				.Select(g => new Bucket(g.Key.ToString(), g.Count()));
		}

		private IQueryable<ISpell> FilterForMagicSchool(IQueryable<ISpell> pQueryable, Facet pFacet)
		{
			var selectedBucket = pFacet.Buckets.FirstOrDefault(x => x.Selected);
			if (selectedBucket == null)
			{
				return pQueryable;
			}

			return pQueryable.Where(x => x.School.ToString().Equals(selectedBucket.Value));
		}

		private static IEnumerable<Bucket> CreateFacetForClass(IEnumerable<ISpell> pResults)
		{
			var results = pResults as List<ISpell> ?? pResults.ToList();
			var classes = results.SelectMany(x => x.LevelRequirements.Keys).Distinct();
			return classes
				.Select(x => new Bucket(x, results.Count(y => y.LevelRequirements.ContainsKey(x))));
		}

		private IQueryable<ISpell> FilterForClass(IQueryable<ISpell> pPqueryable, Facet pPfacet)
		{
			throw new NotImplementedException();
		}

		private static IEnumerable<Bucket> CreateFacetForLevel(IEnumerable<ISpell> pResults)
		{
			var results = pResults as List<ISpell> ?? pResults.ToList();
			var classes = results.SelectMany(x => x.LevelRequirements.Values).Distinct();
			return classes
				.Select(x => new Bucket($"{x}", results.Count(y => y.LevelRequirements.Values.Contains(x))));
		}

		private IQueryable<ISpell> FilterForLevel(IQueryable<ISpell> pPqueryable, Facet pPfacet)
		{
			throw new NotImplementedException();
		}
	}

	public delegate IEnumerable<Bucket> BucketCollectionFactory<in T>(IEnumerable<T> pCollection);
	public delegate IQueryable<T> ApplyFacetFilter<T>(IQueryable<T> pQueryable, Facet pFacet);
	public class FacetManager<T>
	{
		private delegate Facet FacetFactory(IEnumerable<T> pCollection);

		private List<FacetFactory> Factories { get; } = new List<FacetFactory>();
		private Dictionary<string, ApplyFacetFilter<T>> Filters { get; } = new Dictionary<string, ApplyFacetFilter<T>>();

		public FacetManager<T> Register(string pName, BucketCollectionFactory<T> pBucketCollectionFactory, ApplyFacetFilter<T> pFilter)
		{
			return Register(pName, pName, pBucketCollectionFactory, pFilter);
		}

		public FacetManager<T> Register(string pId, string pName, BucketCollectionFactory<T> pFactory, ApplyFacetFilter<T> pFilter)
		{
			Factories.Add(x => new Facet(pId, pName, pFactory(x)));
			Filters[pId] = pFilter;

			return this;
		}

		public IEnumerable<Facet> Build(IEnumerable<T> pCollection)
		{
			var collection = pCollection.ToList();

			return Factories.Select(x => x(collection));
		}

		public IQueryable<T> Filter(IQueryable<T> pQueryable, IEnumerable<Facet> pFacets)
		{
			return pFacets != null
				? pFacets.Aggregate(pQueryable, FilterOnFacet)
				: pQueryable;

			IQueryable<T> FilterOnFacet(IQueryable<T> queryable, Facet facet)
			{
				ApplyFacetFilter<T> filter;
				var success = Filters.TryGetValue(facet.Id, out filter);
				if (!success || !facet.Buckets.Any(x => x.Selected))
				{
					return queryable;
				}

				return filter(queryable, facet);
			}
		}
	}
}
