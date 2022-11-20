using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<TEntity> GetAllProductsAsync<TEntity>();
        Task<TEntity> GetProductByIdAsync<TEntity>(int productId);
        Task<TEntity> CreateProductAsync<TEntity>(ProductDto product);
        Task<TEntity> UpdateProductAsync<TEntity>(ProductDto product);
        Task<TEntity> DeleteProductAsync<TEntity>(int productId);
    }
}
