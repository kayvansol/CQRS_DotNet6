using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Store.Api.Rest.Attributes;
using Store.Api.Rest.Middlewares;
using Store.Api.Rest.Services;
using Store.Api.Rest.Mapper;
using Store.Infra.Sql.Context;
using Microsoft.Extensions.Options;
//using Microsoft.EntityFrameworkCore.InMemory;

namespace Store.Api.Rest.Startup
{
    public static class ServiceRegistry
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {

            /*services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 80;
            });*/

            #region Public

            services.AddControllers();

            services.AddHttpContextAccessor();

            #endregion

            #region Hosted Service

            services.AddHostedService<GlobalTimer>();
            services.AddHostedService<GlobalTimer2>();

            #endregion

            #region DbContext

            // Scaffold-DbContext "Data Source=.;Initial Catalog=LogDB;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Context -Force

            services.AddDbContext<StoreContext>(option => option.UseSqlServer(configuration["ApplicationOptions:StoreConnectionString"]));

            services.AddDbContext<LogDbContext>(option => option.UseSqlServer(configuration["ApplicationOptions:LogDBConnectionString"]));

            //services.AddDbContext<LogDbContext>(opt => opt.UseInMemoryDatabase("LogDB")); // change also in dbcontext

            services.AddScoped<StoreContext>();

            services.AddScoped<LogDbContext>();

            #endregion

            #region Validators

            services.AddValidatorsFromAssembly(typeof(Store.Core.InjectCore).GetTypeInfo().Assembly);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            #endregion

            #region Dependency Injection

            services.AddTransient(typeof(PermissionAttribute));

            services.AddScoped(typeof(LoggingMiddleware));

            services.AddScoped(typeof(ExceptionHandlingMiddleware));

            //services.AddScoped(typeof(ValidationMiddleware<WeatherForecast>));

            services.AddOptions<CustomOptions>().Bind(configuration.GetSection("ApplicationOptions"));

            services.AddScoped(typeof(LoggingBehaviour<,>));

            #endregion

            #region Auto Mapper

            // Auto Mapper Config ...

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            #endregion

            #region Hangfire

            services.AddScoped<ICronJobs, CronJobs>();

            services.AddHangfire(config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170).UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseSqlServerStorage(configuration.GetSection("ApplicationOptions:HangFireConnectionString").Value, new         Hangfire.SqlServer.SqlServerStorageOptions
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


            #endregion

            #region Authentication & Authorization

            services.AddAuthorization(c =>
            {
                c.AddPolicy("MyApiPolicy", policy =>
                 {
                     //policy.RequireAuthenticatedUser();
                     policy.RequireClaim("scope", "api_rest");
                 });
            });

            bool inDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", option =>
                {
                    option.Authority = "https://localhost:7003";
                    if (inDocker)
                    {
                        option.MetadataAddress = "http://identityserver:8080/.well-known/openid-configuration";

                        option.RequireHttpsMetadata = false;
                        
                    }
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                    option.TokenValidationParameters.ValidateIssuer = false;
                    option.TokenValidationParameters.NameClaimType = "name";
                });

            #endregion

            #region Swagger

            // Swagger 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

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

            #endregion

        }

    }

}
