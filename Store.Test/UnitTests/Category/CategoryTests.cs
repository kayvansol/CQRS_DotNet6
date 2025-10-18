using Store.Application.UseCases.Category.Commands;
using Store.Domain.DTOs.Category;
using Store.Infra.Sql.Repositories.CategoryRepo;
using Store.Test.Data.Category;


namespace Store.Test.UnitTests.Category
{
    public class CategoryTests : IDisposable
    {

        private readonly ICategoryRepository _repository;
        private readonly AddCategoryCommandHandler _handler;
        private readonly Repository<Domain.Category, int> _repo;
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public CategoryTests()
        {
            _mapper = GetServices.GetMapper();
            _context = GetServices.GetStoreContext();
            _repo = new Repository<Domain.Category, int>(_context);
            _repository = new CategoryRepository(_context, _repo, _mapper);
            _handler = new AddCategoryCommandHandler(_repository, _mapper);
        }


        [Theory]
        [MemberData(nameof(CategoryTestData.addCategoryCommandDto), MemberType = typeof(CategoryTestData))]
        public async Task Post_create_a_new_category_and_response_status_code_ok(AddCategoryCommandDto dto)
        {
            // Arrange
            var request = new AddCategoryCommand(dto);
            var token = new CancellationToken(false);

            // Act
            var result = await _handler.Handle(request, token);

            // Assert       
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);

            result.Data.Should().NotBeNull();

        }

        public void Dispose()
        {                        
            _repo.Dispose();            
            _context.Dispose();
        }

    }
}
