using RestSharp;
using Neredekal.RestHelper.Interfaces;

namespace Neredekal.RestHelper.Concretes
{
    public class QueryContent : IContent
    {
        public RestRequest GetRestRequest(object data)
        {
            var restRequest = new RestRequest();

            if (data is null) return restRequest;

            if (data is Dictionary<string, string>)
            {
                ((Dictionary<string, string>)data).ToList().ForEach(q => restRequest.AddParameter(q.Key, q.Value, ParameterType.QueryString));
                return restRequest;
            }

            throw new Exception($"{data.GetType()} is not supported");
        }
    }
}