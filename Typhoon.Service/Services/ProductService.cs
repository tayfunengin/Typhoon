using AutoMapper;
using Typhoon.Core;
using Typhoon.Core.Repositories;
using Typhoon.Domain.DTOs.Product;
using Typhoon.Domain.Entities;
using Typhoon.Domain.Repositories;
using Typhoon.Service.Responses;
using Typhoon.Service.Services.Interfaces;

namespace Typhoon.Service.Services
{
    public class ProductService : Service<Product, ProductCreateDto, ProductUpdateDto, ProductDto, IProductRepository>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        public async override Task<BaseResponse> GetAsync(int id)
        {
            var response = new BaseEntityResponse<ProductDto>(true);

            var result = await _repository.GetAsync(id, x => x.Category);
            if (result != null)
                response.Data = _mapper.Map<ProductDto>(result);

            return response;
        }
    }
}
