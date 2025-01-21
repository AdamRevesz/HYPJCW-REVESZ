using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Comment;
using Logic;
using System.Security.Claims;

namespace MovieClub.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        CommentLogic logic;

        public CommentController(CommentLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("/addcomment/{contentId}")]
        [Authorize]
        public void AddComment(string contentId, CommentCreateUpdateDto dto)
        {
            logic.AddComment(contentId, dto);
        }

        [HttpGet("{contentId}")]
        public IEnumerable<CommentViewDto> GetAllComments(string contentId)
        {
            return logic.GetAllComments(contentId);
        }

        [HttpDelete("/deletecomment/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteComment(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.DeleteComment(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("/updatecomment/{id}")]
        [Authorize]
        public IActionResult UpdateComment(string id, [FromBody] CommentCreateUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.UpdateComment(id,userId, dto);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}





