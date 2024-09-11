﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ApplyDiscountDTO
    {
        public IEnumerable<int> ProductIds { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
