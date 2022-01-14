using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelSaber.Models;
using ModelSaber.Main.Helpers;
using ModelSaber.Main.Services;

namespace ModelSaber.Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Could not login user." });

            Response.Cookies.Append("login", response.Token, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(7) });
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Authenticated()
        {
            var user = (User?)HttpContext.Items["User"];
            return Ok(new { user?.Name });
        }
    }

    public class LoginModel
    {
        public ulong? DiscordId { get; set; } // required if using discord sign in. Only way to do signing from start while still developing email + pass sign in.
        public string Email { get; set; } = ""; // required in case needing to contact person and can't find them on discord. NOT IN USE RIGHT NOW
        public string Name { get; set; } = ""; // required send due to needed info. Will always be required to be a valid string based of readable characters.
        public string Avatar { get; set; } = "";
        public string Password { get; set; } = "";

        public User MakeUser()
        {
            return new User
            {
                DiscordId = DiscordId,
                Name = Name,
                Level = UserLevel.Normal,
                Avatar = Avatar
            };
        }
    }
}
