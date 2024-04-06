using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typhoon.Core;
using Typhoon.Core.DTOs;
using Typhoon.Core.Filters;
using Typhoon.Core.Repositories;
using Typhoon.Service;
using Typhoon.Service.Responses;

namespace Typhoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController<TEntity, TCreateDto, TUpdateDto, TResult, TFilter, TRepository> : ControllerBase
        where TEntity : BaseEntity
        where TCreateDto : IDto
        where TUpdateDto : IUpdateDto
        where TResult : IDto
        where TFilter : BaseFilter<TEntity>
         where TRepository : IRepository<TEntity>
    {
        private readonly IService<TEntity, TCreateDto, TUpdateDto, TResult, TRepository> _service;

        public BaseController(IService<TEntity, TCreateDto, TUpdateDto, TResult, TRepository> service)
        {
            this._service = service;
        }
        /// <summary>
        /// Gets the all the entities.
        /// </summary>
        /// <returns>All entities.</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<BaseResponse>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        /// <summary>
        /// Gets the entity by identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity</returns>
        /// <response code="200">Returns the selected entity</response>
        /// <response code="400">If the entity is null</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse>> GetByIdAsync(int id)
        {
            return await _service.GetAsync(id);
        }

        /// <summary>
        /// Lists filtered all entities
        /// </summary>  
        /// <returns>List of entities.</returns>
        /// <response code="200">Returns the selected entity</response>     
        [HttpGet("getFilteredList")]
        public async Task<ActionResult<BaseResponse>> ListAsync([FromQuery] TFilter filter)
        {
            return await _service.GetFilteredListAsync(filter);
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>  
        /// <returns>List of entities.</returns>
        /// <response code="200">Returns the selected entity</response>   
        /// <response code="400">If error occured</response>  
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostAsync([FromBody] TCreateDto createDto)
        {
            var response = await _service.CreateAsync(createDto);
            if (!response.Success)
            {
                if (response is ValidationErrorResponse)
                {
                    ModelState.Clear();
                    var errorRes = (ValidationErrorResponse)response;
                    foreach (var error in errorRes.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }
            }

            return response;
        }

        /// <summary>
        /// Updates an entity by identifier.
        /// </summary>  
        /// <param name="id">Entity identifier.</param>
        /// <param name="categoryUpdateDto">Updated entity data</param>
        /// <returns>Updated entity</returns>
        /// <response code="200">Updates entity</response>
        /// <response code="400">If error occured</response>   
        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse>> PutAsync(int id, [FromBody] TUpdateDto updateDeto)
        {
            if (id != updateDeto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            var response = await _service.UpdateAsync(id, updateDeto);

            if (!response.Success)
            {
                if (response is ValidationErrorResponse)
                {
                    ModelState.Clear();
                    var errorRes = (ValidationErrorResponse)response;
                    foreach (var error in errorRes.ValidationErrors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }
            }

            return response;
        }


        /// <summary>
        /// Deletes an entity by identifier
        /// </summary>  
        /// <param name="id">Entity identifier.</param>   
        /// <returns>No content</returns>
        /// <response code="200">Deletes entity</response>
        /// <response code="400">If error occured</response>   
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse>> DeleteAsync(int id)
        {
            return await _service.SoftDeleteAsync(id);
        }
    }
}
