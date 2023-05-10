using Neredekal.RestHelper.Interfaces;

namespace Neredekal.RestHelper.Concretes
{
    public class BearerAuthenticator : IBearerAuthenticator
    {
        public string Authenticate(string token) => $"Bearer {token}";
    }
}