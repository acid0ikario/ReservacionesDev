using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.APIutilis
{
    public static class ApiManager
    {
        private static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Crea un llamada post a un endpoint de un api rest
        /// </summary>
        /// <param name="endPoint">URL donde se encuentra de la accion del API a consumir</param>
        /// <param name="objectToSend">objeto que sera serializado a JSON para enviarlo en el content de la peticion</param>
        /// <returns></returns>
        public static HttpResponseMessage PostAsync(string endPoint, object objectToSend)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(objectToSend), Encoding.UTF8, "application/json");
            _httpClient.BaseAddress = new Uri(endPoint);
            var responseTask = _httpClient.PostAsync(_httpClient.BaseAddress.AbsoluteUri, content);
            responseTask.Wait();
            var ApiResponse = responseTask.Result;
            return ApiResponse;
        }

        public static object GetAsync(string endPoint, object objectToSend)
        {

            return "";
        }

        public static object PutAsync(string endPoint, object objectToSend)
        {

            return "";
        }
    }
}
