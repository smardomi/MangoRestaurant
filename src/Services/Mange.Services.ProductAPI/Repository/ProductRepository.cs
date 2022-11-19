using AutoMapper;
using Mange.Services.ProductAPI.DbContext;
using Mange.Services.ProductAPI.Entities;
using Mange.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mange.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto product)
        {
           var productToCreateOrUpdate = _mapper.Map<Product>(product);
           if (product.Id > 0)
           {
               _context.Update(productToCreateOrUpdate);
           }
           else
           {
              await _context.AddAsync(productToCreateOrUpdate);
           }

           await _context.SaveChangesAsync();

          return _mapper.Map<ProductDto>(productToCreateOrUpdate);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product == null)
                {
                    return false;
                }

                _context.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
