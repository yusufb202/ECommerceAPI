﻿using Core.DTOs;
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

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            var newCategory = await _categoryService.AddAsync(category);
            return Ok(newCategory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var updatedCategory = await _categoryService.UpdateAsync(category);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deletedCategory = await _categoryService.DeleteAsync(id);
            return Ok(deletedCategory);
        }

        [HttpGet("GetByProductId")]
        public async Task<IActionResult> GetCategoryByProductId(int productId)
        {
            var categoryName = await _categoryService.GetCategoryByProductIdAsync(productId);
            return Ok(categoryName);
        }
    }
}
