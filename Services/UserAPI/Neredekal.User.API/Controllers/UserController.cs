using Microsoft.AspNetCore.Mvc;
using Neredekal.UserAPI.Models.Request;
using Neredekal.UserAPI.Service.Interfaces;

namespace Neredekal.User.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService= userService;
        }

        [HttpPost("api/user/create-dummy")]
        public IActionResult Create() => Ok(_userService.CreateDummyUser());

        [HttpPost("api/user/confirm")]
        public IActionResult UserConfirm([FromBody] UserConfirmRequest request) => Ok(_userService.UserConfirm(request));
    }
}