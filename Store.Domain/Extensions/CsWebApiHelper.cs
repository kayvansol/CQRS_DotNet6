using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Extensions
{
    public static class CsWebApiHelper
    {
        public static string CallPostMethod(string uri, string parameters)
        {
            try
            {
                var stringContent = new StringContent(parameters, System.Text.Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();

                var response = httpClient.PostAsync(uri, stringContent).Result;
                var responseContent = response.Content.ReadAsStringAsync().Result;
                return responseContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string CallPostMethodWithBasic(string uri, string parameters, string username, string password)
        {
            try
            {
                var stringContent = new StringContent(parameters, System.Text.Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (ServicePointManager, cer, chin, sslerror) => { return true; };

                var bytearray = ASCIIEncoding.ASCII.GetBytes(username + ":" + parameters);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",Convert.ToBase64String(bytearray));  
                var response = httpClient.PostAsync(uri, stringContent).Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;
                return responseContent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
