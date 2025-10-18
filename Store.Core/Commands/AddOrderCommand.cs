using Store.Domain.DTOs.Order;

namespace Store.Core.Commands
{
    public record AddOrderCommand(AddOrderCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {

    }
}
