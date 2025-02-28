using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Commands;
using Store.Core.Queries;
using Store.Domain.DTOs;
using Store.Domain.DTOs.Category;

namespace Store.Api.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {

        //[AllowAnonymous]
        [HttpPost("GetAllCategories")]
        public async Task<ResultDto<List<GetAllCategoryDto>>> GetAllCategories(GetAllCategoryQuery query, CancellationToken cancellationToken)
        {
            return await Mediator.Send(query, cancellationToken);
        } 


        [HttpPost("InsertCategory")]
        public async Task<ResultDto<Unit>> InsertCategory(AddCategoryCommand command, CancellationToken cancellationToken) => await Mediator.Send(command, cancellationToken);

    }
        
}
