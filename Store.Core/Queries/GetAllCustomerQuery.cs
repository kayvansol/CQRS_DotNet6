using Store.Domain.DTOs.Customer;

namespace Store.Core.Queries
{
    public class GetAllCustomerQuery : IRequest<ResultDto<List<GetAllCustomerDto>>>
    {

    }
}
