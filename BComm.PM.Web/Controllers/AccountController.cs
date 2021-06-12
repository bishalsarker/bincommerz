using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BComm.PM.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace BComm.PM.Web.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index(string return_uri, string client)
        {
            if (return_uri != null && client != null)
            {
                ViewBag.return_uri = return_uri;
                ViewBag.client_id = client;
                ViewBag.error = TempData["error"];

                return View();
            }
            else
            {
                return RedirectToAction("PageNotFound");
            }
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password, string client_id, string return_uri)
        {
            var shop = _authService.GetShop(username, password);

            if (shop != null)
            {
                var authCallbackUrl = _authService.GetLoginRedirectUri(client_id, return_uri, shop);
                Console.WriteLine(authCallbackUrl);
                return Redirect(authCallbackUrl);
            }
            else
            {
                TempData["error"] = "Wrong username or password";

                return RedirectToAction("Index", new
                {
                    return_uri = return_uri,
                    client = client_id
                });
            }
        }
    }
}
