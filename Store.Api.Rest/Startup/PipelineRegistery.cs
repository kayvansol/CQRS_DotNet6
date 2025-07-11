﻿using Store.Api.Rest.Middlewares;
using Store.Api.Rest.Services;

namespace Store.Api.Rest.Startup
{
    public static class PipelineRegistery
    {
        public static void Register(this WebApplication webApplication)
        {

            #region Development

            // Configure the HTTP request pipeline.
            if (webApplication.Environment.IsDevelopment())
            {

            }

            #endregion

            #region Middlewares

            //webApplication.UseMiddleware(typeof(ValidationMiddleware<>)); 

            webApplication.UseMiddleware(typeof(LoggingMiddleware));

            webApplication.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            #endregion

            #region Swagger

            webApplication.UseSwagger();

            //webApplication.UseSwaggerUI();

            webApplication.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.OAuthClientSecret("swagger");
                options.OAuthScopes("api_rest");
                options.OAuthClientId("demo_api_swagger");
                options.OAuthAppName("Demo API - Swagger");
                options.OAuthUsePkce();
            });

            #endregion

            #region Hangfire

            var interval = int.Parse(webApplication.Configuration["HangFireSettings:interval"]);

            webApplication.UseHangfireDashboard();

            webApplication.MapHangfireDashboard();

            RecurringJob.AddOrUpdate<ICronJobs>(x => x.GetAppUpDateTime(), Cron.MinuteInterval(1), TimeZoneInfo.Local,"datetimequeue"
                );

            RecurringJob.AddOrUpdate<ICronJobs>(x => x.GetRandomNumber(),$"0/{interval} * * * * *" , TimeZoneInfo.Local, "randomqueue"
                );

            #endregion

            //webApplication.UseHttpsRedirection();
        }
    }
}
