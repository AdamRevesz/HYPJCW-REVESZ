using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Video;
using Logic;
using System.Security.Claims;

namespace MovieClub.Endpoint.Controllers
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

        [HttpPost]
        [Authorize]
        public void AddVideo(VideoCreateUpdateDto dto)
        {
            logic.AddVideo(dto);
        }

        [HttpGet]
        public IEnumerable<VideoShortViewDto> GetAllVideos()
        {
            return logic.GetAllVideos();
        }

        [HttpDelete("/deletevideo/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteVideo(string id)
        {
            logic.DeleteVideo(id);
        }

        [HttpDelete("/deleteownervideo/{id}")]
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

        [HttpPut("/updatevideo/{id}")]
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

        [HttpGet("/getvideo/{id}")]
        public VideoViewDto GetVideo(string id)
        {
            return logic.GetVideo(id);
        }
    }
}



