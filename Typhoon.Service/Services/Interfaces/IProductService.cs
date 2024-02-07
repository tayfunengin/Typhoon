using Typhoon.Domain.DTOs.Product;
using Typhoon.Domain.Entities;
using Typhoon.Domain.Repositories;

namespace Typhoon.Service.Services.Interfaces
{
    public interface IProductService : IService<Product, ProductCreateDto, ProductUpdateDto, ProductDto, IProductRepository>
    {
    }
}
