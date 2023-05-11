using RestSharp;
using System.Text;
using System.Text.Json;
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
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtModel = _jwtModel ?? _configuration.GetSection("JWt").Get<JWTModel>();
        }

        public async Task<BaseResponse<CreateTokenResponse>> CreateToken(CreateTokenRequest request)
        {
            var response = new BaseResponse<CreateTokenResponse>();

            var baseurl = _configuration.GetSection("BaseUrl").GetValue<string>("Gateway");
            var endpoint = _configuration.GetSection("Endpoint").GetSection("UserApi").GetValue<string>("UserConfirm");

            SendRestRequest restRequest = new SendRestRequest(baseurl);
            var serviceResponse = await restRequest.RunAsync<BaseResponse<UserConfirmResponse>>(new RestRequestModel()
            {
                Endpoint = endpoint,
                Method = Method.Post,
                Content = new JsonContent(),
                Data = request
            });

            if (serviceResponse == null || !string.IsNullOrEmpty(serviceResponse.Content))
            {
                response.SetMessage("Kullanıcı bilgileri alınamadı.");
                return response;
            }

            var data = JsonSerializer.Deserialize<BaseResponse<UserConfirmResponse>>(serviceResponse.Content);
            if (data == null || !data.IsSuccess)
            {
                response.SetMessage("Kullanıcı bilgileri alınamadı.");
                return response;
            }

            response.SetMessage("transaction is success", true);
            response.Data = new CreateTokenResponse()
            {
                Token = CreateJWTToken(data.Data.Id)
            };

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