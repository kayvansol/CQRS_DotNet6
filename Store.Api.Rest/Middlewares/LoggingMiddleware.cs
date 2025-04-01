
namespace Store.Api.Rest.Middlewares
{

    public class LoggingMiddleware : IMiddleware
    {

        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly LoggingBehaviour<string?, string?> logging;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger, LoggingBehaviour<string?, string?> logging)
        {

            _logger = logger;
            this.logging = logging;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            if (context.Request.Path.HasValue)
            {
                if (context.Request.Path.Value.Contains("/hangfire/stats"))
                {
                    return;
                }
            }
            
            // Log the request details  
            var requestBody = await LogRequestAsync(context);

            // Create a new MemoryStream to capture the response body  
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                var sw = new Stopwatch();

                sw.Start();

                await next(context); // Call the next middleware/component  

                sw.Stop();

                // Log the response details  
                await LogResponseAsync(context, responseBody, sw.ElapsedMilliseconds);

                // Write the response body back to the original stream  
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> LogRequestAsync(HttpContext context)
        {
            // Enable buffering to read the request body  
            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var requestBody = await reader.ReadToEndAsync();
                // Reset the request body stream position so the next middleware can read it  
                context.Request.Body.Position = 0;

                if (requestBody == "" && context.Request.Method == "POST")
                    return requestBody;


                if (context.Request.Method == "GET")
                {
                    if (context.Request.QueryString.HasValue == false)
                    {
                        return requestBody;
                    }
                    else
                    {
                        requestBody = context.Request.QueryString.Value;
                    }                    
                }

                //requestBody = requestBody.ToLower();

                //requestBody = JObject.Parse(requestBody).ToString();

                requestBody = requestBody.Replace("\n", "");

                string logStr = $"Request Method: {context.Request.Method} - Request Path: {context.Request.Path} - Request Body: {requestBody}";

                // Log request details  
                //Console.WriteLine(logStr);

                await logging.Log(logStr, "", LogLevel.Information);

                return requestBody;
            }
        }

        private async Task LogResponseAsync(HttpContext context, MemoryStream responseBody, long time)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

            string logStr = $"Response Status Code: {context.Response.StatusCode} - Response Body: {responseBodyText} - Exec Time: {time}";

            // Log response details  
            //Console.WriteLine(logStr);

            if (context.Response.StatusCode == 400)
            {
                await logging.Log(logStr, null, LogLevel.Error, 0, new Exception(responseBodyText));
            }
            else
            {
                await logging.Log("", logStr, LogLevel.Information, time);
            }

            // Reset stream position back to the beginning  
            responseBody.Seek(0, SeekOrigin.Begin);
        }
    }
}
