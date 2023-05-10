namespace Neredekal.RestHelper.Interfaces
{
    public interface IBasicAuthenticator : IAuthenticator
    {
        public string Authenticate(string username, string password);
    }
}