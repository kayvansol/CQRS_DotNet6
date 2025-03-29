using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SsoSamples.IdentityServer;
using System.Reflection;

namespace Store.IdentityServer;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {

        const string connectionString = "Data Source=.;Initial Catalog=StoreAccount;Integrated Security=True;TrustServerCertificate=True";

        var migrationsAssembly = typeof(HostingExtensions).GetTypeInfo().Assembly.GetName().Name;

        builder.Services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var identityServerBuilder = builder.Services.AddIdentityServer(options => options.KeyManagement.Enabled = true);


        //.well-known/openid-configuration

        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.AddTransient<IEventSink, SeqEventSink>();

        /*builder.Services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes

                options.EmitStaticAudienceClaim = true;

                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);*/

        identityServerBuilder.AddOperationalStore(options =>
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
            .AddConfigurationStore(options =>
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

        identityServerBuilder.AddAspNetIdentity<ApplicationUser>();

        return builder.Build();
        
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        InitializeDbTestData(app);

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();
            
        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }

    private static void InitializeDbTestData(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
            serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            if (!context.Clients.Any())
            {
                foreach (var client in Clients.Get())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Resources.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var scope in Resources.GetApiScopes())
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Resources.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (!userManager.Users.Any())
            {
                foreach (var testUser in Users.Get())
                {
                    var identityUser = new ApplicationUser
                    {
                        Id = testUser.SubjectId,
                        UserName = testUser.Username,                        
                        FirstName = "Kayvan",
                        LastName = "Soleimani",
                        Mobile = "0989148883420",
                        Email = "kayvan.sol2@gmail.com"
                    };

                    userManager.CreateAsync(identityUser, "Password123!").Wait();
                    userManager.AddClaimsAsync(identityUser, testUser.Claims.ToList()).Wait();
                }
            }
        }
    }

}
