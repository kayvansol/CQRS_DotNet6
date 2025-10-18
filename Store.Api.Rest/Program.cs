using Store.Api.Rest.Startup;
using Store.Application;
using Store.Infra.Sql.Extensions;

#region Services

var builder = WebApplication.CreateBuilder(args);

builder.Services.Register(builder.Configuration);

builder.Services.AddInfraServicesRegister();

builder.Services.AddApplicationServicesRegister();

builder.Host.Register(builder.Configuration);

#endregion

#region Application

var app = builder.Build();

app.Register();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization("MyApiPolicy");

app.Run(); 

#endregion

