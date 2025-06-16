using BusinessObject.DTOs;
using DataAccess;
using HaHuuTrung_SE1821_A01_DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryRepo.GetAllAsync();
            var result = categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                Name = c.CategoryName,
                CategoryDesciption = c.CategoryDesciption,
                IsActive = c.IsActive,
                ParentCategoryId = c.ParentCategoryId
            });

            return Ok(result);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(short id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var result = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.CategoryName,
                CategoryDesciption = category.CategoryDesciption,
                IsActive = category.IsActive,
                ParentCategoryId = category.ParentCategoryId
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryDTO dto)
        {
            var category = new Category
            {
                CategoryName = dto.Name,
                CategoryDesciption = dto.CategoryDesciption,
                IsActive = dto.IsActive ?? true,
                ParentCategoryId = dto.ParentCategoryId
            };

            await _categoryRepo.AddAsync(category);

            // Sau khi thêm, trả về dữ liệu mới có ID
            var resultDto = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                Name = category.CategoryName,
                CategoryDesciption = category.CategoryDesciption,
                IsActive = category.IsActive,
                ParentCategoryId = category.ParentCategoryId
            };

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, resultDto);
        }



        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(short id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest("Category ID mismatch");
            }

            var existing = await _categoryRepo.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.CategoryName = categoryDto.Name;
            existing.CategoryDesciption = categoryDto.CategoryDesciption;
            existing.IsActive = categoryDto.IsActive ?? existing.IsActive;
            existing.ParentCategoryId = categoryDto.ParentCategoryId;

            await _categoryRepo.UpdateAsync(existing);
            return NoContent();
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(short id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            await _categoryRepo.DeleteAsync(category);
            return NoContent();
        }
    }
}
