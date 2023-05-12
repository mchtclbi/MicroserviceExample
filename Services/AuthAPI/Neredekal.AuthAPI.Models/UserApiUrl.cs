namespace Neredekal.AuthAPI.Models
{
    public class UserApiUrl
    {
        public string BaseUrl { get; set; }
        public UserApiEndpoint Endpoint { get; set; }
    }

    public class UserApiEndpoint
    {
        public string UserConfirm { get; set; }
    }
}