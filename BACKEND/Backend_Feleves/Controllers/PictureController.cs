using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Picture;
using Logic;
using System.Security.Claims;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        PictureLogic logic;

        public PictureController(PictureLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("/contents")]
        [Authorize]
        public void AddPicture(PictureCreateUpdateDto dto)
        {
            logic.AddPicture(dto);
        }

        [HttpGet("/contents")]
        public IEnumerable<PictureShortViewDto> GetAllPictures()
        {
            return logic.GetAllPictures();
        }

        [HttpDelete("/pictures/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeletePicture(string id)
        {
            logic.DeletePicture(id);
        }

        [HttpDelete("/pictures/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteOwnerPicture(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.DeleteOwnerPicture(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("/pictures/{id}")]
        [Authorize]
        public IActionResult UpdatePicture(string id, [FromBody] PictureCreateUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.UpdatePicture(id, dto, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("/pictures/{id}")]
        public PictureViewDto GetPicture(string id)
        {
            return logic.GetPicture(id);
        }
    }
}


