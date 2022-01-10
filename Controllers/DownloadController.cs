using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelSaber.Database;
using ModelSaber.Database.Models;

namespace ModelSaber.Main.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly ModelSaberDbContext _dbContext;

        public DownloadController(ModelSaberDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult DownloadModel([FromQuery] Guid id)
        {
            var model = _dbContext.Models.FirstOrDefault(t => t.Uuid == id);
            return Ok(new {Id = id, Filename = $"{model?.Name}.{model?.Type.GetTypeExt()}"});
        }
    }
}
