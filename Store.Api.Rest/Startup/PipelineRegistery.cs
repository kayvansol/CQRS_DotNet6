using Store.Api.Rest.Middlewares;

namespace Store.Api.Rest.Startup
{
    public static class PipelineRegistery
    {
        public static void Register(this WebApplication webApplication)
        {

            // Configure the HTTP request pipeline.
            if (webApplication.Environment.IsDevelopment())
            {

            }

            //webApplication.UseMiddleware(typeof(ValidationMiddleware<>)); 

            webApplication.UseMiddleware(typeof(LoggingMiddleware));

            webApplication.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            webApplication.UseSwagger();

            webApplication.UseSwaggerUI();

        }
    }
}
