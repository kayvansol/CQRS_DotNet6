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
        Task<List<GetAllCategoryDto>> GetAllCategories();

        Task<Pagination<GetAllCategoryDto>> GetAllCategories(int statrtPage, int pageSize);

        Task<Category> Cresate(Category data);
    }

}
