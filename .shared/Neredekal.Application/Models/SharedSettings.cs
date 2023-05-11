namespace Neredekal.Application.Models
{
    public class SharedSettings
    {
        public BaseUrl BaseUrl { get; set; }
        public Endpoint Endpoint { get; set; }
    }

    public class BaseUrl
    {
        public string GateWay { get; set; }
    }

    public class Endpoint
    {
        public UserApi UserApi { get; set; }
    }

    public class UserApi
    {
        public string UserConfirm { get; set; }
    }
}