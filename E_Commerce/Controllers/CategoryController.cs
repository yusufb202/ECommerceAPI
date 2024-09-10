using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("WithProducts")]
        public async Task<IActionResult> GetAllCategoriesWithProducts()
        {
            var categories = await _categoryService.GetAllWithProductsAsync();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("GetByProductId")]
        public async Task<IActionResult> GetCategoryByProductId(int productId)
        {
            var categoryName = await _categoryService.GetCategoryByProductIdAsync(productId);
            return Ok(categoryName);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                Products = new List<Product>()
            };

            var newCategory = await _categoryService.AddAsync(category);
            return Ok(newCategory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateDTO categoryUpdateDTO)
        {
            var category = await _categoryService.GetByNameAsync(categoryUpdateDTO.CategoryName);
            if (category == null)
            {
                return NotFound($"Category with name {categoryUpdateDTO.CategoryName} not found.");
            }

            category.Name = categoryUpdateDTO.NewName;
            var updatedCategory = await _categoryService.UpdateAsync(category);
            return Ok(updatedCategory);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deletedCategory = await _categoryService.DeleteAsync(id);
            if (deletedCategory == null)
            {
                return NotFound();
            }
            return Ok(deletedCategory);
        }

        [HttpDelete("CategoryWithProducts/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryWithProducts(int categoryId)
        {
            try
            {
                await _categoryService.DeleteCategoryWithProductsAsync(categoryId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
