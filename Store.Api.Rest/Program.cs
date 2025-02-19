using Store.Api.Rest.Startup;
using Store.Application;
using Store.Infra.Sql.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Register(builder.Configuration);

builder.Services.AddInfraServicesRegister();

builder.Services.AddApplicationServicesRegister();

builder.Host.Register(builder.Configuration);

var app = builder.Build();

app.Register();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); 
