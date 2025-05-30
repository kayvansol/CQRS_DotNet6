﻿
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

        public async Task<Category> CreateAsync(Category data)
        {
            _context.Add(data);
            _context.SaveChanges();

            return data;
        }

        public async Task<Pagination<GetAllCategoryDto>> GetAllCategoriesAsync(int statrtPage, int pageSize)
        {
            var list = _repo.GetAll();

            list = list.OrderByDescending(o => o.CategoryName);

            var result = _mapper.ProjectTo<GetAllCategoryDto>(list).ToPagination(statrtPage, pageSize);

            return result;
        }

        public async Task<List<GetAllCategoryDto>> GetAllCategoriesAsync()
        {
            var list = _repo.GetAll();

            list = list.OrderByDescending(o => o.CategoryName);

            var result = _mapper.ProjectTo<GetAllCategoryDto>(list).ToList();

            return result;
        }
    }
}
