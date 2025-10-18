
namespace Store.Infra.Sql.Repositories.CategoryRepo
{
    public interface ICategoryRepository:IRepository<Category, int>
    {
        Task<List<GetAllCategoryDto>> GetAllCategoriesAsync();

        Task<Pagination<GetAllCategoryDto>> GetAllCategoriesAsync(int statrtPage, int pageSize);

        Task<Category> CreateAsync(Category data);
    }

}
