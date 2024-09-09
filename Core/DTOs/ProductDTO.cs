using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength =2)]
        public string? Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? Category { get; set; }
    }

    public class CreateProductDTO
    {
        [Required]
        [StringLength (100, MinimumLength = 2)]
        public string? Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int Stock { get; set; }

        public string? CategoryName { get; set; }
    }

    public class UpdateProductDTO
    {
        [Required]
        [StringLength(100, MinimumLength =2)]
        public string? Name{ get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? CategoryName { get; set; }
    }
}
