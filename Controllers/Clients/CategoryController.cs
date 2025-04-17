using HeThongMoiGioiDoCu.Dtos.Category;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Mappers;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Repository;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeThongMoiGioiDoCu.Controllers.Clients
{
    [Route("api/category")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly AccountService _accountService;
        public CategoryController(ICategoryRepository categoryRepository, AccountService accountService)
        {
            _categoryRepository = categoryRepository;
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _categoryRepository.GetListCategory();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
                CreatedAt = DateTime.Now
            };

            var createdCategory = await _categoryRepository.AddCategory(category);
            var categoryDto = createdCategory.MapToCategoryDto();

            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryID }, categoryDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            try
            {
                return await _categoryRepository.GetCategoryById(id);
            }
            catch (Exception e)
            {
                return NotFound("Category not found!");

            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory([FromRoute] int id, [FromBody] Category category)
        {
            var newCategory = await _categoryRepository.UpdateCategory(id, category.CategoryName);

            return newCategory != null ? newCategory : NotFound("Category not found!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var categoryId = await _categoryRepository.DeleteCategory(id);

            return categoryId != null ? Ok("Delete success!") : NotFound("Category not found!");
        }
    }
}
