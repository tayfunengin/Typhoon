using Typhoon.Core.Filters;
using Typhoon.Domain.Entities;

namespace Typhoon.Domain.Filters
{
    public class ProductFilter : BaseFilter<Product>
    {
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public override IQueryable<Product> Apply(IQueryable<Product> query)
        {
            if (!string.IsNullOrEmpty(Brand))
                query = query.Where(x => x.Brand.Contains(Brand));

            if (!string.IsNullOrEmpty(Category))
                query = query.Where(x => x.Category.Name.Contains(Category));

            return base.Apply(query);
        }
    }
}
