using Mange.Services.ProductAPI.Filters;
using Mange.Services.ProductAPI.Models;
using Mange.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mange.Services.ProductAPI.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts() => Ok(await _productRepository.GetProducts());

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProducts(int productId) => Ok(await _productRepository.GetProductById(productId));

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductDto product) => Ok(await _productRepository.CreateUpdateProduct(product));

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct(ProductDto product) => Ok(await _productRepository.CreateUpdateProduct(product));

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteProduct(int productId) => Ok(await _productRepository.DeleteProduct(productId));
    }
}
