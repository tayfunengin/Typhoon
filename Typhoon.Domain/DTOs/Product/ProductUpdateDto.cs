using Typhoon.Core.DTOs;

namespace Typhoon.Domain.DTOs.Product
{
    public class ProductUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
