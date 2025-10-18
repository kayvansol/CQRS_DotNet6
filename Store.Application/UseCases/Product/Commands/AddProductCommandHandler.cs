using Store.Infra.Sql.Repositories.ProductRepo;

namespace Store.Application.UseCases.Product.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResultDto<Unit>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public AddProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ResultDto<Unit>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Domain.Product>(request.AddDto);

            await productRepository.CreateAsync(category);

            return ResultDto<Unit>.ReturnData(Unit.Value, (int)EnumResponseStatus.OK, (int)EnumResultCode.Success, EnumResultCode.Success.GetDisplayName());
        }
    }
}
