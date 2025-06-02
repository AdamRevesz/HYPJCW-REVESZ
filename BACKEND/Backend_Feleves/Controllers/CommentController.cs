using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Comment;
using Logic;
using System.Security.Claims;
using Logic.Helper;
using Microsoft.AspNetCore.Identity;
using Models;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentLogic _logic;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DtoProvider _dtoProvider;

        public CommentController(CommentLogic logic, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DtoProvider dtoProvider)
        {
            _logic = logic;
            _userManager = userManager;
            _roleManager = roleManager;
            _dtoProvider = dtoProvider;
        }

        [HttpPost("/api/addcomment/")]
        public IActionResult AddComment([FromQuery]string contentId, CommentCreateUpdateDto dto)
        {
            try
            {
                _logic.AddComment(contentId, dto);
                return Ok(new { message = "Comment added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("/api/{contentId}")]
        public async Task<IEnumerable<CommentViewDto>> GetAllComments(string contentId)
        {
            return await _logic.GetAllComments(contentId);
        }

        [HttpDelete("deletecomment/{id}")]
        public IActionResult DeleteComment(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Comment ID is required");
            }

            try
            {
                _logic.DeleteComment(id);
                return Ok( new {message = "Comment deleted successfully."});
            }
            catch (Exception ex)
            {
                return NotFound($"Comment not found: {ex.Message}");
            }
        }

        [HttpPut("/api/updatecomment/{id}")]
        //[Authorize]
        public IActionResult UpdateComment(string id, [FromBody] CommentCreateUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                _logic.UpdateComment(id, userId, dto);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
