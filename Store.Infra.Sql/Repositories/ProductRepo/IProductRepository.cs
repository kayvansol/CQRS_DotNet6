using Store.Domain.DTOs.Product;

namespace Store.Infra.Sql.Repositories.ProductRepo
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<List<GetAllProductDto>> GetAllProductsAsync();

        Task<Product> CreateAsync(Product data);
    }
}
