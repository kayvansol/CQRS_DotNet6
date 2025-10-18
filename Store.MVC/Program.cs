using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System.IdentityModel.Tokens.Jwt;

#region Services

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IdentityModelEventSource.ShowPII = true;

#endregion

#region Authentication

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

bool inDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

builder.Services.AddAuthentication(c =>
{
    c.DefaultScheme = "Cookies";
    c.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies")
  .AddOpenIdConnect("oidc", c =>
  {
      c.Authority = "https://localhost:7003";
      if (inDocker)
      {
          c.MetadataAddress = "http://identityserver:8080/.well-known/openid-configuration";
      }
      c.ClientId = "web";
      c.ClientSecret = "secret";
      c.ResponseType = "code";
      c.Scope.Clear();
      c.Scope.Add("api_rest");
      c.Scope.Add("profile"); 
      c.Scope.Add("openid");
      //c.Scope.Add("offline_access");
      c.GetClaimsFromUserInfoEndpoint = true;
      c.SaveTokens = true;
      if (inDocker)
      {
          c.RequireHttpsMetadata = false;
          c.Events.OnRedirectToIdentityProvider = context =>
          {
              context.ProtocolMessage.IssuerAddress = "https://localhost:7003/connect/authorize";
              return Task.CompletedTask;
          };
      }
      
  });

#endregion

#region Http Clients

builder.Services.AddHttpClient("w", c =>
{
    if (!inDocker)
    {
        c.BaseAddress = new Uri("https://localhost:7084");
    }
    else
    {
        c.BaseAddress = new Uri("https://api");
    }
}).ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback =
            (httpRequestMessage, cert, certChain, policyErrors) => true
    };
});

#endregion

#region Application

var app = builder.Build();

#region Production

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

app.Run();

#endregion