using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.SalesItem;
using Logic;
using System.Security.Claims;

namespace MovieClub.Endpoint.Controllers
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

        [HttpPost]
        [Authorize]
        public void AddSalesItem(SalesItemCreateUpdateDto dto)
        {
            logic.AddSalesItem(dto);
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
        [Authorize]
        public IActionResult UpdateSalesItem(string id, [FromBody] SalesItemCreateUpdateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                logic.UpdateSalesItem(id, dto, userId);
                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpGet("/getsalesitem/{id}")]
        public SalesItemViewDto GetSalesItem(string id)
        {
            return logic.GetSalesItem(id);
        }
    }
}




