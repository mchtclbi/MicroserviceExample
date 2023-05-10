using RestSharp;
using System.Text.Json;
using Neredekal.RestHelper.Interfaces;

namespace Neredekal.RestHelper.Concretes
{
    public class JsonContent : IContent
    {
        public RestRequest GetRestRequest(object data)
        {
            var restRequest = new RestRequest();
            restRequest.AddJsonBody(JsonSerializer.Serialize(data), "application/json");

            return restRequest;
        }
    }
}