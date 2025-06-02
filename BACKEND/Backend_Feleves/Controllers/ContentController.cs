using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Data;
using Models;
using Models.Dtos.Content;
using Logic;
using System.Security.Claims;
using Models.Dtos.Video;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        ContentLogic logic;

        public ContentController(ContentLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        //[Authorize]
        public void AddContent(ContentCreateDto dto)
        {
            logic.AddContent(dto);
        }

        [HttpGet]
        public IEnumerable<ContentShortViewDto> GetAllContent()
        {
            return logic.GetAllContent();
        }

        [HttpDelete("contentdelete/{id}")]
        //[Authorize(Roles = "Admin")]
        public void DeleteContent(string id)
        {
            logic.DeleteContent(id);
        }


        [HttpDelete("contentownerdelete/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteOwnerContent(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.DeleteOwnerContent(id, userId);
                return Ok(new { message = "Content deleted successfully" });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("contentupdate/{id}")]
        //[Authorize]
        public IActionResult UpdateContent(string id, [FromBody] ContentCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.UpdateContent(id, userId, dto);
                return Ok(new { message = "Content updated successfully" });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("getcontent/{id}")]
        public ContentViewDto GetContent(string id)
        {
            return logic.GetContent(id);
        }

        [HttpDelete]
        public void DeleteAllContent()
        {
            logic.DeleteAllContent();
        }

        [HttpPut("randomstats")]
        public void RandomLikes()
        {
            logic.LikeDislikeViewership();
        }

        [HttpPut("randomtags")]
        public void RandomTags()
        {
            logic.RandomTag();
        }

        [HttpPut("randompictures")]
        public void RandomPictures()
        {
            logic.RandomFilePath();
        }

        [HttpDelete("Withoutowner")]
        public void DeleteWithoutOwner()
        {
            logic.DeleteAllContentWithNoExistingUserId();
        }
    }
}