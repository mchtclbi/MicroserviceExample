using RestSharp;
using System.Threading.Tasks;
using Neredekal.RestHelper.Models;

namespace Neredekal.RestHelper
{
    public class SendRestRequest
    {
        public string BaseURL { get; set; }

        public SendRestRequest(string baseURL)
        {
            BaseURL = baseURL;
        }

        public RestResponse<T> Run<T>(RestRequestModel request) where T : new()
        {
            RestClient restClient = new RestClient(BaseURL);
            RestRequest restRequest = GetRestRequest(request);

            var serviceResponse = restClient.Execute<T>(restRequest);

            return serviceResponse;
        }

        public async Task<RestResponse<T>> RunAsync<T>(RestRequestModel request) where T : new()
        {
            RestClient restClient = new RestClient(BaseURL);
            RestRequest restRequest = GetRestRequest(request);

            var serviceResponse = await restClient.ExecuteAsync<T>(restRequest);

            return serviceResponse;
        }

        private RestRequest GetRestRequest(RestRequestModel request)
        {
            RestRequest restRequest = request.Content.GetRestRequest(request.Data);

            restRequest.Method = request.Method;

            if (!string.IsNullOrEmpty(request.Endpoint))
                restRequest.Resource = request.Endpoint;

            if (!string.IsNullOrEmpty(request.Authenticator))
                restRequest.AddHeader("Authorization", request.Authenticator);

            if (request.Header != null)
                restRequest.AddHeaders(request.Header);

            return restRequest;
        }
    }
}