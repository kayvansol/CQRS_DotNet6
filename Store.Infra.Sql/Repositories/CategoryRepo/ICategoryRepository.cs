using Store.Domain;
using Store.Domain.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Store.Domain.Extensions.PaginationExtension;

namespace Store.Infra.Sql.Repositories.CategoryRepo
{
    public interface ICategoryRepository:IRepository<Category, int>
    {
        Task<List<GetAllCategoryDto>> GetAllCategoriesAsync();

        Task<Pagination<GetAllCategoryDto>> GetAllCategoriesAsync(int statrtPage, int pageSize);

        Task<Category> CreateAsync(Category data);
    }

}
