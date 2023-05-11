namespace Neredekal.AuthAPI.Models.Request
{
    public class CreateTokenRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}