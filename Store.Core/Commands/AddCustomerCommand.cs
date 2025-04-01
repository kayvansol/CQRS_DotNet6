using Store.Domain.DTOs.Customer;

namespace Store.Core.Commands
{
    public record AddCustomerCommand(AddCustomerCommandDto AddDto) : IRequest<ResultDto<Unit>>
    {

    }
}
