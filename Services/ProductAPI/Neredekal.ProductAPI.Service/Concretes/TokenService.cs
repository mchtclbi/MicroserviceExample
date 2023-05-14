using System.IdentityModel.Tokens.Jwt;

namespace Neredekal.ProductAPI.Service.Concretes
{
    public class TokenService
    {
        public string GetUserIdWithToken(string token) => ReadToken(token).Claims.First(c => c.Type == "Id").Value;

        private JwtSecurityToken ReadToken(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token);
    }
}