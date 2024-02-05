using Typhoon.Core.DTOs;

namespace Typhoon.Domain.DTOs.Category
{
    public class CategoryUpdateDto : BaseCategoryDto, IUpdateDto
    {
        public int Id { get; set; }
    }
}
