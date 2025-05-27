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
            _logic.AddComment(contentId,dto);
            return Ok("Comment added successfully.");
        }

        [HttpGet("/api/{contentId}")]
        public async Task<IEnumerable<CommentViewDto>> GetAllComments(string contentId)
        {
            return await _logic.GetAllComments(contentId);
        }

        [HttpDelete("/api/deletecomment/{id}")]
        //[Authorize(Roles = "User")]
        public IActionResult DeleteComment(string id)
        {
                _logic.DeleteComment(id);
                return Ok();
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
