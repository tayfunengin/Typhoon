using Microsoft.AspNetCore.Mvc;
using Typhoon.Domain.DTOs.Product;
using Typhoon.Domain.Entities;
using Typhoon.Domain.Filters;
using Typhoon.Domain.Repositories;
using Typhoon.Service.Services.Interfaces;

namespace Typhoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController<Product, ProductCreateDto, ProductUpdateDto, ProductDto, ProductFilter, IProductRepository>
    {
        public ProductController(IProductService service) : base(service) { }

    }
}
