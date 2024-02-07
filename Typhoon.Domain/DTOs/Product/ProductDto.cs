using Typhoon.Core.DTOs;
using Typhoon.Domain.DTOs.Category;

namespace Typhoon.Domain.DTOs.Product
{
    public class ProductDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public CategoryDto Category { get; set; }
    }
}
