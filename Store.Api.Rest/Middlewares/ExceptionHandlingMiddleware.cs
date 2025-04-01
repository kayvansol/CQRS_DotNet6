using System.Diagnostics;
using System.Text;
using Store.Api.Rest.Logging;

namespace Store.Api.Rest.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly LoggingBehaviour<string, string> _logging;

        public ExceptionHandlingMiddleware(LoggingBehaviour<string?, string?> logging)
        {
            _logging = logging;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestBody = await GetRequestBodyAsync(context);
            var sw = new Stopwatch();

            try
            {
                sw.Start();
                await next(context);
            }
            catch (Exception ex) when (ex is BusinessException || ex is ValidationException)
            {

                /*if (ex is BusinessException)
                {
                    var _ex = ex as BusinessException;

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new BusinessException
                    (Error: _ex?.Error, Code: _ex.Code).ToString()));
                }
                else if (ex is ValidationException)
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new BusinessException
                    (Error: ex.Message, Code: 0).ToString()));
                }*/

                sw.Stop();

                string logStr = $"Request Method: {context.Request.Method} - Request Path: {context.Request.Path} - Request Body: {requestBody.Replace("\n ", "")}";

                Console.WriteLine(logStr);

                await _logging.Log(logStr, null, LogLevel.Warning, sw.ElapsedMilliseconds, ex);

                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new CustomException
                {
                    Message = ex.Message,
                    StatusCode = 401,
                    HResult = ex.HResult,
                    Source = ex.Source
                }));


            }
            catch (Exception ex)
            {

                sw.Stop();

                string logStr = $"Request Method: {context.Request.Method} - Request Path: {context.Request.Path} - Request Body: {requestBody.Replace("\n ", "")}";

                Console.WriteLine(logStr);

                await _logging.Log(logStr, null, LogLevel.Error, 0, ex);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new CustomException
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    HResult = ex.HResult,
                    Source = ex.Source
                }));

            }
            
        }


        private async Task<string> GetRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset the stream position  
                return body;
            }
        }

        private async Task<string> GetResponse(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            return responseBodyText;

        }

    }
}
