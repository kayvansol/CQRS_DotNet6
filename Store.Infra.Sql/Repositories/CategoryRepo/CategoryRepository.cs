using AutoMapper;
using Store.Domain;
using Store.Domain.DTOs.Category;
using Store.Domain.Extensions;
using Store.Infra.Sql.Context;
using static Store.Domain.Extensions.PaginationExtension;

namespace Store.Infra.Sql.Repositories.CategoryRepo
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        private readonly StoreContext _context;
        private readonly IRepository<Category, int> _repo;
        private readonly IMapper _mapper;

        public CategoryRepository(StoreContext context,
            IRepository<Category, int> repo, IMapper mapper) : base(context)
        {
            _context = context;
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Category> Create(Category data)
        {
            _context.Add(data);
            _context.SaveChanges();

            return data;
        }

        public async Task<Pagination<GetAllCategoryDto>> GetAllCategories(int statrtPage, int pageSize)
        {
            var list = _repo.GetAll();

            list = list.OrderByDescending(o => o.CategoryName);

            var result = _mapper.ProjectTo<GetAllCategoryDto>(list).ToPagination(statrtPage, pageSize);

            return result;
        }

        public async Task<List<GetAllCategoryDto>> GetAllCategories()
        {
            var list = _repo.GetAll();

            list = list.OrderByDescending(o => o.CategoryName);

            var result = _mapper.ProjectTo<GetAllCategoryDto>(list).ToList();

            return result;
        }
    }
}
