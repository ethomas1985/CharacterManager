using System.Web.Http;

namespace Pathfinder.Api.Searching
{
    public interface ISearchController<T>
    {
        SearchResults<T> Search([FromBody] SearchCriteria pCriteria);
    }
}
