using RestSharp;
using System.Text;
using Neredekal.RestHelper;
using System.Security.Claims;
using Neredekal.AuthAPI.Models;
using Neredekal.RestHelper.Models;
using Microsoft.IdentityModel.Tokens;
using Neredekal.RestHelper.Concretes;
using System.IdentityModel.Tokens.Jwt;
using Neredekal.AuthAPI.Models.Request;
using Neredekal.AuthAPI.Models.Response;
using Microsoft.Extensions.Configuration;
using Neredekal.AuthAPI.Service.Interfaces;
using Neredekal.Application.Models.Response;

namespace Neredekal.AuthAPI.Service.Concretes
{
    public class AuthService : IAuthService
    {
        private readonly JWTModel _jwtModel;
        private readonly UserApiUrl _userApiUrl;
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtModel ??= _configuration.GetSection("JWt").Get<JWTModel>();
            _userApiUrl ??= _configuration.GetSection("Url").GetSection("UserApi").Get<UserApiUrl>();
        }

        public async Task<BaseResponse<CreateTokenResponse>> CreateToken(CreateTokenRequest request)
        {
            var response = new BaseResponse<CreateTokenResponse>();

            try
            {
                SendRestRequest restRequest = new SendRestRequest(_userApiUrl.BaseUrl);
                var serviceResponse = await restRequest.RunAsync<BaseResponse<UserConfirmResponse>>(new RestRequestModel()
                {
                    Endpoint = _userApiUrl.Endpoint.UserConfirm,
                    Method = Method.Post,
                    Content = new JsonContent(),
                    Data = request
                });

                if (serviceResponse == null || serviceResponse.Data == null || string.IsNullOrEmpty(serviceResponse.Content))
                {
                    response.SetMessage("user informations not read.");
                    return response;
                }

                response.SetMessage("transaction is success", true);
                response.Data = new CreateTokenResponse()
                {
                    UserId = serviceResponse.Data.Data.Id,
                    Token = CreateJWTToken(serviceResponse.Data.Data.Id)
                };
            }
            catch (Exception)
            {
                response.SetMessage("Please try again later!");
            }

            return response;
        }

        private string CreateJWTToken(Guid id)
        {
            var key = Encoding.ASCII.GetBytes(_jwtModel.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtModel.Issuer,
                Audience = _jwtModel.Audience,
                Expires = DateTime.UtcNow.AddHours(_jwtModel.ExpireTime),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}