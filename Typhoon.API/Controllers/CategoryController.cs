﻿using Microsoft.AspNetCore.Mvc;
using Typhoon.Core;
using Typhoon.Core.Repositories;
using Typhoon.Domain.DTOs.Category;
using Typhoon.Domain.Entities;
using Typhoon.Domain.Filters;
using Typhoon.Service;

namespace Typhoon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IService<Category, CategoryCreateDto, CategoryUpdateDto, CategoryDto, IRepository<Category>> _service;

        public CategoryController(IService<Category, CategoryCreateDto, CategoryUpdateDto, CategoryDto, IRepository<Category>> service)
        {
            this._service = service;
        }

        /// <summary>
        /// Gets the all the entities.
        /// </summary>
        /// <returns>All entities.</returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<BaseResponse>> GetCategoriesAsync()
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
        public async Task<ActionResult<BaseResponse>> ListAsync([FromQuery] CategoryFilter filter)
        {
            return await _service.GetFilteredListAsync(filter);
        }

        /// <summary>
        /// Creates a new entity
        /// </summary>  
        /// <returns>List of entities.</returns>
        /// <response code="200">Returns the selected entity</response>     
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> PostAsync([FromBody] CategoryCreateDto categoryCreateDto)
        {
            return await _service.CreateAsync(categoryCreateDto);
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
        public async Task<ActionResult<BaseResponse>> PutAsync(int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            return await _service.UpdateAsync(id, categoryUpdateDto);
        }

    }
}
