
namespace Store.Domain.DTOs.Product
{
    public class AddProductCommandDto
    {
        public string ProductName { get; set; } = null!;

        public int CategoryId { get; set; }

        public decimal Price { get; set; }
    }
}
