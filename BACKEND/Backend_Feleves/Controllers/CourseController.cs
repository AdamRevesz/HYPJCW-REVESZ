using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.Course;
using Logic;
using System.Security.Claims;
using Models.Dtos.Picture;

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

        [HttpPost("addcourse")]
        //[Authorize]
        public async Task<IActionResult> AddCourse([FromForm] CourseCreateUpdateDto dto, [FromForm] IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                string folderPath = Path.Combine("..", "..", "FrontEnd_Feleves", "src", "assets", "UploadedPictures");
                Directory.CreateDirectory(folderPath);

                var fileName = Path.GetFileName(uploadedFile.FileName);
                var fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await uploadedFile.CopyToAsync(stream);

                dto.FilePath = $"assets/UploadedPictures/{fileName}";
            }

            await logic.AddCourse(dto);

            return Ok();
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




