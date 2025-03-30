using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace Store.IdentityServer;

internal class Clients
{
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
            {
                new Client
                {
                    ClientId = "oauthClient",
                    ClientName = "Example client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())}, // change me!
                    AllowedScopes = new List<string> {"api1.read", "api_rest" }
                },
                new Client
                {
                    ClientId = "web",
                    ClientName = "Store MVC",
                    ClientSecrets = new List<Secret> {new Secret("secret".Sha256())}, // change me!
                    
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string> {"https://localhost:7205/signin-oidc"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:7205/signout-callback-oidc"},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "api_rest",
                        "offline_access",
                        "api_rest.read",
                        "api_rest.write"
                    },

                    RequirePkce = true,
                    AllowPlainTextPkce = false
                },
                new Client
                {
                    ClientId = "demo_api_swagger",
                    ClientName = "Swagger UI for demo_api",
                    ClientSecrets = {new Secret("swagger".Sha256())}, // change me!
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:7084/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:7084"},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "api_rest",
                        "offline_access",
                        "api_rest.read",
                        "api_rest.write"
                    }
                }
            };
    }
}

internal class Resources
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new[]
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new[]
        {
                new ApiResource
                {
                    Name = "api_rest",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string> {"api_rest"},
                    ApiSecrets = new List<Secret> {new Secret("secret".Sha256())}, // change me!
                    UserClaims = new List<string> {"role"}
                }
            };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
                new ApiScope("api_rest.read", "Read Access to API #1"),
                new ApiScope("api_rest", "Access to API"),
                new ApiScope("api_rest.write", "Write Access to API #1")
            };
    }
}

internal class Users
{
    public static List<TestUser> Get()
    {
        return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                    Username = "Kayvan",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, "kayvan.sol2@gmail.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };
    }
}