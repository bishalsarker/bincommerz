using BComm.PM.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("processes")]
    [ApiController]
    public class ProcessesController : ControllerBase
    {
        private readonly IProcessService _processService;

        public ProcessesController(IProcessService processService)
        {
            _processService = processService;
        }

        [HttpGet("getnextprocess/{currentStep}")]
        public async Task<IActionResult> GetNextProcess(int currentStep)
        {
            return Ok(await _processService.GetNextProcess("vbt_xyz", currentStep));
        }
    }
}
