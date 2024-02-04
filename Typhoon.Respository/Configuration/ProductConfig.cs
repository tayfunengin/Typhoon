using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Typhoon.Domain.Entities;

namespace Typhoon.Respository.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price).HasPrecision(precision: 9, scale: 2);
            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
