using AutoMapper;
using Typhoon.Domain.DTOs.Category;
using Typhoon.Domain.Entities;

namespace Typhoon.Domain.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
