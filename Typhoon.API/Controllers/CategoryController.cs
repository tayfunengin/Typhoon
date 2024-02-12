using Microsoft.AspNetCore.Mvc;
using Typhoon.Core.Repositories;
using Typhoon.Domain.DTOs.Category;
using Typhoon.Domain.Entities;
using Typhoon.Domain.Filters;
using Typhoon.Service;

namespace Typhoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryCreateDto, CategoryUpdateDto, CategoryDto, CategoryFilter, IRepository<Category>>
    {
        public CategoryController(IService<Category, CategoryCreateDto, CategoryUpdateDto, CategoryDto, IRepository<Category>> service) : base(service) { }

    }
}
