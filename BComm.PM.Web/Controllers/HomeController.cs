using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BComm.PM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _hostURL;

        public HomeController(IConfiguration configuration)
        {
            _hostURL = configuration.GetSection("HostURL").Value;
        }

        public IActionResult Index()
        {
            return Redirect(_hostURL + "retail-admin/portal");
        }
    }
}
