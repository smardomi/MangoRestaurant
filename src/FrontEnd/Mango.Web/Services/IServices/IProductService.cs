using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int productId);
        Task<ProductDto> CreateProductAsync(ProductDto product,string? accessToken);
        Task<ProductDto> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(int productId);
    }
}
