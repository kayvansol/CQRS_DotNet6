
namespace Store.Api.Rest.Services
{
    public class GlobalTimer : IHostedService, IDisposable
    {

        private Timer _timer;
        static Stopwatch stopwatch;

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));

            stopwatch = new Stopwatch();

            stopwatch.Start();

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            Console.WriteLine($"UpTime: {stopwatch.ElapsedMilliseconds} ms");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);

            stopwatch.Stop();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
