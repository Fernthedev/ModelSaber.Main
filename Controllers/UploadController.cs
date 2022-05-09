using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelSaber.Main.Helpers;

namespace ModelSaber.Main.Controllers
{
    [ApiController]
    [Authorize]
    public class UploadController : ControllerBase
    {
        [HttpPost("api/upload")]
        public IActionResult Upload([FromForm] UploadModel model)
        {
            var modelFile = model.Model;
            var imgFile = model.Img;
            if (modelFile == null || imgFile == null || model.ModelId == null || imgFile.Length == 0 || modelFile.Length == 0)
                return BadRequest();
            return Ok();
        }
    }

    public class UploadModel
    {
        public IFormFile? Model { get; set; }
        public IFormFile? Img { get; set; }
        public uint? ModelId { get; set; }
    }
}
