using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Commands;
using Store.Core.Queries;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Category;
using Store.Domain.DTOs.Product;

namespace Store.Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {

        //[AllowAnonymous]
        [HttpPost("GetAllProducts")]
        public async Task<ResultDto<List<GetAllProductDto>>> GetAllProducts(GetAllProductQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.Send(query, cancellationToken);
        }


        [HttpPost("InsertProduct")]
        public async Task<ResultDto<Unit>> InsertProduct(AddProductCommand command, CancellationToken cancellationToken) => await Mediator.Send(command, cancellationToken);

    }
}
