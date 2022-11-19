using Mange.Services.ProductAPI.Entities;
using Mange.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mange.Services.ProductAPI.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
