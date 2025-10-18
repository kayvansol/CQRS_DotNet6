
namespace Store.Api.Rest.Startup
{
    public static class HostRegistery
    {
        public static void Register(this ConfigureHostBuilder configureHostBuilder, IConfiguration config)
        {

            #region Serilog

            // Serilog Configs ...

            configureHostBuilder.UseSerilog();

            configureHostBuilder.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration).
                MinimumLevel.Debug()
                .MinimumLevel.Information()
                .Filter.ByIncludingOnly(e => e.Properties.ContainsKey("Arka"))
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Sink(new LoggingSqlServerSinkProvider(config["ApplicationOptions:LogDBConnectionString"], services));

            });

            #endregion

        }
    }
}
