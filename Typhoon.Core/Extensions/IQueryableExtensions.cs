using Typhoon.Core.Filters;

namespace Typhoon.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> ApplyFilter<TEntity>(this IQueryable<TEntity> query, BaseFilter<TEntity> filter) where TEntity : BaseEntity
        {
            query = filter.Apply(query);
            return query;
        }
    }
}
