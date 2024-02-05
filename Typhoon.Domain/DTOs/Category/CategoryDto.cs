using Typhoon.Core.DTOs;

namespace Typhoon.Domain.DTOs.Category
{
    public class CategoryDto : BaseCategoryDto, IDto
    {
        public int Id { get; set; }
    }
}
