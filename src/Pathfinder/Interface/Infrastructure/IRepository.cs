using System.Collections.Generic;
using System.Linq;

namespace Pathfinder.Interface.Infrastructure
{
    public interface IRepository<T>
	{
		IQueryable<T> GetQueryable();

		void Insert(T pValue);
        void Insert(IEnumerable<T> pValues);
        void Update(T pValue);
        void Replace();

        IEnumerable<T> GetAll();
        T Get(string Id);
        //IEnumerable<T> GetList(IEnumerable<IPredicate> pPredicates);
    }

    //public interface IPredicate
    //{
    //    string FieldName { get; set; }
    //    Operator Operator { get; set; }
    //    object Value { get; set; }
    //}

    //public enum Operator
    //{
    //    Equals,
    //    NotEquals,
    //    Like,
    //    LessThan,
    //    LessThanOrGreater,
    //    GreaterThan,
    //    GreaterThanOrGreater
    //}
}
