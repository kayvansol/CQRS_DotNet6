using Store.Domain.DTOs.Product;

namespace Store.Core.Queries
{
    public record GetAllProductQuery : IRequest<ResultDto<List<GetAllProductDto>>>
    {

    }
}
