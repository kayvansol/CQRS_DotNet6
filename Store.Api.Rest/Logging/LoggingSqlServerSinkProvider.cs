using Serilog.Core;
using Serilog.Events;

namespace Store.Api.Rest.Logging
{
    public class LoggingSqlServerSinkProvider : ILogEventSink
    {
        private readonly string _connectionString;
        private readonly IServiceProvider _serviceProvider;

        public LoggingSqlServerSinkProvider(string connectionString, IServiceProvider serviceProvider)
        {
            _connectionString = connectionString;
            _serviceProvider = serviceProvider;
        }

        public async void Emit(LogEvent logEvent)
        {
            try
            {
                if (logEvent.Level == LogEventLevel.Warning)
                {
                    var parameters = logEvent.Properties.ContainsKey("Parameters") ? logEvent.Properties["Parameters"].ToString().Replace("\\", "") : "Unknown";
                    var ErrorCode = logEvent.Properties.ContainsKey("ErrorCode") ? logEvent.Properties["ErrorCode"].ToString().Replace("\\", "") : "Unknown";
                    var ErrorMessage = logEvent.Properties.ContainsKey("ErrorMessage") ? logEvent.Properties["ErrorMessage"].ToString().Replace("\\", "") : "Unknown";
                    var executeTime = logEvent.Properties.ContainsKey("ExecuteTime") ? logEvent.Properties["ExecuteTime"].ToString().Replace("\\", "") : "Unknown";

                    HandleLog operationLog = new HandleLog()
                    {
                        Parameters = parameters,
                        ExecuteTime = long.Parse(executeTime),
                        CreateDateTime = DateTime.Now,
                        ErrorCode = int.Parse(ErrorCode),
                        Exception = ErrorMessage
                    };

                    using (var scop = _serviceProvider.CreateScope())
                    {
                        var dbcontext = scop.ServiceProvider.GetRequiredService<LogDbContext>();

                        await dbcontext.HandleLogs.AddAsync(operationLog);
                        await dbcontext.SaveChangesAsync();
                    }

                }
                else if (logEvent.Level == LogEventLevel.Information)
                {
                    var parameters = logEvent.Properties.ContainsKey("Parameters") ? logEvent.Properties["Parameters"].ToString().Replace("\\", "") : "Unknown";
                    var answer = logEvent.Properties.ContainsKey("Answer") ? logEvent.Properties["Answer"].ToString().Replace("\\", "") : "Unknown";
                    var executeTime = logEvent.Properties.ContainsKey("ExecuteTime") ? logEvent.Properties["ExecuteTime"].ToString().Replace("\\", "") : "Unknown";

                    OperationLog operationLog = new OperationLog()
                    {
                        Parameters = parameters,
                        ExecuteTime = long.Parse(executeTime),
                        CreateDateTime = DateTime.Now,
                        Answer = answer
                    };

                    using (var scop = _serviceProvider.CreateScope())
                    {
                        var dbcontext = scop.ServiceProvider.GetRequiredService<LogDbContext>();

                        await dbcontext.OperationLogs.AddAsync(operationLog);
                        await dbcontext.SaveChangesAsync();
                    }
                }
                else if (logEvent.Level == LogEventLevel.Error)
                {
                    var parameters = logEvent.Properties.ContainsKey("Parameters") ? logEvent.Properties["Parameters"].ToString().Replace("\\", "") : "Unknown";
                    var exception = logEvent.Properties.ContainsKey("Exception") ? logEvent.Properties["Exception"].ToString().Replace("\\", "") : "Unknown";
                    
                    ErrorLog errorLog = new ErrorLog()
                    {
                        Parameters = parameters,                        
                        CreateDateTime = DateTime.Now,
                        Exception = exception
                    };

                    using (var scop = _serviceProvider.CreateScope())
                    {
                        var dbcontext = scop.ServiceProvider.GetRequiredService<LogDbContext>();

                        await dbcontext.ErrorLogs.AddAsync(errorLog);
                        await dbcontext.SaveChangesAsync();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
