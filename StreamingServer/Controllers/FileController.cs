using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StreamingServer.DTOs;
using StreamingServer.Interfaces;

namespace StreamingServer.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FileController(IFileService fileService) : Controller
    {

        [HttpPost]
        public async Task<ActionResult<string>> uploadFile(IFormFile file)
        {
            //var file =Request.Form.Files.FirstOrDefault();
            if (file != null)
                return Ok(await fileService.saveFile(file));
            return BadRequest();

        }

        [HttpGet("{mediaName}")]
        public dynamic media([FromRoute(Name = "mediaName")] string mediaName)
        {
            if (String.IsNullOrEmpty(mediaName))
                return BadRequest();

            FileStream fs = fileService.GetMedia(mediaName);
          
            return Results.File(fs, mediaName.Contains(".ts", StringComparison.InvariantCultureIgnoreCase) ? "video/MP2T" : "application/vnd.apple.mpegurl");

        }

      
    }
}
