﻿using Store.IdentityServer;

#region Logger

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

#endregion

#region Services & Pipeline

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder.ConfigureServices().ConfigurePipeline();
    
    app.Run();

}
catch (Exception ex)
{
    //Log.Fatal(ex, "Unhandled exception");
    string type = ex.GetType().Name;

    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
    //return 1;
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

#endregion