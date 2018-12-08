using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using Pathfinder.Interface.Infrastructure;
using Pathfinder.Interface.Model;
using Pathfinder.Model;

namespace Pathfinder.Repository
{
	internal interface ISpellPersistenceStore : IRepository<ISpell>
	{
		IEnumerable<ISpell> GetListForClass(IClass pClass);
	}

	public class SpellMongoRepository : ISpellPersistenceStore
	{
		private readonly MongoRepository<Spell> _store;

		public SpellMongoRepository()
		{
			_store = new MongoRepository<Spell>("mongodb://localhost:27017", "pathfinder", "spells");
		}

		private IMongoCollection<Spell> GetCollection()
		{
			return _store.GetCollection();
		}

		private static Spell ConvertToConcrete(ISpell pValue)
		{
			return pValue as Spell
				?? new Spell(pValue.Name,
							 pValue.School,
							 pValue.SubSchools,
							 pValue.MagicDescriptors,
							 pValue.SavingThrow,
							 pValue.Description,
							 pValue.HasSpellResistance,
							 pValue.SpellResistance,
							 pValue.CastingTime,
							 pValue.Range,
							 pValue.LevelRequirements,
							 pValue.Duration,
							 pValue.Components);
		}

		public void Insert(ISpell pValue)
		{
			GetCollection().InsertOne(ConvertToConcrete(pValue));
		}

		public void Insert(IEnumerable<ISpell> pValues)
		{
			GetCollection().InsertMany(pValues.Select(ConvertToConcrete));
		}

		public void Update(ISpell pValue)
		{
			var filter = new FilterDefinitionBuilder<Spell>().Eq(nameof(Spell.Name), pValue.Name);

			GetCollection().ReplaceOne(filter, ConvertToConcrete(pValue));
		}

		public void Replace()
		{
			throw new NotImplementedException();
		}

		public IQueryable<ISpell> GetQueryable()
		{
			return GetCollection().AsQueryable();
		}

		public IEnumerable<ISpell> GetAll()
		{
			return GetCollection()
				.Find(new BsonDocument())
				.ToList();
		}

		public ISpell Get(string pId)
		{
			var filter = new FilterDefinitionBuilder<Spell>().Eq(nameof(Spell.Name), pId);
			return GetCollection()
				.Find(filter)
				.Limit(1)
				.FirstOrDefault();
		}

		//public IEnumerable<ISpell> GetList(IEnumerable<IPredicate> pPredicates)
		//{
		//    var aggregate = GetCollection()
		//        .Aggregate();
		//    foreach (var predicate in pPredicates)
		//    {
		//        var filter = new FilterDefinitionBuilder<Spell>().Eq(x => x.Name, pId);

		//        aggregate.Match()
		//    }

		//    return aggregate
		//        .Match()
		//        .ToList();
		//}

		public IEnumerable<ISpell> GetListForClass(IClass pClass)
		{
			return GetCollection()
				.AsQueryable()
				.Where(x => x.LevelRequirements.Any(y => y.Key == pClass.Name))
				.ToList();
		}
	}
}
