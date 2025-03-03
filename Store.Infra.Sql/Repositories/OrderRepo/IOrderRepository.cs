using Store.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Sql.Repositories.OrderRepo
{
    public interface IOrderRepository : IRepository<Order, int>
    {
        //Task<List<GetAllCustomerDto>> GetAllCustomersAsync();

        Task<Order> CreateAsync(Order data);
    
    }
}
