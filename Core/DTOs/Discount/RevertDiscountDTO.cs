using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Discount
{
    public class RevertDiscountDTO
    {
        public IEnumerable<int> ProductIds { get; set; }
    }
}
