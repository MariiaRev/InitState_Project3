﻿using Microsoft.AspNetCore.Mvc;

namespace PMFightAcademy.Client.Controllers
{
    /// <summary>
    /// Just information about service.
    /// </summary>
    [Route("")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Just information about service.
        /// </summary>
        [HttpGet("")]
        public IActionResult GetInfo()
        {
            return Ok("clients service");
        }
    }
}
