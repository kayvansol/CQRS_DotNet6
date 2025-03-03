using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Commands;
using Store.Domain.DTOs;

namespace Store.Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("InsertOrder")]
        public async Task<ResultDto<Unit>> InsertOrder(AddOrderCommand command, CancellationToken cancellationToken) => await Mediator.Send(command, cancellationToken);

    }
}
