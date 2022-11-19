using System.ComponentModel.DataAnnotations;

namespace Mange.Services.ProductAPI.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1,1000)]
        public double Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(100)]
        public string ImageUrl { get; set; }

    }
}
