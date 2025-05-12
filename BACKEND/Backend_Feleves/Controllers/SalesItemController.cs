using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos.SalesItem;
using Logic;
using System.Security.Claims;

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

        [HttpPost("/salesitems")]
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

        [HttpDelete("/salesitems/{id}")]
        [Authorize(Roles = "Admin")]
        public void DeleteSalesItem(string id)
        {
            logic.DeleteSalesItem(id);
        }

        [HttpDelete("/salesitems/{id}")]
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

        [HttpPut("/salesitems/{id}")]
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

        [HttpGet("/salesitems/{id}")]
        public SalesItemViewDto GetSalesItem(string id)
        {
            return logic.GetSalesItem(id);
        }
    }
}




