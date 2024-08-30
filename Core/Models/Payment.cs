using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PaymentRequest
    {
        public string? Gateway { get; set; }

        public decimal Amount { get; set; }

        public string? Currency { get; set; }

        public string? Description { get; set; }

    }

    public class PaymentResponse
    {
        public bool Success { get; set; }

        public string? TransactionId { get; set; }

        public string? Message { get; set; }
    }
}
