using System.Text;
using Neredekal.RestHelper.Interfaces;

namespace Neredekal.RestHelper.Concretes
{
    public class BasicAuthenticator : IBasicAuthenticator
    {
        public string Authenticate(string username, string password) =>
            $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))}";
    }
}