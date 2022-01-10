using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelSaber.Database;

namespace ModelSaber.Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ModelSaberDbContext _dbContext;

        public UserController(ModelSaberDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<bool> FindUser([FromQuery] ulong discordId)
        {
            return _dbContext.Users.Any(t => t.DiscordId == discordId && t.Avatar != null);
        }

        [HttpPost]
        public IActionResult CreateUser(LoginModel model)
        {
            var user = _dbContext.Users.SingleOrDefault(t => t.DiscordId == model.DiscordId);
            if (user != null)
            {
                user.Avatar = model.Avatar;
                user.Name = model.Name;
            }
            else
            {
                _dbContext.Users.Add(model.MakeUser());
                _dbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
