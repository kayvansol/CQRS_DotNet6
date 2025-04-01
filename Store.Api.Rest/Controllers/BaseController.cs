﻿
namespace Store.Api.Rest.Controllers
{
    //[ServiceFilter(typeof(PermissionAttribute))]
    //[Authorize]      //replaced with MyApiPolicy policy ...
    public class BaseController : Controller
    {
        private IMediator mediator;

        protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
