using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BComm.PM.Services.Common
{
    public class AuthService
    {
        private readonly HttpContext _httpContext;

        public AuthService(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetShopId()
        {
            return _httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
        }
    }
}
