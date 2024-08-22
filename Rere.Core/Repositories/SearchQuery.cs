using System.Linq.Expressions;

namespace Rere.Core.Repositories;

public abstract class SearchQuery<T>
{
    public virtual Expression<Func<T, bool>> SearchCriteria { get; } =
        Expression.Lambda<Func<T, bool>>(Expression.Constant(true), Expression.Parameter(typeof(T)));
}