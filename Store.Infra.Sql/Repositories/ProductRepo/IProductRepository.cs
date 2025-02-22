using Store.Domain;
using Store.Domain.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Sql.Repositories.ProductRepo
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<List<GetAllProductDto>> GetAllProductsAsync();

        Task<Product> CreateAsync(Product data);
    }
}
