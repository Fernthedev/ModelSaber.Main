using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelSaber.Database;
using ModelSaber.Main.Helpers;
using ModelSaber.Models;

namespace ModelSaber.Main.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly ModelSaberDbContext _dbContext;

        public OAuthController(ModelSaberDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("create")]
        [Authorize]
        public IActionResult Create(string name)
        {
            var user = (User?)HttpContext.Items["User"];
            if(user == null)
                return Unauthorized();

            var client = new OAuthClient
            {
                UserId = user.Id,
                Name = name
            };
            client.Init();

            _dbContext.OAuthClients.Add(client);
            _dbContext.SaveChanges();

            return Ok(client.GetClientJson());
        }

        [HttpGet("list")]
        public IActionResult List()
        {
            var user = (User?)HttpContext.Items["User"];
            if(user == null)
                return Unauthorized();
            
            var clients = _dbContext.OAuthClients.Where(t => t.UserId == user.Id).ToList().Select(t => t.GetClientJson());

            return Ok(clients);
        }
    }
}
