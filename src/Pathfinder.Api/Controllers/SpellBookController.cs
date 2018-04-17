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
			SpellsRepository =
				PathfinderConfiguration.Instance
					.CreatePathfinderManager(Path.GetFullPath("."))
					.Get<IRepository<ISpell>>();

			FacetManager = new FacetManager<ISpell>()
				.Register(nameof(ISpell.School), CreateFacetForSchool, FilterForMagicSchool)
				.Register("Class", "Available to", CreateFacetForClass, FilterForClass);
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
			IEnumerable<Facet> facets = FacetManager.Build(results, pCriteria.Facets);

			var searchResults = new SearchResults<ISpell>
			{
				SearchText = pCriteria.SearchText,
				Facets = facets,
				Results = results.Take(20),
				Count = results.Count
			};

			var chips = pCriteria.Facets?.Where(x => x.Buckets.Any(y => y.Selected)).Select(x => $"{x.Name}:'{x.Buckets.First(y => y.Selected).Value}'")
				?? new string[0];

			LogTo.Info("RESPONSE|SearchText|{SearchText}|Chips|{Chips}|Results|{Results}", searchResults.SearchText, chips, results.Count);
			return searchResults;
		}

		private static IEnumerable<Bucket> CreateFacetForSchool(IEnumerable<ISpell> pResults)
		{
			return pResults
				.GroupBy(x => x.School)
				.Select(g => new Bucket(g.Key.ToString(), g.Count()))
				.ToList();
		}

		private IQueryable<ISpell> FilterForMagicSchool(IQueryable<ISpell> pQueryable, Facet pFacet)
		{
			var selectedBucket = pFacet.Buckets.FirstOrDefault(x => x.Selected);
			if (selectedBucket == null)
			{
				return pQueryable;
			}

			if (!Enum.TryParse(selectedBucket.Value, out MagicSchool outValue))
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

		private IQueryable<ISpell> FilterForClass(IQueryable<ISpell> pQueryable, Facet pFacet)
		{
			var selectedBucket = pFacet.Buckets.FirstOrDefault(x => x.Selected);
			if (selectedBucket == null)
			{
				return pQueryable;
			}

			var className = selectedBucket.Value;

			return pQueryable.Where(x => x.LevelRequirements.ContainsKey(className));
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

		public IEnumerable<Facet> Build(IEnumerable<T> pCollection, IEnumerable<Facet> pOriginalFacets)
		{
			var originalFacets = pOriginalFacets?.ToDictionary(k=> k.Name) ?? new Dictionary<string, Facet>();
			var collection = pCollection.ToList();

			return Factories.Select(generateNewFacet);

			Facet generateNewFacet(FacetFactory pFactory)
			{
				var newFacet = pFactory(collection);

				if (originalFacets.TryGetValue(newFacet.Name, out var oldFacet))
				{
					var oldBuckets = new HashSet<string>(oldFacet.Buckets.Where(x => x.Selected).Select(k=>k.Value));
					foreach (var bucket in newFacet.Buckets.Where(x => oldBuckets.Contains(x.Value)))
					{
						bucket.Selected = true;
					}
				}

				return newFacet;
			}
		}

		public IQueryable<T> Filter(IQueryable<T> pQueryable, IEnumerable<Facet> pFacets)
		{
			return pFacets != null
				? pFacets.Aggregate(pQueryable, filterOnFacet)
				: pQueryable;

			IQueryable<T> filterOnFacet(IQueryable<T> queryable, Facet facet)
			{
				var success = Filters.TryGetValue(facet.Id, out var filter);
				if (!success || !facet.Buckets.Any(x => x.Selected))
				{
					return queryable;
				}

				return filter(queryable, facet);
			}
		}
	}
}
