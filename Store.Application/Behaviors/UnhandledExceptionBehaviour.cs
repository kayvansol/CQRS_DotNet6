using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using Store.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Behaviors
{
    internal class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        public UnhandledExceptionBehaviour()
        {

        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var sw = new Stopwatch();
                sw.Start();
                TResponse response = await next();
                sw.Stop();

                Serilog.Log.ForContext("Arka", true).Information("{CreateDateTime}{Parameters}{Answer}{ExecuteTime}", DateTime.Now, GetRequestValues(request), JsonConvert.SerializeObject(response), sw.Elapsed.ToString());

                return response;

            }
            catch (Exception ex) when (ex is BusinessException || ex is ValidationException)
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

                Serilog.Log.ForContext("Arka", true).Warning("{CreateDateTime}{Parameters}{ErrorCode}{ErrorMessage}{ExecuteTime}", DateTime.Now, GetRequestValues(request), ErrorCode, ErrorMessage, 0);

                throw ex;

            }
            catch (Exception ex)
            {
                Serilog.Log.ForContext("Arka", true).Error("{CreateDateTime}{Parameters}{Exception}", DateTime.Now, GetRequestValues(request), ex.Message);

                throw ex;

            }
        }


        private string GetRequestValues(TRequest request)
        {

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
