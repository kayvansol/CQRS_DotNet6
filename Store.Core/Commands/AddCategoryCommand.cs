using Store.Domain.DTOs.Category;

namespace Store.Core.Commands
{
    public record AddCategoryCommand(AddCategoryCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {
        
    }
}
