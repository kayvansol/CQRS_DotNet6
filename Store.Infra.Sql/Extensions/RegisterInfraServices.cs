using Microsoft.Extensions.DependencyInjection;
using Store.Infra.Sql.Repositories;
using Store.Infra.Sql.Repositories.CategoryRepo;
using Store.Infra.Sql.Repositories.CustomerRepo;
using Store.Infra.Sql.Repositories.OrderRepo;
using Store.Infra.Sql.Repositories.ProductRepo;

namespace Store.Infra.Sql.Extensions
{
    public static class RegisterInfraServices
    {
        public static void AddInfraServicesRegister(this IServiceCollection service)
        {
            service.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));

            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<ICustomerRepository, CustomerRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}
