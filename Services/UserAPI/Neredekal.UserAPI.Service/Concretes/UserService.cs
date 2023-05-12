using Neredekal.UserAPI.Models;
using Neredekal.Data.Concretes;
using Neredekal.Data.Interfaces;
using Neredekal.Application.Helper;
using Neredekal.UserAPI.Models.Request;
using Neredekal.UserAPI.Models.Entities;
using Neredekal.UserAPI.Models.Response;
using Neredekal.UserAPI.Service.Interfaces;
using Neredekal.Application.Models.Response;

namespace Neredekal.UserAPI.Service.Concretes
{
    public class UserService : IUserService
    {
        private readonly IMongoRepository<User> _userRepository;

        public UserService()
        {
            _userRepository = new MongoRepository<User>();
        }

        public BaseResponse<object> CreateDummyUser()
        {
            var response = new BaseResponse<object>();

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "UserName",
                Surname = "UserSurname",
                EMail = "testuser@neredekal.com",
                Password = Encrypt.MD5("123456"),
                IsActive = true,
            };

            _userRepository.Add(user);

            user.Password = "123456";

            response.Data = user;
            response.SetMessage("Dummy user created", true);

            return response;
        }

        public BaseResponse<UserConfirmResponse> UserConfirm(UserConfirmRequest request)
        {
            var response = new BaseResponse<UserConfirmResponse>();

            try
            {
                var passwordHash = Encrypt.MD5(request.Password);

                var user = _userRepository.Get(q => q.EMail.Equals(request.UserName) 
                && q.Password.Equals(passwordHash)
                && q.IsActive);
                if (user is null)
                {
                    response.SetMessage(ConstantMessage.UserNofFound);
                    return response;
                }

                response.SetMessage(ConstantMessage.UserConfirm, true);
                response.Data = new UserConfirmResponse()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    EMail= user.EMail
                };
            }
            catch (Exception)
            {
                response.SetMessage(ConstantMessage.ExceptionMessage);
            }

            return response;
        }
    }
}