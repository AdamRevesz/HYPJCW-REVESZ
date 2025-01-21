using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;

namespace Backend_Feleves.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        RateLogic<Content> rateLogic;

        public RateController(RateLogic<Content> rateLogic)
        {
            this.rateLogic = rateLogic;
        }

        [HttpPost("{id}/like")]
        public IActionResult Like(string id)
        {
            try
            {
                rateLogic.Like(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/dislike")]
        public IActionResult Dislike(string id)
        {
            try
            {
                rateLogic.Dislike(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
