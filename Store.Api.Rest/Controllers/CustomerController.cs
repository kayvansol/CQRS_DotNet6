﻿
namespace Store.Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : BaseController
    {

        [AllowAnonymous]
        [HttpPost("GetAllCustomers")]
        public async Task<ResultDto<List<GetAllCustomerDto>>> GetAllCustomers(GetAllCustomerQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.Send(query, cancellationToken);
        }


        [HttpPost("InsertCustomer")]
        public async Task<ResultDto<Unit>> InsertCustomer(AddCustomerCommand command, CancellationToken cancellationToken) => await Mediator.Send(command, cancellationToken);

    }
}
