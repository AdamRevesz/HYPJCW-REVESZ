using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.SalesItem;
using Logic;
using System.Security.Claims;
using Models.Dtos.Course;
using Models.Dtos.Picture;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesItemController : ControllerBase
    {
        SalesItemLogic logic;

        public SalesItemController(SalesItemLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("addsalesitem")]
        //[Authorize]
        public async Task<IActionResult> AddSalesItem([FromForm] SalesItemCreateUpdateDto dto, [FromForm] IFormFile uploadedFile)
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

            await logic.AddSalesItem(dto);

            return Ok();
        }

        [HttpGet]
        public IEnumerable<SalesItemShortViewDto> GetAllSalesItems()
        {
            return logic.GetAllSalesItems();
        }

        [HttpDelete("/deletesalesitem/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteSalesItem(string id)
        {
            logic.DeleteSalesItem(id);
        }

        [HttpDelete("/deleteownersalesitem/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult DeleteOwnerSalesItem(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.DeleteOwnerSalesItem(id, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("/updatesalesitem/{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateSalesItem(string id, [FromForm] SalesItemCreateUpdateDto dto, [FromForm] IFormFile? uploadedFile)
        {

            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                string folderPath = Path.Combine("..", "..", "FrontEnd_Feleves", "src", "assets", "UploadedPictures");
                Directory.CreateDirectory(folderPath);

                var fileName = Path.GetFileName(uploadedFile.FileName);
                var fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await uploadedFile.CopyToAsync(stream);

                dto.FilePath = Path.Combine("images", fileName);
            }
            else
            {
                var existing = logic.GetPictureEntity(id);
                if (existing != null)
                {
                    dto.FilePath = existing.FilePath;
                }
            }
            logic.UpdateSalesItem(id, dto);
            return Ok(new {message ="SalesItems updated successfully."});

        }

        [HttpGet("/getsalesitem/{id}")]
        public SalesItemViewDto GetSalesItem(string id)
        {
            return logic.GetSalesItem(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddSalesItemsFromJson(string json)
        {
            logic.AddSalesItemsFromJson(json);
            return Ok();
        }
        [HttpPut]
        public void RandomPrice()
        {
            logic.Randomprice();
        }
    }
}




