using Typhoon.Core.Filters;
using Typhoon.Domain.Entities;

namespace Typhoon.Domain.Filters
{
    public class CategoryFilter : BaseFilter<Category>
    {
        public string? CategoryName { get; set; }
        public override IQueryable<Category> Apply(IQueryable<Category> query)
        {

            if (!string.IsNullOrEmpty(CategoryName))
                query = query.Where(x => x.Name.Contains(CategoryName));

            return base.Apply(query);
        }
    }
}
