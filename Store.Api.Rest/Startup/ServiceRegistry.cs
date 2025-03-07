using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Store.Api.Rest.Attributes;
using Store.Api.Rest.Logging;
using Store.Api.Rest.Middlewares;
using Store.Api.Rest.Services;
using Store.Infra.Sql.LogContext;
using Store.Domain.Objects;
using Store.Api.Rest.Mapper;
using Store.Infra.Sql.Context;
using Hangfire;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
//using Microsoft.EntityFrameworkCore.InMemory;

namespace Store.Api.Rest.Startup
{
    public static class ServiceRegistry
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddHostedService<GlobalTimer>();
            services.AddHostedService<GlobalTimer2>();

            services.AddHttpContextAccessor();

            // Scaffold-DbContext "Data Source=.;Initial Catalog=LogDB;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Context -Force

            services.AddDbContext<StoreContext>(option => option.UseSqlServer(configuration["ApplicationOptions:StoreConnectionString"]));

            services.AddDbContext<LogDbContext>(option => option.UseSqlServer(configuration["ApplicationOptions:LogDBConnectionString"]));

            //services.AddDbContext<LogDbContext>(opt => opt.UseInMemoryDatabase("LogDB")); // change also in dbcontext

            services.AddScoped<StoreContext>();

            services.AddScoped<LogDbContext>();

            services.AddValidatorsFromAssembly(typeof(Store.Core.InjectCore).GetTypeInfo().Assembly);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(PermissionAttribute));

            services.AddScoped(typeof(LoggingMiddleware));

            services.AddScoped(typeof(ExceptionHandlingMiddleware));

            //services.AddScoped(typeof(ValidationMiddleware<WeatherForecast>));

            services.AddOptions<CustomOptions>().Bind(configuration.GetSection("ApplicationOptions"));

            services.AddScoped(typeof(LoggingBehaviour<,>));

            // Auto Mapper Config ...

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            // Swagger 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            /*services.AddSwaggerGen(a =>
            {
                a.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                a.AddSecurityRequirement(new OpenApiSecurityRequirement{{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type =  ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
                    }
                });
            });*/



            services.AddScoped<ICronJobs, CronJobs>();

            services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(configuration.GetSection("ApplicationOptions:HangFireConnectionString").Value, new Hangfire.SqlServer.SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(6),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(6),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true,
                //CountersAggregateInterval = TimeSpan.FromMinutes(5)
            })
            );

            services.AddHangfireServer(option => option.Queues = new[] { "datetimequeue", "randomqueue" });


            services.AddAuthorization(c =>
            {
                c.AddPolicy("MyApiPolicy", policy =>
                 {
                     //policy.RequireAuthenticatedUser();
                     policy.RequireClaim("scope", "api_rest");
                 });
            });

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", option =>
                {
                    option.Authority = "https://localhost:7003";
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                    
                });


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Protected API", Version = "v1" });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:7003/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:7003/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"api_rest", "Demo API - full access"}
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }

    }

    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                               context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "oauth2"}}]
                            = new[] { "api_rest" }
                    }
                };
            }
        }
    }

}
