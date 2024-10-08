﻿using Core.Models;
using Core.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.SignalR;
using ECommerceAPI;

namespace Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private readonly IHubContext<StockHub> _hubContext;
        //private readonly HttpClient _httpClient;

        public ProductService(IProductRepository productRepository, IMemoryCache cache /*HttpClient httpClient*/)

        {
            _productRepository = productRepository;
            _cache = cache;
            //_httpClient = httpClient;
        }

        public async Task UpdateStockLevel(string productId, int newStockLevel)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveStockUpdate", productId, newStockLevel);
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            product.OriginalPrice = product.Price;
            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            const string cacheKey = "allProducts";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
            {
                products = await _productRepository.GetAllAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));
                _cache.Set(cacheKey, products, cacheEntryOptions);
            }
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            product.OriginalPrice = product.Price;
            await _productRepository.UpdateAsync(product);
            return product;
        }

        public async Task ApplyDiscountToProductsAsync(IEnumerable<int> productIds, decimal discountPercentage)
        {
            var products = await _productRepository.GetProductsByIdsAsync(productIds);
            foreach (var product in products)
            {
                product.Price -= product.Price * (discountPercentage / 100);
                await _productRepository.UpdateAsync(product);
            }
            await _productRepository.SaveChangesAsync();
        }

        public async Task RevertDiscountFromProductsAsync(IEnumerable<int> productIds)
        {
            var products = await _productRepository.GetProductsByIdsAsync(productIds);
            foreach (var product in products)
            {
                product.Price = product.OriginalPrice;
                await _productRepository.UpdateAsync(product);
            }
            await _productRepository.SaveChangesAsync();
        }
    }
}
