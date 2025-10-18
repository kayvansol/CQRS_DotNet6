
namespace Store.Api.Rest.Logging
{
    public class LoggingBehaviour<TRequest, TResponse>
    {

        public async Task Log(string? request, string? response, LogLevel logLevel, long? executeTime = 0,Exception? ex = null)
        {

            if (logLevel == LogLevel.Information)
            {
                Serilog.Log.ForContext("Arka", true).Information("{CreateDateTime}{Parameters}{Answer}{ExecuteTime}", DateTime.Now, GetRequestValues(request), response, executeTime);
            }
            if (logLevel == LogLevel.Warning)
            {

                int ErrorCode = 0;
                string ErrorMessage = string.Empty;

                if (ex is BusinessException)
                {
                    var businessEx = ex as BusinessException;
                    ErrorCode = businessEx.Code;
                    ErrorMessage = businessEx.Error;
                }
                else if (ex is ValidationException)
                {
                    var businessEx = ex as ValidationException;
                    ErrorMessage = businessEx.Message;
                    ErrorCode = 100;
                }

                Serilog.Log.ForContext("Arka", true).Warning("{CreateDateTime}{Parameters}{ErrorCode}{ErrorMessage}{ExecuteTime}", DateTime.Now, GetRequestValues(request), ErrorCode, ErrorMessage, executeTime);

            }
            else if (logLevel == LogLevel.Error)
            {
                Serilog.Log.ForContext("Arka", true).Error("{CreateDateTime}{Parameters}{Exception}", DateTime.Now, GetRequestValues(request), ex.Message);
            }

        }

        private string GetRequestValues(string request)
        {

            return request;

            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            var parameters = request.GetType().GetProperties().ToList();
            foreach (var param in parameters)
            {
                dictionary.Add(param.Name, param.GetValue(request));
            }

            var paramJson = JsonConvert.SerializeObject(dictionary, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            return new string(paramJson);
        }

    }
}
