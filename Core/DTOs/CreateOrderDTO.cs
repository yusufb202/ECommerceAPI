﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class CreateOrderDTO
    {
        [Required]
        public List<CreateOrderItemDTO> Items { get; set; }
    }

    public class CreateOrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
