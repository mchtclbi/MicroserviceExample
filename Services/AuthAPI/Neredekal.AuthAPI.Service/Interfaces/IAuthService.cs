using Neredekal.AuthAPI.Models.Request;
using Neredekal.AuthAPI.Models.Response;
using Neredekal.Application.Models.Response;

namespace Neredekal.AuthAPI.Service.Interfaces
{
    public interface IAuthService
    {
        public Task<BaseResponse<CreateTokenResponse>> CreateToken(CreateTokenRequest request);
    }
}