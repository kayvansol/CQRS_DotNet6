using IdentityModel.Client;
using Newtonsoft.Json;
using Store.ConsoleClient;

static async Task main()
{

    // Call Identity Server to get AccessToken using the GrantTypes ClientCredentials ...

    var identityClient = new HttpClient();
    var discovery = await identityClient.GetDiscoveryDocumentAsync("https://localhost:7003");
    
    if (discovery.IsError)
    {
        System.Console.WriteLine(discovery.Error);
        return;
    }

    // Next Action ...

    var tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    {
        Address = discovery.TokenEndpoint,
        //ClientId = "console_client",
        ClientId = "oauthClient",
        ClientSecret = "SuperSecretPassword",
        Scope = "api_rest"
    });

    if (tokenResponse.IsError)
    {
        System.Console.WriteLine(tokenResponse.Error);
        return;
    }

    // Main Api Call with received AccessToken from Identity Server ...

    var client = new HttpClient();

    client.BaseAddress = new Uri("https://localhost:7084");

    client.SetBearerToken(tokenResponse.AccessToken);

    var stringResult = await client.GetStringAsync("/WeatherForecast");

    var result = JsonConvert.DeserializeObject<List<WeatherForecast>>(stringResult);

    foreach (var item in result)
    {
        System.Console.WriteLine($"{item.Date}\t {item.Summary} \t\t {item.TemperatureC}\t{item.TemperatureF}");
        System.Console.WriteLine("".PadLeft(200, '-'));
    }

    System.Console.ReadLine();
}

Task.Delay(10000).Wait();

await main();

System.Console.ReadKey();