using Hangfire;
using Store.Api.Rest.Middlewares;
using Store.Api.Rest.Services;

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

            var interval = int.Parse(webApplication.Configuration["HangFireSettings:interval"]);

            webApplication.UseHangfireDashboard();

            webApplication.MapHangfireDashboard();

            RecurringJob.AddOrUpdate<ICronJobs>(x => x.GetAppUpDateTime(), Cron.MinuteInterval(1), TimeZoneInfo.Local,"datetimequeue"
                );

            RecurringJob.AddOrUpdate<ICronJobs>(x => x.GetRandomNumber(),$"0/{interval} * * * * *" , TimeZoneInfo.Local, "randomqueue"
                );

        }
    }
}
