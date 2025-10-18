
namespace Store.Api.Rest.Services
{
    public class GlobalTimer2 : BackgroundService
    {
        private readonly ILogger<GlobalTimer2> _logger;

        public GlobalTimer2(ILogger<GlobalTimer2> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Background task (GlobalTimer 2) is running...");

                Console.WriteLine("Background task (GlobalTimer 2) is running...");

                // Perform background task
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
