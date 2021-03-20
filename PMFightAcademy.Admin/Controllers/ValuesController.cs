using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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