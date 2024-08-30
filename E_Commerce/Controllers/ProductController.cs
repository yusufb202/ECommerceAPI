using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IProductService productService, ILogger<ProductController> logger, IHttpClientFactory httpClientFactory)
        {
            _productService = productService;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {
            _logger.LogInformation("Getting all products");
            var products = await _productService.GetAllProductsAsync();
            var productDTOs = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
            });

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /*public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            _logger.LogInformation($"Getting product with id {id}");
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
            };
            return Ok(product);
        }*/
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> AddProduct(CreateProductDTO createProductDTO)
        {
            _logger.LogInformation("Creating a new product");
            var product = new Product
            {
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                Description = createProductDTO.Description,
            };

            var createdProduct= await _productService.AddProductAsync(product);

            var productDTO = new ProductDTO
            {
                Id = createdProduct.Id,
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                Description = createdProduct.Description,
            };

            return CreatedAtAction(nameof(GetProductById), new {id=createdProduct.Id}, productDTO);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
        {
            _logger.LogInformation($"Updating product with id {id}");
            var product = new Product
            {
                Id = id,
                Name = updateProductDTO.Name,
                Price = updateProductDTO.Price,
                Description = updateProductDTO.Description,
            };

            var updatedProduct=await _productService.UpdateProductAsync(product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            var productDTO = new ProductDTO
            {
                Id = updatedProduct.Id,
                Name = updatedProduct.Name,
                Price = updatedProduct.Price,
                Description = updatedProduct.Description,
            };

            return Ok(productDTO);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation($"Deleting product with id {id}");
            var deletedProduct= await _productService.DeleteProductAsync(id);
            if (deletedProduct == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
