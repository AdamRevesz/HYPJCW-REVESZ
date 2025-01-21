using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Course;
using Logic;
using System.Security.Claims;

namespace MovieClub.Endpoint.Controllers
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

        [HttpPost]
        [Authorize]
        public void AddCourse(CourseCreateUpdateDto dto)
        {
            logic.AddCourse(dto);
        }

        [HttpGet]
        public IEnumerable<CourseShortViewDto> GetAllCourses()
        {
            return logic.GetAllCourses();
        }

        [HttpDelete("/deletecourse/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteCourse(string id)
        {
            logic.DeleteCourse(id);
        }

        [HttpDelete("/deleteownercourse/{id}")]
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

        [HttpPut("/updatecourse/{id}")]
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

        [HttpGet("/getcourse/{id}")]
        public CourseViewDto GetCourse(string id)
        {
            return logic.GetCourse(id);
        }
    }
}




