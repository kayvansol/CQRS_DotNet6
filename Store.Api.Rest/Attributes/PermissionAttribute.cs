using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Store.Domain.Objects;

namespace Store.Api.Rest.Attributes
{
    public class PermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IOptionsMonitor<CustomOptions> options;

        public PermissionAttribute(IOptionsMonitor<CustomOptions> options)
        {
            this.options = options;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if(context.HttpContext.Request.Headers["Authorization"].Any())
            {
                string apiKey = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault() ?? "";

                if (!(apiKey == options.CurrentValue.apiKey))
                {
                    throw new BusinessException(1500, "لطفا کلید برنامه را به درستی وارد نمایید");
                }
            }
            else
            {
                throw new BusinessException(1600, "لطفا کلید برنامه را وارد نمایید");
            }

            if (context.HttpContext.Request.QueryString.HasValue)
            {
                string blackList = options.CurrentValue.BlackList;

                if (context.HttpContext.Request.QueryString.Value.Contains(blackList))
                    throw new BusinessException(1700, "ورود کلمه ی غیر مجاز");
            }

            return;
        }
    }
}
