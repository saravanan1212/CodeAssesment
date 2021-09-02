using Newtonsoft.Json;
using System;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class HelperClass<T>
    {
        public string hostname = System.Configuration.ConfigurationManager.AppSettings["hostname"];

        public RestClient setUrl(String endpoint)
        {
            var url = $"{hostname}/{endpoint}";
            var restClient = new RestClient(url);
            return restClient;
        }

        public RestRequest CreateTokenPostRequest(String payload, string guid)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("X-CorrelationId", guid);
            restRequest.AddHeader("X-Forwarded-For", guid);
            restRequest.AddHeader("X-Clienttypeid", "5");
            restRequest.AddParameter("application/json", payload, ParameterType.RequestBody);
            return restRequest;
        }

        public RestRequest CreateGamePlayPostRequest(String payload, string productId, string moduleId, string newToken)
        {
            var restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("X-Route-ProductId", productId);
            restRequest.AddHeader("X-Route-ModuleId", moduleId);
            restRequest.AddHeader("X-Clienttypeid", "38");
            restRequest.AddParameter("Authorization", string.Format("Bearer " + newToken));
            restRequest.AddHeader("X - correlationid", "93D10259 - 30F8 - 4339 - B456 - 3F30A43F65A2");
            restRequest.AddParameter("application/json", payload, ParameterType.RequestBody);
            return restRequest;
        }

        // Respoce part
        public IRestResponse GetResponce(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }
        public DTO GetContent<DTO>(IRestResponse response)
        {
            var content = response.Content;
            DTO DTOobject = JsonConvert.DeserializeObject<DTO>(content);
            return DTOobject;
        }
        public string serialize(dynamic content)
        {
            string serializeObject = JsonConvert.SerializeObject(content, Formatting.Indented);
            return serializeObject;
        }

    }
}
