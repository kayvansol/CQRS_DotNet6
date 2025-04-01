using Store.Domain.DTOs.Customer;

namespace Store.Infra.Sql.Repositories.CustomerRepo
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        Task<List<GetAllCustomerDto>> GetAllCustomersAsync();

        Task<Customer> CreateAsync(Customer data);
    }
}
