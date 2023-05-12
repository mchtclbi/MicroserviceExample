using System;

namespace Neredekal.AuthAPI.Models.Response
{
    public class CreateTokenResponse
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}