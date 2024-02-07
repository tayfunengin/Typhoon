using Typhoon.Core;
using Typhoon.Core.DTOs;
using Typhoon.Core.Filters;
using Typhoon.Core.Repositories;

namespace Typhoon.Service
{
    public interface IService<TEntity, TCreateDto, TUpdateDto, TResult, TRepository>
        where TEntity : BaseEntity
        where TCreateDto : IDto
        where TUpdateDto : IUpdateDto
        where TResult : IDto
        where TRepository : IRepository<TEntity>
    {
        Task<BaseResponse> CreateAsync(TCreateDto source);
        Task<BaseResponse> CreateRangeAsync(IEnumerable<TCreateDto> sources);

        Task<BaseResponse> GetAsync(int id);

        Task<BaseResponse> GetAllAsync();

        Task<BaseResponse> DeleteAsync(int id);

        Task<BaseResponse> DeleteRangeAsync(IEnumerable<int> ids);

        Task<BaseResponse> UpdateAsync(int id, TUpdateDto source);
        Task<BaseResponse> UpdateAsync(IEnumerable<TUpdateDto> sources);

        Task<BaseResponse> GetFilteredListAsync(BaseFilter<TEntity> filter);
        Task<BaseResponse> SoftDeleteAsync(int id);
    }
}
