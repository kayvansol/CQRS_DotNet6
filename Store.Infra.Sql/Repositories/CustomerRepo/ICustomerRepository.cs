using Store.Domain;
using Store.Domain.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Sql.Repositories.CustomerRepo
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Task<List<GetAllCustomerDto>> GetAllCustomersAsync();

        Task<Customer> CreateAsync(Customer data);
    }
}
