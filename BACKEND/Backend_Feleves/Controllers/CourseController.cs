using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Course;
using Logic;
using System.Security.Claims;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        CourseLogic logic;

        public CourseController(CourseLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("/contents")]
        //[Authorize]
        public void AddCourse(CourseCreateUpdateDto dto)
        {
            logic.AddCourse(dto);
        }

        [HttpGet("/contents")]
        public IEnumerable<CourseShortViewDto> GetAllCourses()
        {
            return logic.GetAllCourses();
        }

        [HttpDelete("/courses/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteCourse(string id)
        {
            logic.DeleteCourse(id);
        }

        [HttpDelete("/courses/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteOwnerCourse(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.DeleteOwnerCourse(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("/courses/{id}")]
        [Authorize]
        public IActionResult UpdateCourse(string id, [FromBody] CourseCreateUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.UpdateCourse(id, dto, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("/courses/{id}")]
        public CourseViewDto GetCourse(string id)
        {
            return logic.GetCourse(id);
        }
    }
}




