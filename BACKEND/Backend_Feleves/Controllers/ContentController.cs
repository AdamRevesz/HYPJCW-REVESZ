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

        [HttpPost("/contents")]
        [Authorize]
        public void AddContent(ContentCreateDto dto)
        {
            logic.AddContent(dto);
        }

        [HttpGet("/contents")]
        public IEnumerable<ContentShortViewDto> GetAllContent()
        {
            return logic.GetAllContent();
        }

        [HttpDelete("/contents/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteContent(string id)
        {
            logic.DeleteContent(id);
        }


        [HttpDelete("/contents/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteOwnerContent(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.DeleteOwnerContent(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("/contents/{id}")]
        //[Authorize]
        public IActionResult UpdateContent(string id, [FromBody] ContentCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.UpdateContent(id, userId, dto);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("/contents/{id}")]
        public ContentViewDto GetContent(string id)
        {
            return logic.GetContent(id);
        }
    }
}