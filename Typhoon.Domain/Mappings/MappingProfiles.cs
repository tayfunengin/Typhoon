using AutoMapper;
using Typhoon.Domain.DTOs.Category;
using Typhoon.Domain.DTOs.Product;
using Typhoon.Domain.DTOs.User;
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

            CreateMap<ProductUpdateDto, Product>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
