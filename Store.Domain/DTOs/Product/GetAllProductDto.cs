using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.DTOs.Product
{
    public class GetAllProductDto
    {
        public string ProductName { get; set; } = null!;

        public string CategoryName { get; set; }

        public decimal Price { get; set; }
    }
}
