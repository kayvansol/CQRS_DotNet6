using Store.Domain.DTOs.Category;

namespace Store.Core.Queries
{
    public record GetAllCategoryQuery(int statrtPage,int pageSize) : IRequest<ResultDto<List<GetAllCategoryDto>>>
    {

    }
}
