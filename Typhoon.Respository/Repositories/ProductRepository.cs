using AutoMapper;
using Typhoon.Domain.Entities;
using Typhoon.Domain.Repositories;
using Typhoon.Respository.Context;

namespace Typhoon.Respository.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
