using Microsoft.AspNetCore.Mvc;

namespace PMFightAcademy.Admin.Controllers
{
    /// <summary>
    /// Just information about service.
    /// </summary>
    [Route("")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult GetInfo()
        {
            return Ok("administrators service");
        }
    }
}