/*using Moq;
using Xunit;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using Core.Models;
using Core.Repositories;
using Service;

public class ProductServiceTests
{
    [Fact]
    public async Task GetProductByIdAsync_ReturnsProduct()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var mockCache = new Mock<IMemoryCache>();

        // Setup the mock repository
        mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(new Product { Id = 1, Name = "Test Product" });

        // Setup the mock cache (if needed)
        object cachedProduct;
        mockCache.Setup(mc => mc.TryGetValue(It.IsAny<object>(), out cachedProduct)).Returns(false);

        var service = new ProductService(mockRepo.Object, mockCache.Object);

        // Act
        var product = await service.GetProductByIdAsync(1);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
        Assert.Equal("Test Product", product.Name);
    }
}
*/