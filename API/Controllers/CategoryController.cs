using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Category;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(IGenericRepository<Category> genericRepository) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Categorys()
        {
            var categorys = await genericRepository.GetAsync();

            return Ok(categorys);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> CategoryById([FromRoute] int id)
        {
            var category = await genericRepository.GetByIdAsync(id);

            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<Category>> CreateCategory(AddCategoryDTO addCategoryDTO)
        {
            var category = new Category
            {
                CategoryName = addCategoryDTO.CategoryName,
                CategoryType = addCategoryDTO.CategoryType,
            };

            await genericRepository.CreateAsync(category);
            await genericRepository.SaveChangesAsync();

            return Ok(category);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> CategoryUpdate([FromRoute] int id, UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await genericRepository.GetByIdAsync(id);

            if (category == null) return NotFound();

            category.CategoryName = updateCategoryDTO.CategoryName;
            category.CategoryType = updateCategoryDTO.CategoryType;

            await genericRepository.UpdateAsync(category);

            await genericRepository.SaveChangesAsync();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> CategoryDelete([FromRoute] int id)
        {
            var category = await genericRepository.GetByIdAsync(id);

            if (category == null) return NotFound();

            await genericRepository.DeleteAsync(category);

            await genericRepository.SaveChangesAsync();

            return Ok(category);
        }

    }
}