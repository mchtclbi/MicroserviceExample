using RestSharp;

namespace Neredekal.RestHelper.Interfaces
{
    public interface IContent
    {
        public RestRequest GetRestRequest(object data);
    }
}