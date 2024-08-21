using System.Linq.Expressions;

namespace Rere.Core.Repositories;

public abstract class SearchQuery<T>
{
    public Expression<Func<T, bool>>? SearchCriteria { get; set; }
}