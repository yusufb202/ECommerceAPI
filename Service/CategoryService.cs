using Core.Models;
using Core.Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, IProductService productService)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null!;
            }

            // Get the default category
            var defaultCategory = await _categoryRepository.GetByNameAsync("Default");
            if (defaultCategory == null)
            {
                // Create a default category if it doesn't exist
                defaultCategory = new Category { Name = "Default" };
                await _categoryRepository.AddAsync(defaultCategory);
                await _categoryRepository.SaveChangesAsync(); // Save changes to get the default category ID
            }

            // Reassign products to the default category
            var products = await _productRepository.GetByCategoryIdAsync(category.Id);
            foreach (var product in products)
            {
                product.CategoryId = defaultCategory.Id;
                await _productRepository.UpdateAsync(product);
            }

            // Save changes to the context
            await _productRepository.SaveChangesAsync();

            // Delete the category
            return await _categoryRepository.DeleteAsync(id);
        }
        public async Task DeleteCategoryWithProductsAsync(int categoryId)
        {
            var categories = await _categoryRepository.GetAllWithProductsAsync();
            var category = categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            var productIds = category.Products.Select(p => p.Id).ToList();
            // Delete all products associated with the category
            foreach (var productId in productIds)
            {
                await _productService.DeleteProductAsync(productId);
            }

            // Delete the category
            await _categoryRepository.DeleteAsync(categoryId);
        }

        public async Task<string> GetCategoryByProductIdAsync(int productId)
        {
            return await _categoryRepository.GetCategoryByProductIdAsync(productId);
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _categoryRepository.GetByNameAsync(name);
        }

        public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
        {
            return await _categoryRepository.GetAllWithProductsAsync();
        }
    }
}
