using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Video;
using Logic;
using System.Security.Claims;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        VideoLogic logic;

        public VideoController(VideoLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("/videos")]
        [Authorize]
        public void AddVideo(VideoCreateUpdateDto dto)
        {
            logic.AddVideo(dto);
        }

        [HttpGet("/videos")]
        public IEnumerable<VideoShortViewDto> GetAllVideos()
        {
            return logic.GetAllVideos();
        }

        [HttpDelete("/videos/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteVideo(string id)
        {
            logic.DeleteVideo(id);
        }

        [HttpDelete("/videos/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteOwnerVideo(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.DeleteOwnerVideo(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("/videos/{id}")]
        [Authorize]
        public IActionResult UpdateVideo(string id, [FromBody] VideoCreateUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.UpdateVideo(id, dto, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("/videos/{id}")]
        public VideoViewDto GetVideo(string id)
        {
            return logic.GetVideo(id);
        }
    }
}



