﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        public int Stock { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
