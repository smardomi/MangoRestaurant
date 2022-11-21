using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<List<ProductDto>> GetAllProductsAsync(string? accessToken);
        Task<ProductDto?> GetProductByIdAsync(int productId, string? accessToken);
        Task<ProductDto> CreateProductAsync(ProductDto product,string? accessToken);
        Task<ProductDto> UpdateProductAsync(ProductDto product, string? accessToken);
        Task<bool> DeleteProductAsync(int productId, string? accessToken);
    }
}
