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

        [HttpPost("/addpicture")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddPicture( [FromForm] PictureCreateUpdateDto dto, [FromForm] IFormFile uploadedFile)
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

            await logic.AddPicture(dto);

            return Ok(new {message = "Picture added succsessfully"});
        }

        [HttpPost("/addpicturelist")]
        public async Task<IActionResult> AddPictureList([FromBody] PictureCreateDto dtos)
        {
            await logic.AddPictureList(dtos);
            return Ok();
        }

        [HttpGet]
        public IEnumerable<PictureShortViewDto> GetAllPictures()
        {
            return logic.GetAllPictures();
        }

        [HttpDelete("/deletepicture/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeletePicture(string id)
        {
            logic.DeletePicture(id);
        }

        [HttpDelete("/deleteownerpicture/{id}")]
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
        [HttpPut("/updatepicture/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdatePicture(string id, [FromForm] PictureCreateUpdateDto dto, [FromForm] IFormFile? uploadedFile)
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
            else
            {
                var existing = logic.GetPictureEntity(id);
                if (existing != null)
                {
                    dto.FilePath = existing.FilePath;
                }
            }

            logic.UpdatePicture(id, dto);
            return Ok(new {message = "Picture updated successfully"});
        }

        [HttpPut("/updatepictureadmin/{id}")]
        public IActionResult UpdatePictureAdmin(string id, [FromBody] PictureCreateUpdateDto dto)
        {
            logic.UpdatePictureAdmin(id, dto);
            return Ok();
        }

        [HttpGet("/getpicture/{id}")]
        public PictureViewDto GetPicture(string id)
        {
            return logic.GetPicture(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddPictureFromJson (string json)
        {
            logic.AddPicturesFromJson(json);
            return Ok();
        }
    }
}


