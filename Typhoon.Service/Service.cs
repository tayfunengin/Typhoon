using AutoMapper;
using Typhoon.Core;
using Typhoon.Core.DTOs;
using Typhoon.Core.Filters;
using Typhoon.Core.Repositories;
using Typhoon.Service.Responses;

namespace Typhoon.Service
{
    public class Service<TEntity, TCreateDto, TUpdateDto, TResult, TRepository> : IService<TEntity, TCreateDto, TUpdateDto, TResult, TRepository>
        where TEntity : BaseEntity
        where TCreateDto : IDto
        where TUpdateDto : IUpdateDto
        where TResult : IDto
        where TRepository : IRepository<TEntity>
    {

        private readonly TRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Service(TRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BaseResponse> CreateAsync(TCreateDto createDto)
        {
            // TODO: validator ekle.
            var response = new BaseEntityResponse<TResult>(true);

            var entity = _mapper.Map<TEntity>(createDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChanges();

            response.Data = _mapper.Map<TResult>(entity);

            return response;
        }

        public async Task<BaseResponse> CreateRangeAsync(IEnumerable<TCreateDto> createDtos)
        {
            var response = new BaseEntityResponse<IEnumerable<TResult>>(true);

            var entities = _mapper.Map<IEnumerable<TEntity>>(createDtos);
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.SaveChanges();


            response.Data = _mapper.Map<IEnumerable<TResult>>(entities);

            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseEntityResponse<TResult>(true);

            var entity = await _repository.FindAsync(id);
            if (entity is null)
                throw new Exception("Entity Not found");

            _repository.Remove(entity);
            await _unitOfWork.SaveChanges();

            return response;
        }

        public async Task<BaseResponse> SoftDeleteAsync(int id)
        {
            var response = new BaseEntityResponse<TResult>(true);

            var entity = await _repository.FindAsync(id);
            if (entity is null)
                throw new Exception("Entity Not found");
            else
                entity.Deleted = true;

            await _unitOfWork.SaveChanges();

            return response;
        }

        public async Task<BaseResponse> DeleteRangeAsync(IEnumerable<int> ids)
        {
            var response = new BaseEntityResponse<TResult>(true);

            var entityList = new List<TEntity>();
            foreach (var id in ids)
            {
                var result = await _repository.FindAsync(id);
                if (result is null)
                    throw new Exception("Not found");
                else
                {
                    result.Deleted = true;
                    entityList.Add(result);
                }
            }

            _repository.RemoveRange(entityList);
            await _unitOfWork.SaveChanges();

            return response;
        }

        public async Task<BaseResponse> GetAllAsync()
        {
            var response = new BaseEntityResponse<IEnumerable<TResult>>(true);

            var result = await _repository.GetAllAsync<TResult>();
            if (result != null)
                response.Data = result;

            return response;
        }

        public virtual async Task<BaseResponse> GetAsync(int id)
        {
            var response = new BaseEntityResponse<TResult>(true);

            var result = await _repository.GetAsync(id);
            if (result != null)
                response.Data = _mapper.Map<TResult>(result);

            return response;
        }

        public async Task<BaseResponse> GetFilteredListAsync(BaseFilter<TEntity> filter)
        {
            //TODO: filtered response ekle
            var response = new BaseEntityResponse<IEnumerable<TResult>>(true);

            filter ??= new BaseFilter<TEntity>();
            var result = await _repository.ListAsync<TResult>(filter);

            response.Data = result;
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, TUpdateDto updateDto)
        {
            var response = new BaseEntityResponse<TUpdateDto>(true);

            var entity = await _repository.GetAsync(id);
            if (entity is null)
                throw new Exception("Not found");

            entity.UpdatedDate = DateTime.Now;
            _mapper.Map(updateDto, entity);
            await _unitOfWork.SaveChanges();
            response.Data = updateDto;

            return response;
        }

        public async Task<BaseResponse> UpdateAsync(IEnumerable<TUpdateDto> updateDtos)
        {
            var response = new BaseEntityResponse<TResult>(true);


            var entityList = new List<TEntity>();
            foreach (var source in updateDtos)
            {
                var result = await _repository.GetAsync(source.Id);
                if (result is null)
                    throw new Exception("Not found");
                else
                {
                    result.UpdatedDate = DateTime.Now;
                    entityList.Add(result);
                }
            }

            _mapper.Map(updateDtos, entityList);

            await _unitOfWork.SaveChanges();

            return response;
        }
    }
}
