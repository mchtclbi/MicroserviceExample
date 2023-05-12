using Neredekal.UserAPI.Models.Request;
using Neredekal.UserAPI.Models.Response;
using Neredekal.Application.Models.Response;

namespace Neredekal.UserAPI.Service.Interfaces
{
    public interface IUserService
    {
        public BaseResponse<object> CreateDummyUser();

        public BaseResponse<UserConfirmResponse> UserConfirm(UserConfirmRequest request);
    }
}