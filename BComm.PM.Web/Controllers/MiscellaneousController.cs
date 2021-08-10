using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("miscellaneous")]
    [ApiController]
    public class MiscellaneousController : ControllerBase
    {
        [HttpGet("awake")]
        public IActionResult Awake()
        {
            return Ok("I'm awake now...");
        }
    }
}
