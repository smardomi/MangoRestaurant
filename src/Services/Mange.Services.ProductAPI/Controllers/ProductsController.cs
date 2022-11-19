using Mange.Services.ProductAPI.Filters;
using Mange.Services.ProductAPI.Models;
using Mange.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mange.Services.ProductAPI.Controllers
{
    [ApiController]
    [ApiResultFilter]
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


    }
}
