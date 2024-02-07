using System.Linq.Dynamic.Core;

namespace Typhoon.Core.Filters
{
    public class BaseFilter<TEntity> where TEntity : BaseEntity
    {
        public bool IgnoreSkipTake { get; set; } = false;
        public int Page { get; set; } = 1;
        public int RecordsToTake { get; set; } = 10;
        public string OrderBy { get; set; } = "Id Desc";

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            query = query.OrderBy(OrderBy);

            if (!IgnoreSkipTake)
                query = query.Skip((Page - 1) * RecordsToTake).Take(RecordsToTake);

            return query;

        }
    }
}
