using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(string? accessToken)
        {
            return await SendAsync<List<ProductDto>>(new ApiRequest
            {
                ApiType = ApiUtil.ApiType.GET,
                Url = ApiUtil.ProductAPIBase + "/products",
                AccessToken = accessToken
            });
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId, string? accessToken)
        {
            return await SendAsync<ProductDto>(new ApiRequest
            {
                ApiType = ApiUtil.ApiType.GET,
                Url = ApiUtil.ProductAPIBase + "/products/" + productId,
                AccessToken = accessToken
            });
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto product, string? accessToken)
        {
            return await SendAsync<ProductDto>(new ApiRequest
            {
                ApiType = ApiUtil.ApiType.POST,
                Url = ApiUtil.ProductAPIBase + "/products",
                Data = product,
                AccessToken = accessToken
            });
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto product, string? accessToken)
        {
            return await SendAsync<ProductDto>(new ApiRequest
            {
                ApiType = ApiUtil.ApiType.PUT,
                Url = ApiUtil.ProductAPIBase + "/products",
                Data = product,
                AccessToken = accessToken
            });
        }

        public async Task<bool> DeleteProductAsync(int productId, string? accessToken)
        {
            return await SendAsync<bool>(new ApiRequest
            {
                ApiType = ApiUtil.ApiType.DELETE,
                Url = ApiUtil.ProductAPIBase + "/products",
                Data = productId,
                AccessToken = accessToken
            });
        }
    }
}
