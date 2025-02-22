using Microsoft.Extensions.DependencyInjection;
using Store.Infra.Sql.Repositories;
using Store.Infra.Sql.Repositories.CategoryRepo;
using Store.Infra.Sql.Repositories.ProductRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Sql.Extensions
{
    public static class RegisterInfraServices
    {
        public static void AddInfraServicesRegister(this IServiceCollection service)
        {
            service.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));

            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
