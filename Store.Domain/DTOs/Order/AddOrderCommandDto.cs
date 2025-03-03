using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.DTOs.Order
{
    public class AddOrderCommandDto
    {
        public int? CustomerId { get; set; }

        public byte OrderStatus { get; set; }

        public DateTime OrderDate { get; set; } 

        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public List<Items> Items { get; set; } = new List<Items>();

    }

    public class Items
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

    }

}
