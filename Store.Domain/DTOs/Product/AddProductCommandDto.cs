using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.DTOs.Product
{
    public class AddProductCommandDto
    {
        public string ProductName { get; set; } = null!;

        public int CategoryId { get; set; }

        public decimal Price { get; set; }
    }
}
