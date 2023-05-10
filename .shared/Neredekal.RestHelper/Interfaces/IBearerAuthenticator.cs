namespace Neredekal.RestHelper.Interfaces
{
    public interface IBearerAuthenticator : IAuthenticator
    {
        public string Authenticate(string token);
    }
}